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
        public form_Convert()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            postgresConnection = Connection.getInstance().GetPostgresConnection();
            //oracleConnection = new OracleConnection(Constants.oracle_connstring);
            //postgresConnection.Open();
            cbBox_Donvi.DataSource = Common.GetDanhMucDonvi(postgresConnection, Constants.sql_danhmuc_donvi_schema);
            cbBox_Donvi.DisplayMember = "name";
            cbBox_Donvi.ValueMember = "organizationid";
            cbBox_Donvi.SelectedIndex = cbBox_Donvi.FindString("UBND tỉnh Bạc Liêu");
            //Common.TestCallProcFromPostgres(postgresConnection);
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
            OracleConnection oracleConnection = Connection.getInstance().GetOracleConnection();
            // Initial timer
            Stopwatch timer = new Stopwatch();
            try
            {
                // Open connection
                /* if (oracleConnection.State != ConnectionState.Open)
                 {
                     oracleConnection.Open();
                 }*/

                // Initial seq from db
                Common.InitialSeqFromDB(oracleConnection, configs);

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
                    Common.DeleteDCM_DOC(oracleConnection, configs.Schema);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Deleted Docs!"));
                    timer.Stop();
                    //Console.WriteLine("Total exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported data: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // So van ban
                if (this.chkBox_Book.Checked)
                {
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting book data from Postgres..."));
                    Import_SoVanBan import_SoVanBan = new Import_SoVanBan();
                    DataTable dcm_type = import_SoVanBan.GetDcm_Type(oracleConnection, configs.Schema, Constants.sql_get_dcm_type);
                    string query = string.Format(Constants.sql_danhmuc_sovanban, configs.Donvi_lay_du_lieu);
                    import_SoVanBan.exportdataFromPostgres(postgresConnection, query, Common.VB_TYPE.NONE, dcm_type);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported book data from Postgres..."));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported book data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to Dcm_SoVanBan
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_SoVanBan ..."));
                    import_SoVanBan.insert_Dcm_SoVanBan(oracleConnection, configs, Constants.SQL_INSERT_SOVANBAN, import_SoVanBan.GetDcm_SoVanBans());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_SoVanBan!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to Dcm_SoVanBan: " + import_SoVanBan.GetDcm_SoVanBans().Count);
                    Console.WriteLine("Consumption of imported data to Dcm_SoVanBan: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to Dcm_SoVB_TemplateSinhSo
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_SoVB_TemplateSinhSo ..."));
                    import_SoVanBan.insert_Dcm_SoVB_TemplateSinhSo(oracleConnection, configs, Constants.sql_insert_Dcm_SoVB_TemplateSinhSo, import_SoVanBan.GetDCM_SOVB_TEMPLATESINHSOs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_SoVB_TemplateSinhSo!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to Dcm_SoVB_TemplateSinhSo: " + import_SoVanBan.GetDCM_SOVB_TEMPLATESINHSOs().Count);
                    Console.WriteLine("Consumption of imported data to Dcm_SoVB_TemplateSinhSo: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_activiti_log
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to DCM_QUYTAC_NHAYSO ..."));
                    import_SoVanBan.insert_DCM_QUYTAC_NHAYSO(oracleConnection, configs, Constants.sql_insert_DCM_QUYTAC_NHAYSO, import_SoVanBan.GetDCM_QUYTAC_NHAYSOs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to DCM_QUYTAC_NHAYSO!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to DCM_QUYTAC_NHAYSO: " + import_SoVanBan.GetDCM_QUYTAC_NHAYSOs().Count);
                    Console.WriteLine("Consumption of imported data to DCM_QUYTAC_NHAYSO: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Do mat
                if (this.chkBox_Confidential.Checked)
                {

                }

                // Do khan
                if (this.chkBox_Priority.Checked)
                {

                }

                // Hinh thuc
                if (this.chkBox_HinhThuc.Checked)
                {
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting hinh thuc data from Postgres ..."));
                    Import_DCM_TYPE import_DCM_TYPE = new Import_DCM_TYPE();
                    string query = string.Format(Constants.sql_danhmuc_dcm_type, configs.Donvi_lay_du_lieu);
                    import_DCM_TYPE.exportdataFromPostgres(postgresConnection, query, Common.VB_TYPE.NONE);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported hinh thuc data from Postgres !"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported hinh thuc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to DCM_TYPE
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting hinh thuc to dcm_linhvuc ..."));
                    import_DCM_TYPE.insert_Dcm_Type(oracleConnection, configs, Constants.SQL_INSERT_DCM_TYPE, import_DCM_TYPE.GetDCM_TYPEs());
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
                    import_LinhVuc.exportdataFromPostgres(postgresConnection, query, Common.VB_TYPE.NONE);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported linh vuc data from Postgres !"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported linh vuc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to DCM_LINHVUC
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting linh vuc to dcm_linhvuc ..."));
                    import_LinhVuc.insert_Dcm_Linhvuc(oracleConnection, configs, Constants.SQL_INSERT_DCM_LINHVUC, import_LinhVuc.GetDCM_LINHVUCs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted linh vuc to dcm_linhvuc !"));
                    timer.Stop();
                    Console.WriteLine("Consumption of imported data to dcm_linhvuc: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Thong tin van ban di
                if (this.chkbox_OutGoing_Info.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting data from Postgres ..."));
                    Import_VanBan import_VanBan = new Import_VanBan();
                    string query = string.Format(configs.IsUBND ? Constants.sql_thongtin_vb_di : Constants.sql_so_thongtin_vbdi, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu, configs.Year);
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
                        import_VanBan.insert_Dcm_Doc(oracleConnection, configs, Constants.sql_insert_dcm_doc, import_VanBan.GetDcm_Docs());
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
                    string query = string.Format(configs.IsUBND ? Constants.sql_luong_xuly_vb_di : Constants.sql_so_luong_xuly_vbdi, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu, configs.Year);
                    import_Outgoing_Doc_Flow.exportdataFromPostgres(postgresConnection, query, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc flow data from Postgres!"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported outgoing doc flow data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

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
                    string query = string.Format(configs.IsUBND ? Constants.sql_log_xuly_vb_di : Constants.sql_so_log_xuly_vbdi, configs.Donvi_lay_du_lieu, configs.Year);
                    import_VanBan_Log.exportdataFromPostgres(postgresConnection, query, Common.VB_TYPE.VB_DI);
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
                    string query = string.Format(configs.IsUBND ? Constants.sql_thongtin_vb_den : Constants.sql_so_thongtin_vbden, configs.Donvi_lay_du_lieu, configs.Year);
                    Import_VanBan import_VanBan = new Import_VanBan();
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
                        import_VanBan.insert_Dcm_Doc(oracleConnection, configs, Constants.sql_insert_dcm_doc, import_VanBan.GetDcm_Docs());
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
                    string query = "";
                    if (configs.IsUBND)
                    {
                        query = string.Format(Constants.sql_luong_xuly_vb_den, configs.Year, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu
                        , configs.Year, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu
                        , configs.Year, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu);
                    } else
                    {
                        query = string.Format(Constants.sql_so_luong_xuly_vbden, configs.Year, configs.Donvi_lay_du_lieu, configs.Year, configs.Donvi_lay_du_lieu
                        , configs.Year, configs.Donvi_lay_du_lieu);
                    }
                    import_Incoming_Doc_Flow.exportdataFromPostgres(postgresConnection, query, Common.VB_TYPE.VB_DEN);
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
                    string query = string.Format(configs.IsUBND ? Constants.sql_log_xuly_vb_den : Constants.sql_so_log_xuly_vbden, configs.Donvi_lay_du_lieu, configs.Year);
                    import_VanBan_Log.exportdataFromPostgres(postgresConnection, query, Common.VB_TYPE.VB_DEN);
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
                // Update seq to db
                Common.updateSeqFromProcedure(oracleConnection, configs);
                // Close connection
                /*postgresConnection.Close();
                oracleConnection.Close();*/
                Connection.getInstance().CloseConnection();
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Done"));
            }
        }

        private void CollectConfigs()
        {
            configs.Schema = txt_Schema.Text.Trim();
            configs.Year = Common.getExportedDataYears(txt_Year.Text.Trim());
            configs.Donvi_lay_du_lieu = int.Parse(cbBox_Donvi.SelectedValue.ToString());
            configs.IsUBND = configs.Donvi_lay_du_lieu == 3528;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txt_Schema_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_OpenVerifyForm(object sender, EventArgs e)
        {
            // Open connection
            CheckingDataForm checkingDataForm = new CheckingDataForm();
            checkingDataForm.Show();
        }

        private void cbBox_Donvi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView row = (DataRowView)cbBox_Donvi.SelectedItem;
            configs.Old_schema = row["schema"].ToString();
            Console.WriteLine(configs.Old_schema);
        }
    }
}
