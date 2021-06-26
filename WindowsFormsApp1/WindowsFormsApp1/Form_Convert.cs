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
using WindowsFormsApp1.Controller;

namespace WindowsFormsApp1
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

                // So van ban
                if (this.chkBox_Book.Checked)
                {

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
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting data from Postgres ..."));
                    Import_VanBan.exportdataFromPostgres(postgresConnection, configs, Constants.sql_thongtin_vb_di, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_doc
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_Doc ..."));
                    //Import_VanBan.insert_Dcm_Doc(oracleConnection, configs, Constants.sql_insert_dcm_doc, Import_VanBan.getDcm_Docs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_Doc!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_doc: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of imported data to dcm_doc: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_doc_relation
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to Dcm_Doc_Relation ..."));
                    Import_VanBan.insert_Dcm_Doc_Relation(oracleConnection, configs, Constants.sql_insert_dcm_doc_relation, Import_VanBan.getDcm_Doc_Relations());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to Dcm_Doc_Relation!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to Dcm_Doc_Relation: " + Import_VanBan.getDcm_Doc_Relations().Count);
                    Console.WriteLine("Consumption of imported data to dcm_doc_relation: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to fem_file
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to fem_file ..."));
                    //Import_VanBan.insert_fem_file(oracleConnection, configs, Constants.sql_insert_fem_file, Import_VanBan.getFem_Files());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to fem_file!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to fem_file: " + Import_VanBan.getFem_Files().Count);
                    Console.WriteLine("Consumption of imported data to fem_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_attach_file
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_attach_file ..."));
                    //Import_VanBan.insert_Dcm_Attach_File(oracleConnection, configs, Constants.sql_insert_dcm_attach_file, Import_VanBan.getDcm_Attach_Files());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_attach_file!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_attach_file: " + Import_VanBan.getDcm_Attach_Files().Count);
                    Console.WriteLine("Consumption of imported data to dcm_attach_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Luong xu ly van ban di
                if (this.chkbox_OutGoing_Flow.Checked)
                {
                    // Export data
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting outgoing doc flow data from Postgres ..."));
                    Import_VanBan_Flow import_Outgoing_Doc_Flow = new Import_VanBan_Flow();
                    import_Outgoing_Doc_Flow.exportdataFromPostgres(postgresConnection, Constants.sql_luong_xuly_vb_di, Common.VB_TYPE.VB_DI);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc flow data from Postgres!"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported outgoing doc flow data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_activiti_log
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_activiti_log ..."));
                    import_Outgoing_Doc_Flow.insert_Dcm_Activiti_Log(oracleConnection, configs, Constants.sql_insert_dcm_activiti_log, import_Outgoing_Doc_Flow.GetDcm_Activiti_Logs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_activiti_log!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_activiti_log: " + import_Outgoing_Doc_Flow.GetDcm_Activiti_Logs());
                    Console.WriteLine("Consumption of imported data to dcm_activiti_log: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_assign
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_assign ..."));
                    //import_Outgoing_Doc_Flow.insert_Dcm_Assign(oracleConnection, configs, Constants.sql_insert_dcm_assign, import_Outgoing_Doc_Flow.GetDcm_Assigns());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_assign!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_assign: " + import_Outgoing_Doc_Flow.GetDcm_Assigns());
                    Console.WriteLine("Consumption of imported data to dcm_assign: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_donvi_nhan
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_donvi_nhan ..."));
                    //import_Outgoing_Doc_Flow.insert_Dcm_Donvi_Nhan(oracleConnection, configs, Constants.sql_insert_dcm_donvi_nhan, import_Outgoing_Doc_Flow.GetDcm_Donvi_Nhans());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_donvi_nhan!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_donvi_nhan: " + import_Outgoing_Doc_Flow.GetDcm_Donvi_Nhans());
                    Console.WriteLine("Consumption of imported data to dcm_donvi_nhan: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Log xu ly van ban di
                if (this.chkbox_OutGoing_Log.Checked)
                {

                }

                // Thong tin van ban den
                if (this.chkbox_InComing_Info.Checked)
                {
                    // Export data
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting incoming doc data from Postgres ..."));
                    Import_VanBan.exportdataFromPostgres(postgresConnection, configs, Constants.sql_thongtin_vb_den, Common.VB_TYPE.VB_DEN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported incoming doc data from Postgres!"));
                    timer.Stop();
                    //Console.WriteLine("Total incoming doc exported data: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of exported incoming doc data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_doc
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to Dcm_Doc ..."));
                    //Import_VanBan.insert_Dcm_Doc(oracleConnection, configs, Constants.sql_insert_dcm_doc, Import_VanBan.getDcm_Docs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to Dcm_Doc!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_doc: " + Import_VanBan.getDcm_Docs().Count);
                    Console.WriteLine("Consumption of imported data to dcm_doc: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_doc_relation
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to Dcm_Doc_Relation ..."));
                    Import_VanBan.insert_Dcm_Doc_Relation(oracleConnection, configs, Constants.sql_insert_dcm_doc_relation, Import_VanBan.getDcm_Doc_Relations());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to Dcm_Doc_Relation!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to Dcm_Doc_Relation: " + Import_VanBan.getDcm_Doc_Relations().Count);
                    Console.WriteLine("Consumption of imported data to dcm_doc_relation: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to fem_file
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to fem_file ..."));
                    //Import_VanBan.insert_fem_file(oracleConnection, configs, Constants.sql_insert_fem_file, Import_VanBan.getFem_Files());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to fem_file!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to fem_file: " + Import_VanBan.getFem_Files().Count);
                    Console.WriteLine("Consumption of imported data to fem_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_attach_file
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_attach_file ..."));
                    //Import_VanBan.insert_Dcm_Attach_File(oracleConnection, configs, Constants.sql_insert_dcm_attach_file, Import_VanBan.getDcm_Attach_Files());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_attach_file!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_attach_file: " + Import_VanBan.getDcm_Attach_Files().Count);
                    Console.WriteLine("Consumption of imported data to dcm_attach_file: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_track
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
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting incoming doc flow data from Postgres ..."));
                    Import_VanBan_Flow import_Incoming_Doc_Flow = new Import_VanBan_Flow();
                    import_Incoming_Doc_Flow.exportdataFromPostgres(postgresConnection, Constants.sql_luong_xuly_vb_den, Common.VB_TYPE.VB_DEN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported outgoing doc flow data from Postgres!"));
                    timer.Stop();
                    Console.WriteLine("Consumption of exported incoming doc flow data: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_activiti_log
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting incoming doc to dcm_activiti_log ..."));
                    import_Incoming_Doc_Flow.insert_Dcm_Activiti_Log(oracleConnection, configs, Constants.sql_insert_dcm_activiti_log, import_Incoming_Doc_Flow.GetDcm_Activiti_Logs());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted incoming doc to dcm_activiti_log!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_activiti_log: " + import_Incoming_Doc_Flow.GetDcm_Activiti_Logs());
                    Console.WriteLine("Consumption of imported data to dcm_activiti_log: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_assign
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_assign ..."));
                    import_Incoming_Doc_Flow.insert_Dcm_Assign(oracleConnection, configs, Constants.sql_insert_dcm_assign, import_Incoming_Doc_Flow.GetDcm_Assigns());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_assign!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_assign: " + import_Incoming_Doc_Flow.GetDcm_Assigns());
                    Console.WriteLine("Consumption of imported data to dcm_assign: " + timer.ElapsedMilliseconds / 1000 + "(s)");

                    // Import to dcm_donvi_nhan
                    timer.Start();
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserting outgoing doc to dcm_donvi_nhan ..."));
                    import_Incoming_Doc_Flow.insert_Dcm_Donvi_Nhan(oracleConnection, configs, Constants.sql_insert_dcm_donvi_nhan, import_Incoming_Doc_Flow.GetDcm_Donvi_Nhans());
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Inserted outgoing doc to dcm_donvi_nhan!"));
                    timer.Stop();
                    Console.WriteLine("Total imported data to dcm_donvi_nhan: " + import_Incoming_Doc_Flow.GetDcm_Donvi_Nhans());
                    Console.WriteLine("Consumption of imported data to dcm_donvi_nhan: " + timer.ElapsedMilliseconds / 1000 + "(s)");
                }

                // Log xu ly van ban den
                if (this.chkbox_InComing_Log.Checked)
                {

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
    }
}
