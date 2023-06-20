using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Convert_Data.Controller;


namespace Convert_Data
{
    public partial class form_Convert : Form
    {
        Configs configs = new Configs();
        NpgsqlConnection postgresConnection;
        //OracleConnection oracleConnection;
        public form_Convert(Configs configs)
        {
            InitializeComponent();
            this.configs = configs;
        }

        public form_Convert()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_ComboBox_Donvi();
            Load_ComboBox_Schema();
            Console.WriteLine(configs.Des_IP);
            Console.WriteLine(configs.Source_IP);
        }

        private void Load_ComboBox_Schema()
        {
            string query = "SELECT SCHEMA, NAME FROM CLOUD_ADMIN_DEV_BLU_2.AGENT ORDER BY SCHEMA";
            OracleConnection bluConnection = Connection.getInstance().GetDevBlu2Connection();
            cb_Schemas.DataSource = Common.GetSchemasFromBLU(bluConnection, query);
            cb_Schemas.DisplayMember = "schema";
            cb_Schemas.ValueMember = "schema";
            cb_Schemas.SelectedIndex = cb_Schemas.FindString("QLVB_BLU_TINHBACLIEU_1.");

        }

        private void Load_ComboBox_Donvi()
        {
            postgresConnection = Connection.getInstance().GetPostgresConnection();
            cbBox_Donvi.DataSource = Common.GetDanhMucDonvi(postgresConnection, Constants.sql_danhmuc_donvi_schema);
            cbBox_Donvi.DisplayMember = "name";
            cbBox_Donvi.ValueMember = "organizationid";
            cbBox_Donvi.SelectedIndex = cbBox_Donvi.FindString("UBND tỉnh Bạc Liêu");
        }

        private void run_Action(object sender, EventArgs e)
        {
            CollectConfigs();
            Console.WriteLine(configs.Donvi_lay_du_lieu);
            if (configs.Schema != String.Empty && configs.Year != String.Empty)
            {
                Thread thread = new Thread(() => Convert_Data());
                thread.Start();
            } else
            {
                txt_Progress.Text = "====Invalid Input====";
                run_action.Enabled = true;
            }
        }

        private void Convert_Data()
        {
            // Create connection
            //NpgsqlConnection postgresConnection = new NpgsqlConnection(Constants.postgres_connstring);
            //OracleConnection oracleConnection = Connection.getInstance().GetOracleConnection();
            //string oracle_connstr = string.Format(Constants.oracle_connstring, configs.Des_IP, configs.Des_Port, configs.Des_Service, configs.Des_User, configs.Des_Pass);
            OracleConnection oracleConnection = new OracleConnection(Constants.oracle_connstring);
            oracleConnection.Open();
            // Initial timer
            Stopwatch timer = new Stopwatch();
            string list_seq = "";
            try
            {
                // Open connection
                if (chkbox_deleteCategory.Checked)
                {
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Deleting Categories ..."));
                    //Import_VanBan.exportdataFromPostgres(postgresConnection, configs, Constants.sql_delete_table, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Deleted Categories!"));
                    timer.Stop();
                    //Console.WriteLine("Total exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported data: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                if (chkbox_DeleteDoc.Checked)
                {
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Deleting Docs ..."));
                    //Common.DeleteAllTableRelatedToDcmDoc(oracleConnection, configs.Schema);
                    //Common.DeleteDCM_DOC(oracleConnection, configs.Schema);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Deleted Docs!"));
                    timer.Stop();
                    //Console.WriteLine("Total exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported data: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Hinh thuc
                if (this.chkBox_HinhThuc.Checked)
                {
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting hinh thuc data from Postgres ..."));
                    Import_DCM_TYPE import_DCM_TYPE = new Import_DCM_TYPE();
                    string query = string.Format(Constants.sql_danhmuc_dcm_type, configs.Donvi_lay_du_lieu);
                    import_DCM_TYPE.exportdataFromPostgres(postgresConnection, CommandType.Text, query, Common.VB_TYPE.NONE);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported hinh thuc data from Postgres !"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported hinh thuc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    import_DCM_TYPE.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_TYPE.ToString());

                    // Import to DCM_TYPE
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting hinh thuc to dcm_linhvuc ..."));
                    import_DCM_TYPE.insert_Dcm_Type(oracleConnection, configs.Schema, Constants.SQL_INSERT_DCM_TYPE, import_DCM_TYPE.GetDCM_TYPEs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted hinh thuc to dcm_linhvuc !"));
                    timer.Stop();
                    Console.WriteLine("Consumption of imported data to dcm_type: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Linh vuc
                if (this.chkBox_Linhvuc.Checked)
                {
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting linh vuc data from Postgres ..."));
                    Import_LinhVuc import_LinhVuc = new Import_LinhVuc();
                    string query = string.Format(Constants.sql_danhmuc_linhvuc, configs.Donvi_lay_du_lieu);
                    import_LinhVuc.exportdataFromPostgres(postgresConnection, CommandType.Text, query, Common.VB_TYPE.NONE);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported linh vuc data from Postgres !"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported linh vuc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Check current records
                    import_LinhVuc.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_LINHVUC.ToString());

                    // Import to DCM_LINHVUC
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting linh vuc to dcm_linhvuc ..."));
                    import_LinhVuc.insert_Dcm_Linhvuc(oracleConnection, configs.Schema, Constants.SQL_INSERT_DCM_LINHVUC);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted linh vuc to dcm_linhvuc !"));
                    timer.Stop();
                    Console.WriteLine("Consumption of imported data to dcm_linhvuc: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // So van ban
                if (this.chkBox_Book.Checked)
                {
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting book data from Postgres..."));
                    Import_SoVanBan import_SoVanBan = new Import_SoVanBan();
                    DataTable dcm_type = Common.GetDcm_Type(oracleConnection, configs.Schema, Constants.sql_get_dcm_type);
                    string query = string.Format(Constants.sql_danhmuc_sovanban, configs.Donvi_lay_du_lieu);
                    
                    if (chkBox_AppendData.Checked)
                    {
                        list_seq += Constants.SEQ_DCM_SOVB_TEMPLATESINHSO + ",";
                        list_seq += Constants.SEQ_DCM_QUYTAC_NHAYSO + ",";
                        Import_DCM_SOVB_TEMPLATESINHSO.SEQ_DCM_SOVB_TEMPLATESINHSO = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_SOVB_TEMPLATESINHSO);
                        Import_DCM_QUYTAC_NHAYSO.SEQ_DCM_QUYTAC_NHAYSO = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_QUYTAC_NHAYSO);
                    } else
                    {
                        Import_DCM_QUYTAC_NHAYSO.SEQ_DCM_QUYTAC_NHAYSO = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_QUYTAC_NHAYSO);
                        Import_DCM_SOVB_TEMPLATESINHSO.SEQ_DCM_SOVB_TEMPLATESINHSO = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_SOVB_TEMPLATESINHSO);
                    }
                    
                    import_SoVanBan.exportdataFromPostgres(postgresConnection, CommandType.Text, query, Common.VB_TYPE.NONE);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported book data from Postgres..."));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported book data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    import_SoVanBan.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_SOVANBAN.ToString());

                    // Import to Dcm_SoVanBan
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_SoVanBan ..."));
                    query = string.Format(Constants.SQL_INSERT_DCM_SOVANBAN, configs.Schema);
                    import_SoVanBan.insert_Dcm_SoVanBan(oracleConnection, query);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_SoVanBan!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to Dcm_SoVanBan: " + import_SoVanBan.GetDcm_SoVanBans().Count);
                    Console.WriteLine("Consumption of imported data to Dcm_SoVanBan: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Export DCM_SOVB_TEMPLATESINHSO
                    Import_DCM_SOVB_TEMPLATESINHSO import_DCM_SOVB_TEMPLATESINHSO = new Import_DCM_SOVB_TEMPLATESINHSO();
                    //import_DCM_SOVB_TEMPLATESINHSO.ExportData(import_SoVanBan.GetDcm_SoVanBans(), dcm_type);

                    // Import to Dcm_SoVB_TemplateSinhSo
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_SoVB_TemplateSinhSo ..."));

                    query = string.Format(Constants.sql_insert_Dcm_SoVB_TemplateSinhSo, configs.Schema);
                    //import_DCM_SOVB_TEMPLATESINHSO.insert_Dcm_SoVB_TemplateSinhSo(oracleConnection, query, import_DCM_SOVB_TEMPLATESINHSO.GetDCM_SOVB_TEMPLATESINHSOs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_SoVB_TemplateSinhSo!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to Dcm_SoVB_TemplateSinhSo: " + import_DCM_SOVB_TEMPLATESINHSO.GetDCM_SOVB_TEMPLATESINHSOs().Count);
                    Console.WriteLine("Consumption of imported data to Dcm_SoVB_TemplateSinhSo: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Export DCM_QUYTAC_NHAYSO
                    Import_DCM_QUYTAC_NHAYSO import_DCM_QUYTAC_NHAYSO = new Import_DCM_QUYTAC_NHAYSO();
                    //import_DCM_QUYTAC_NHAYSO.ExportData(import_SoVanBan.GetDcm_SoVanBans(), dcm_type);

                    // Import to DCM_QUYTAC_NHAYSO
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to DCM_QUYTAC_NHAYSO ..."));
                    query = string.Format(Constants.sql_insert_DCM_QUYTAC_NHAYSO, configs.Schema);
                    //import_DCM_QUYTAC_NHAYSO.insert_DCM_QUYTAC_NHAYSO(oracleConnection, query, import_DCM_QUYTAC_NHAYSO.GetDCM_QUYTAC_NHAYSOs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to DCM_QUYTAC_NHAYSO!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to DCM_QUYTAC_NHAYSO: " + import_DCM_QUYTAC_NHAYSO.GetDCM_QUYTAC_NHAYSOs().Count);
                    Console.WriteLine("Consumption of imported data to DCM_QUYTAC_NHAYSO: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                    if (chkBox_AppendData.Checked)
                    {
                        // Update seq to db
                        Common.UpdateSeqFromProcedure(oracleConnection, configs.Schema, list_seq);
                    }
                }

                // Do mat
                if (this.chkBox_Confidential.Checked)
                {

                }

                // Do khan
                if (this.chkBox_Priority.Checked)
                {

                }

                // Thong tin van ban di
                if (this.chkbox_OutGoing_Info.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting data from Postgres ..."));
                    Import_VanBan import_VanBan = new Import_VanBan();

                    import_VanBan.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_DOC.ToString());

                    string query = configs.IsUBND ? Constants.PROC_UBND_VBDI_INFO : Constants.PROC_SO_VBDI_INFO;
                    query += "(" + configs.Donvi_lay_du_lieu + ";" + configs.Year + ")";
                    if (chkBox_AppendData.Checked)
                    {
                        list_seq = Constants.SEQ_DCM_DOC_RELATION + ",";
                        list_seq += Constants.SEQ_FEM_FILE + ",";
                        list_seq += Constants.SEQ_DCM_ATTACH_FILE + ",";
                        Import_VanBan.SEQ_DCM_DOC_RELATION = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DOC_RELATION);
                        Import_VanBan.SEQ_FEM_FILE = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_FEM_FILE);
                        Import_VanBan.SEQ_DCM_ATTACH_FILE = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ATTACH_FILE);
                    }
                    else
                    {
                        Import_VanBan.SEQ_DCM_DOC_RELATION = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_DOC_RELATION);
                        Import_VanBan.SEQ_FEM_FILE = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.FEM_FILE);
                        Import_VanBan.SEQ_DCM_ATTACH_FILE = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_ATTACH_FILE);
                    }
                    
                    import_VanBan.exportdataFromPostgres(postgresConnection, configs, query, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    if (!import_VanBan.isExportError())
                    {
                        // Import to dcm_doc
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_Doc ..."));
                        query = string.Format(Constants.sql_insert_dcm_doc, configs.Schema);
                        import_VanBan.insert_Dcm_Doc(oracleConnection, query, import_VanBan.GetDcm_Docs());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_Doc!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_doc: " + import_VanBan.GetDcm_Docs().Count);
                        Console.WriteLine("Consumption of imported data to dcm_doc: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_doc_relation
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_Doc_Relation ..."));
                        import_VanBan.insert_Dcm_Doc_Relation(oracleConnection, configs, Constants.sql_insert_dcm_doc_relation, import_VanBan.GetDcm_Doc_Relations());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_Doc_Relation!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to Dcm_Doc_Relation: " + import_VanBan.GetDcm_Doc_Relations().Count);
                        Console.WriteLine("Consumption of imported data to dcm_doc_relation: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to fem_file
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to fem_file ..."));
                        import_VanBan.insert_fem_file(oracleConnection, configs, Constants.sql_insert_fem_file, import_VanBan.GetFem_Files());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to fem_file!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to fem_file: " + import_VanBan.GetFem_Files().Count);
                        Console.WriteLine("Consumption of imported data to fem_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_attach_file
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_attach_file ..."));
                        import_VanBan.insert_Dcm_Attach_File(oracleConnection, configs, Constants.sql_insert_dcm_attach_file, import_VanBan.GetDcm_Attach_Files());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_attach_file!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_attach_file: " + import_VanBan.GetDcm_Attach_Files().Count);
                        Console.WriteLine("Consumption of imported data to dcm_attach_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                        if (chkBox_AppendData.Checked)
                        {
                            // Update seq to db
                            Common.UpdateSeqFromProcedure(oracleConnection, configs.Schema, list_seq);
                        }
                    }
                }

                // Luong xu ly van ban di
                if (this.chkbox_OutGoing_Flow.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting outgoing doc flow data from Postgres ..."));
                    Import_VanBan_Flow import_Outgoing_Doc_Flow = new Import_VanBan_Flow();
                    string query = configs.IsUBND ? Constants.PROC_UBND_VBDI_FLOW : Constants.PROC_SO_VBDI_FLOW;
                    query += "(" + configs.Donvi_lay_du_lieu + ";" + configs.Year + ")";
                    //string.Format(configs.IsUBND ? Constants.sql_luong_xuly_vb_di : Constants.sql_so_luong_xuly_vbdi, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu, configs.Year);
                    if (chkBox_AppendData.Checked)
                    {
                        list_seq = Constants.SEQ_DCM_ACTIVITI_LOG + ",";
                        list_seq += Constants.SEQ_DCM_ASSIGN + ",";
                        list_seq += Constants.SEQ_DCM_DONVI_NHAN + ",";
                        Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ACTIVITI_LOG);
                        Import_VanBan_Flow.SEQ_DCM_ASSIGN = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ASSIGN);
                        Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DONVI_NHAN);
                    }
                    else
                    {
                        Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_ACTIVITI_LOG);
                        Import_VanBan_Flow.SEQ_DCM_ASSIGN = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_ASSIGN);
                        Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_DONVI_NHAN);
                    }

                    import_Outgoing_Doc_Flow.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_DOC.ToString());

                    import_Outgoing_Doc_Flow.exportdataFromPostgres(postgresConnection, CommandType.StoredProcedure, query, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc flow data from Postgres!"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported outgoing doc flow data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // DCM_ASSIGN
                    //Import_DCM_ASSIGN import_DCM_ASSIGN = new Import_DCM_ASSIGN();
                    //import_DCM_ASSIGN.ExportData(import_Outgoing_Doc_Flow.GetDcm_Activiti_Logs());

                    if (!import_Outgoing_Doc_Flow.isExportError())
                    {
                        // Import to dcm_activiti_log
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_activiti_log ..."));
                        import_Outgoing_Doc_Flow.insert_Dcm_Activiti_Log(oracleConnection, configs, Constants.sql_insert_dcm_activiti_log, import_Outgoing_Doc_Flow.GetDcm_Activiti_Logs());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_activiti_log!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_activiti_log: " + import_Outgoing_Doc_Flow.GetDcm_Activiti_Logs().Count);
                        Console.WriteLine("Consumption of imported data to dcm_activiti_log: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_assign
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_assign ..."));
                        import_Outgoing_Doc_Flow.insert_Dcm_Assign(oracleConnection, configs, Constants.sql_insert_dcm_assign, import_Outgoing_Doc_Flow.GetDcm_Assigns());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_assign!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_assign: " + import_Outgoing_Doc_Flow.GetDcm_Assigns().Count);
                        Console.WriteLine("Consumption of imported data to dcm_assign: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_donvi_nhan
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_donvi_nhan ..."));
                        import_Outgoing_Doc_Flow.insert_Dcm_Donvi_Nhan(oracleConnection, configs, Constants.sql_insert_dcm_donvi_nhan, import_Outgoing_Doc_Flow.GetDcm_Donvi_Nhans());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_donvi_nhan!"));
                        timer.Stop();
                        if (chkBox_AppendData.Checked)
                        {
                            // Update seq to db
                            Common.UpdateSeqFromProcedure(oracleConnection, configs.Schema, list_seq);
                        }
                        Console.WriteLine("Total imported data to dcm_donvi_nhan: " + import_Outgoing_Doc_Flow.GetDcm_Donvi_Nhans().Count);
                        Console.WriteLine("Consumption of imported data to dcm_donvi_nhan: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                    } else
                    {
                        Console.WriteLine("=== Exported outgoing doc flow data error ===");
                    }
                }

                // Log xu ly van ban di
                if (this.chkbox_OutGoing_Log.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting outgoing doc log data from Postgres ..."));
                    Import_VanBan_Log import_VanBan_Log = new Import_VanBan_Log();
                    string query = configs.IsUBND ? Constants.PROC_UBND_VBDI_LOG : Constants.PROC_SO_VBDI_LOG;
                    query += "(" + configs.Donvi_lay_du_lieu + ";" + configs.Year + ")";
                    //string.Format(configs.IsUBND ? Constants.sql_log_xuly_vb_di : Constants.sql_so_log_xuly_vbdi, configs.Donvi_lay_du_lieu, configs.Year);
                    if (chkBox_AppendData.Checked)
                    {
                        list_seq = Constants.SEQ_DCM_LOG;
                        Import_VanBan_Log.SEQ_DCM_LOG = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_LOG);
                    }
                    else
                    {
                        Import_VanBan_Log.SEQ_DCM_LOG = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_LOG);
                    }

                    import_VanBan_Log.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_DOC.ToString());

                    import_VanBan_Log.exportdataFromPostgres(postgresConnection, CommandType.StoredProcedure, query, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc log data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total incoming doc exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported outgoing doc log data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    if (!import_VanBan_Log.isExportError())
                    {
                        // Import to dcm_log
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_log ..."));
                        import_VanBan_Log.insert_Dcm_Log(oracleConnection, configs, Constants.sql_insert_dcm_log, import_VanBan_Log.getDcm_Logs());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_log!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_log: " + import_VanBan_Log.getDcm_Logs().Count);
                        Console.WriteLine("Consumption of imported data to dcm_log: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_log_read
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_log_read ..."));
                        import_VanBan_Log.insert_Dcm_Log(oracleConnection, configs, Constants.sql_insert_dcm_log_read, import_VanBan_Log.getDcm_Log_Reads());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_log_read!"));
                        timer.Stop();
                        if (chkBox_AppendData.Checked)
                        {
                            // Update seq to db
                            Common.UpdateSeqFromProcedure(oracleConnection, configs.Schema, list_seq);
                        }
                        Console.WriteLine("Total imported data to dcm_log_read: " + import_VanBan_Log.getDcm_Log_Reads().Count);
                        Console.WriteLine("Consumption of imported data to dcm_log_read: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                    }
                    else
                    {
                        Console.WriteLine("=== Exported outgoing doc log data error ===");
                    }
                }

                // Thong tin van ban den
                if (this.chkbox_InComing_Info.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting incoming doc data from Postgres ..."));
                    string query = configs.IsUBND ? Constants.PROC_UBND_VBDEN_INFO : Constants.PROC_SO_VBDEN_INFO;
                    query += "(" + configs.Donvi_lay_du_lieu + ";" + configs.Year + ")";
                    //string.Format(configs.IsUBND ? Constants.sql_thongtin_vb_den : Constants.sql_so_thongtin_vbden, configs.Donvi_lay_du_lieu, configs.Year);
                    Import_VanBan import_VanBan = new Import_VanBan();

                    import_VanBan.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_DOC.ToString());

                    if (chkBox_AppendData.Checked)
                    {
                        list_seq = Constants.SEQ_DCM_DOC_RELATION + ",";
                        list_seq += Constants.SEQ_FEM_FILE + ",";
                        list_seq += Constants.SEQ_DCM_ATTACH_FILE + ",";
                        list_seq += Constants.SEQ_DCM_TRACK + ",";
                        Import_VanBan.SEQ_DCM_DOC_RELATION = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DOC_RELATION);
                        Import_VanBan.SEQ_FEM_FILE = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_FEM_FILE);
                        Import_VanBan.SEQ_DCM_ATTACH_FILE = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ATTACH_FILE);
                        Import_VanBan.SEQ_DCM_TRACK = Common.GetCurrentSeq(oracleConnection, "CLOUD_ADMIN_DEV_BLU_2.", Constants.SEQ_DCM_TRACK);
                    }
                    else
                    {
                        Import_VanBan.SEQ_DCM_DOC_RELATION = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_DOC_RELATION);
                        Import_VanBan.SEQ_FEM_FILE = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.FEM_FILE);
                        Import_VanBan.SEQ_DCM_ATTACH_FILE = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_ATTACH_FILE);
                        Import_VanBan.SEQ_DCM_TRACK = Common.GetMaxID(oracleConnection, "CLOUD_ADMIN_DEV_BLU_2.", Common.TABLE.DCM_TRACK);
                    }                    
                    import_VanBan.exportdataFromPostgres(postgresConnection, configs, query, Common.VB_TYPE.VB_DEN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported incoming doc data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total incoming doc exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported incoming doc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    if (!import_VanBan.isExportError())
                    {
                        // Import to dcm_doc
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to Dcm_Doc ..."));
                        query = string.Format(Constants.sql_insert_dcm_doc, configs.Schema);
                        import_VanBan.insert_Dcm_Doc(oracleConnection, query, import_VanBan.GetDcm_Docs());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to Dcm_Doc!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_doc: " + import_VanBan.GetDcm_Docs().Count);
                        Console.WriteLine("Consumption of imported data to dcm_doc: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_doc_relation
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to Dcm_Doc_Relation ..."));
                        import_VanBan.insert_Dcm_Doc_Relation(oracleConnection, configs, Constants.sql_insert_dcm_doc_relation, import_VanBan.GetDcm_Doc_Relations());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to Dcm_Doc_Relation!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to Dcm_Doc_Relation: " + import_VanBan.GetDcm_Doc_Relations().Count);
                        Console.WriteLine("Consumption of imported data to dcm_doc_relation: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to fem_file
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to fem_file ..."));
                        import_VanBan.insert_fem_file(oracleConnection, configs, Constants.sql_insert_fem_file, import_VanBan.GetFem_Files());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to fem_file!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to fem_file: " + import_VanBan.GetFem_Files().Count);
                        Console.WriteLine("Consumption of imported data to fem_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_attach_file
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_attach_file ..."));
                        import_VanBan.insert_Dcm_Attach_File(oracleConnection, configs, Constants.sql_insert_dcm_attach_file, import_VanBan.GetDcm_Attach_Files());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_attach_file!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_attach_file: " + import_VanBan.GetDcm_Attach_Files().Count);
                        Console.WriteLine("Consumption of imported data to dcm_attach_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_track
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_track ..."));
                        import_VanBan.insert_Dcm_Track(oracleConnection, configs, Constants.sql_insert_dcm_track, import_VanBan.GetDcm_Tracks());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_track!"));
                        timer.Stop();
                        if (chkBox_AppendData.Checked)
                        {
                            // Update seq to db
                            Common.UpdateSeqFromProcedure(oracleConnection, configs.Schema, list_seq);
                        }
                        Console.WriteLine("Total imported data to dcm_track: " + import_VanBan.GetDcm_Tracks().Count);
                        Console.WriteLine("Consumption of imported data to dcm_track: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                    }
                }

                // Luong xu ly van ban den
                if (this.chkbox_InComing_Flow.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting incoming doc flow data from Postgres ..."));
                    Import_VanBan_Flow import_Incoming_Doc_Flow = new Import_VanBan_Flow();
                    string query = configs.IsUBND ? Constants.PROC_UBND_VBDEN_FLOW : Constants.PROC_SO_VBDEN_FLOW;
                    query += "(" + configs.Donvi_lay_du_lieu + ";" + configs.Year + ")";
                    if (chkBox_AppendData.Checked)
                    {
                        list_seq = Constants.SEQ_DCM_ACTIVITI_LOG + ",";
                        list_seq += Constants.SEQ_DCM_ASSIGN + ",";
                        list_seq += Constants.SEQ_DCM_DONVI_NHAN + ",";
                        Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ACTIVITI_LOG);
                        Import_VanBan_Flow.SEQ_DCM_ASSIGN = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ASSIGN);
                        Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DONVI_NHAN);
                    }
                    else
                    {
                        Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_ACTIVITI_LOG);
                        Import_VanBan_Flow.SEQ_DCM_ASSIGN = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_ASSIGN);
                        Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_DONVI_NHAN);
                    }

                    import_Incoming_Doc_Flow.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_DOC.ToString());

                    import_Incoming_Doc_Flow.exportdataFromPostgres(postgresConnection, CommandType.StoredProcedure, query, Common.VB_TYPE.VB_DEN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc flow data from Postgres!"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported incoming doc flow data: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                    
                    if (!import_Incoming_Doc_Flow.isExportError())
                    {
                        // Import to dcm_activiti_log
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_activiti_log ..."));
                        import_Incoming_Doc_Flow.insert_Dcm_Activiti_Log(oracleConnection, configs, Constants.sql_insert_dcm_activiti_log, import_Incoming_Doc_Flow.GetDcm_Activiti_Logs());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_activiti_log!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_activiti_log: " + import_Incoming_Doc_Flow.GetDcm_Activiti_Logs().Count);
                        Console.WriteLine("Consumption of imported data to dcm_activiti_log: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_assign
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_assign ..."));
                        import_Incoming_Doc_Flow.insert_Dcm_Assign(oracleConnection, configs, Constants.sql_insert_dcm_assign, import_Incoming_Doc_Flow.GetDcm_Assigns());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_assign!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_assign: " + import_Incoming_Doc_Flow.GetDcm_Assigns().Count);
                        Console.WriteLine("Consumption of imported data to dcm_assign: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_donvi_nhan
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_donvi_nhan ..."));
                        import_Incoming_Doc_Flow.insert_Dcm_Donvi_Nhan(oracleConnection, configs, Constants.sql_insert_dcm_donvi_nhan, import_Incoming_Doc_Flow.GetDcm_Donvi_Nhans());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_donvi_nhan!"));
                        timer.Stop();
                        if (chkBox_AppendData.Checked)
                        {
                            // Update seq to db
                            Common.UpdateSeqFromProcedure(oracleConnection, configs.Schema, list_seq);
                        }
                        Console.WriteLine("Total imported data to dcm_donvi_nhan: " + import_Incoming_Doc_Flow.GetDcm_Donvi_Nhans().Count);
                        Console.WriteLine("Consumption of imported data to dcm_donvi_nhan: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                    }
                    else
                    {
                        Console.WriteLine("=== Exported incoming doc flow data error ===");
                    }
                }

                // Log xu ly van ban den
                if (this.chkbox_InComing_Log.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting incoming doc log data from Postgres ..."));
                    Import_VanBan_Log import_VanBan_Log = new Import_VanBan_Log();
                    string query = configs.IsUBND ? Constants.PROC_UBND_VBDEN_LOG : Constants.PROC_SO_VBDEN_LOG;
                    query += "(" + configs.Donvi_lay_du_lieu + ";" + configs.Year + ")";
                    //string.Format(configs.IsUBND ? Constants.sql_log_xuly_vb_den : Constants.sql_so_log_xuly_vbden, configs.Donvi_lay_du_lieu, configs.Year);
                    if (chkBox_AppendData.Checked)
                    {
                        list_seq = Constants.SEQ_DCM_LOG;
                        Import_VanBan_Log.SEQ_DCM_LOG = Common.GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_LOG);
                    }
                    else
                    {
                        Import_VanBan_Log.SEQ_DCM_LOG = Common.GetMaxID(oracleConnection, configs.Schema, Common.TABLE.DCM_LOG);
                    }

                    import_VanBan_Log.StandardizedData(oracleConnection, configs.Schema, Common.TABLE.DCM_DOC.ToString());

                    import_VanBan_Log.exportdataFromPostgres(postgresConnection, CommandType.StoredProcedure, query, Common.VB_TYPE.VB_DEN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported incoming doc data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total incoming doc exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported incoming doc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    if (!import_VanBan_Log.isExportError())
                    {
                        // Import to dcm_log
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_log ..."));
                        import_VanBan_Log.insert_Dcm_Log(oracleConnection, configs, Constants.sql_insert_dcm_log, import_VanBan_Log.getDcm_Logs());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_log!"));
                        timer.Stop();
                        Console.WriteLine("Total imported data to dcm_log: " + import_VanBan_Log.getDcm_Logs().Count);
                        Console.WriteLine("Consumption of imported data to dcm_log: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                        // Import to dcm_log_read
                        timer.Reset();
                        timer.Start();
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_log_read ..."));
                        import_VanBan_Log.insert_Dcm_Log(oracleConnection, configs, Constants.sql_insert_dcm_log_read, import_VanBan_Log.getDcm_Log_Reads());
                        txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_log_read!"));
                        timer.Stop();
                        if (chkBox_AppendData.Checked)
                        {
                            // Update seq to db
                            Common.UpdateSeqFromProcedure(oracleConnection, configs.Schema, list_seq);
                        }
                        Console.WriteLine("Total imported data to dcm_log_read: " + import_VanBan_Log.getDcm_Log_Reads().Count);
                        Console.WriteLine("Consumption of imported data to dcm_log_read: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                    }
                    else
                    {
                        Console.WriteLine("=== Exported incoming doc log data error ===");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {                
                // Close connection
                postgresConnection.Close();
                oracleConnection.Close();
                //Connection.getInstance().CloseConnection();
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Done"));
            }
        }

        private void CollectConfigs()
        {
            configs.Schema = cb_Schemas.SelectedValue.ToString();
            configs.Year = Common.GetExportedDataYears(txt_StartYear.Text.Trim(), txt_EndYear.Text.Trim());
            configs.Donvi_lay_du_lieu = int.Parse(cbBox_Donvi.SelectedValue.ToString());
            configs.IsUBND = configs.Donvi_lay_du_lieu == 3528;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_Schema_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(cb_Schemas.SelectedValue.ToString());
        }

        private void cbBox_Donvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)cbBox_Donvi.SelectedItem;
            configs.Old_schema = row["schema"].ToString();
            Console.WriteLine(configs.Old_schema);
        }

        private void cb_Schemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(cb_Schemas.SelectedValue);
        }
    }
}
