using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convert_Data.Controller;
using WindowsFormsApp1.Controller;

namespace Convert_Data
{
    public partial class form_Convert : Form
    {
        Configs configs = new Configs();
        public form_Convert()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void run_Action(object sender, EventArgs e)
        {
            collectConfigs();
            if (configs.schema != String.Empty && configs.year != String.Empty)
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
            NpgsqlConnection postgresConnection = new NpgsqlConnection(Constants.postgres_connstring);
            OracleConnection oracleConnection = new OracleConnection(Constants.oracle_connstring);
            // Initial timer
            Stopwatch timer = new Stopwatch();
            try
            {
                // Open connection
                postgresConnection.Open();
                oracleConnection.Open();

                // Initial seq from db
                Common.InitialSeqFromDB(oracleConnection, configs);

                if (chkbox_deleteCategory.Checked)
                {
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Deleting Categories ..."));
                    Import_VanBan.exportdataFromPostgres(postgresConnection, configs, Constants.sql_thongtin_vb_di, Common.VB_TYPE.VB_DI);
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
                    Common.DeleteAllTableRelatedToDcmDoc(oracleConnection, configs.schema);
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
                    DataTable dcm_type = import_SoVanBan.GetDcm_Type(oracleConnection, configs.schema, Constants.sql_get_dcm_type);
                    import_SoVanBan.exportdataFromPostgres(postgresConnection, Constants.sql_danhmuc_sovanban, Common.VB_TYPE.NONE, dcm_type);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported book data from Postgres..."));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported book data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to Dcm_SoVanBan
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_SoVanBan ..."));
                    import_SoVanBan.insert_Dcm_SoVanBan(oracleConnection, configs, Constants.sql_insert_sovanban, import_SoVanBan.GetDcm_SoVanBans());
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

                }

                // Linh vuc
                if (this.chkBox_Linhvuc.Checked)
                {

                }

                // Thong tin van ban di
                if (this.chkbox_OutGoing_Info.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting data from Postgres ..."));
                    Import_VanBan.exportdataFromPostgres(postgresConnection, configs, Constants.sql_thongtin_vb_di, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_doc
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_Doc ..."));
                    Import_VanBan.insert_Dcm_Doc(oracleConnection, configs, Constants.sql_insert_dcm_doc, Import_VanBan.getDcm_Docs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_Doc!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_doc: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of imported data to dcm_doc: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_doc_relation
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_Doc_Relation ..."));
                    Import_VanBan.insert_Dcm_Doc_Relation(oracleConnection, configs, Constants.sql_insert_dcm_doc_relation, Import_VanBan.getDcm_Doc_Relations());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_Doc_Relation!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to Dcm_Doc_Relation: " + Import_VanBan.getDcm_Doc_Relations().Count);
                    Console.WriteLine("Consumption of imported data to dcm_doc_relation: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to fem_file
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to fem_file ..."));
                    Import_VanBan.insert_fem_file(oracleConnection, configs, Constants.sql_insert_fem_file, Import_VanBan.getFem_Files());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to fem_file!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to fem_file: " + Import_VanBan.getFem_Files().Count);
                    Console.WriteLine("Consumption of imported data to fem_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_attach_file
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_attach_file ..."));
                    Import_VanBan.insert_Dcm_Attach_File(oracleConnection, configs, Constants.sql_insert_dcm_attach_file, Import_VanBan.getDcm_Attach_Files());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_attach_file!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_attach_file: " + Import_VanBan.getDcm_Attach_Files().Count);
                    Console.WriteLine("Consumption of imported data to dcm_attach_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Luong xu ly van ban di
                if (this.chkbox_OutGoing_Flow.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting outgoing doc flow data from Postgres ..."));
                    Import_VanBan_Flow import_Outgoing_Doc_Flow = new Import_VanBan_Flow();
                    import_Outgoing_Doc_Flow.exportdataFromPostgres(postgresConnection, Constants.sql_luong_xuly_vb_di, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc flow data from Postgres!"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported outgoing doc flow data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

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
                }

                // Log xu ly van ban di
                if (this.chkbox_OutGoing_Log.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting outgoing doc log data from Postgres ..."));
                    Import_VanBan_Log import_VanBan_Log = new Import_VanBan_Log();
                    import_VanBan_Log.exportdataFromPostgres(postgresConnection, Constants.sql_log_xuly_vb_di, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total incoming doc exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported outgoing doc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

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

                // Thong tin van ban den
                if (this.chkbox_InComing_Info.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting incoming doc data from Postgres ..."));
                    Import_VanBan.exportdataFromPostgres(postgresConnection, configs, Constants.sql_thongtin_vb_den, Common.VB_TYPE.VB_DEN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported incoming doc data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total incoming doc exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported incoming doc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_doc
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to Dcm_Doc ..."));
                    Import_VanBan.insert_Dcm_Doc(oracleConnection, configs, Constants.sql_insert_dcm_doc, Import_VanBan.getDcm_Docs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to Dcm_Doc!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_doc: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of imported data to dcm_doc: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_doc_relation
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to Dcm_Doc_Relation ..."));
                    Import_VanBan.insert_Dcm_Doc_Relation(oracleConnection, configs, Constants.sql_insert_dcm_doc_relation, Import_VanBan.getDcm_Doc_Relations());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to Dcm_Doc_Relation!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to Dcm_Doc_Relation: " + Import_VanBan.getDcm_Doc_Relations().Count);
                    Console.WriteLine("Consumption of imported data to dcm_doc_relation: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to fem_file
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to fem_file ..."));
                    Import_VanBan.insert_fem_file(oracleConnection, configs, Constants.sql_insert_fem_file, Import_VanBan.getFem_Files());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to fem_file!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to fem_file: " + Import_VanBan.getFem_Files().Count);
                    Console.WriteLine("Consumption of imported data to fem_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_attach_file
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_attach_file ..."));
                    Import_VanBan.insert_Dcm_Attach_File(oracleConnection, configs, Constants.sql_insert_dcm_attach_file, Import_VanBan.getDcm_Attach_Files());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_attach_file!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_attach_file: " + Import_VanBan.getDcm_Attach_Files().Count);
                    Console.WriteLine("Consumption of imported data to dcm_attach_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_track
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_track ..."));
                    Import_VanBan.insert_Dcm_Track(oracleConnection, configs, Constants.sql_insert_dcm_track, Import_VanBan.getDcm_Tracks());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_track!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_track: " + Import_VanBan.getDcm_Tracks().Count);
                    Console.WriteLine("Consumption of imported data to dcm_track: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Luong xu ly van ban den
                if (this.chkbox_InComing_Flow.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting incoming doc flow data from Postgres ..."));
                    Import_VanBan_Flow import_Incoming_Doc_Flow = new Import_VanBan_Flow();
                    import_Incoming_Doc_Flow.exportdataFromPostgres(postgresConnection, Constants.sql_luong_xuly_vb_den, Common.VB_TYPE.VB_DEN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc flow data from Postgres!"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported incoming doc flow data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

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

                // Log xu ly van ban den
                if (this.chkbox_InComing_Log.Checked)
                {
                    // Export data
                    timer.Reset();
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting incoming doc log data from Postgres ..."));
                    Import_VanBan_Log import_VanBan_Log = new Import_VanBan_Log();
                    import_VanBan_Log.exportdataFromPostgres(postgresConnection, Constants.sql_log_xuly_vb_den, Common.VB_TYPE.VB_DEN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported incoming doc data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total incoming doc exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported incoming doc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Update seq to db
                Common.UpdateSeqToDB(oracleConnection, configs);
                // Close connection
                postgresConnection.Close();
                oracleConnection.Close();
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Done"));
            }
        }

        private void collectConfigs()
        {
            configs.schema = txt_Schema.Text.Trim();
            configs.year = Common.getExportedDataYears(txt_Year.Text.Trim());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
