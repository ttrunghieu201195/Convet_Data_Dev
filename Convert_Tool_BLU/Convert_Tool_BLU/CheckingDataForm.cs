using Convert_Data.Controller.Verify;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Convert_Data
{
    public partial class CheckingDataForm : Form
    {
        private Configs configs;
        CheckingConvertedData checkingConvertedData;
        public CheckingDataForm(Configs configs)
        {
            InitializeComponent();
            this.configs = configs;
            checkingConvertedData = new CheckingConvertedData();
        }

        private void CheckingDataForm_Load(object sender, EventArgs e)
        {
            /*            CheckingConvertedData checkingConvertedData = new CheckingConvertedData();
                        if (checkingConvertedData.Prepare_Data())
                        {
                            Console.WriteLine("=====Done=====");
                        } else
                        {
                            Console.WriteLine("=====Wtf====");
                        }*/

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void verify_Data(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => CollectData());
            thread.Start();
        }

        private void CollectData()
        {
            try
            {
                OracleConnection bluConnection = Connection.getInstance().GetBLUConnection();
                OracleConnection devConnection = Connection.getInstance().GetOracleConnection();
                long countBlu = 0;
                long countDev = 0;

                txtProgress_Checking.Invoke(new Action(() => txtProgress_Checking.Text = "Collecting Data..."));

                // DCM_DOC
                string query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.DCM_DOC);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_Doc.Invoke(new Action(() => txtBlu_Doc.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_Doc.Invoke(new Action(() => txtDev_Doc.Text = countDev.ToString()));
                txtMiss_Doc.Invoke(new Action(() => txtMiss_Doc.Text = (countDev - countBlu).ToString()));


                // DCM_DOC_RELATION
                query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.DCM_DOC_RELATION);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_DocRelation.Invoke(new Action(() => txtBlu_DocRelation.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_DocRelation.Invoke(new Action(() => txtDev_DocRelation.Text = countDev.ToString()));
                txtMiss_DocRelation.Invoke(new Action(() => txtMiss_DocRelation.Text = (countDev - countBlu).ToString()));

                // FEM_FILE
                query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.FEM_FILE);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_Femfile.Invoke(new Action(() => txtBlu_Femfile.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_Femfile.Invoke(new Action(() => txtDev_Femfile.Text = countDev.ToString()));
                txtMiss_Femfile.Invoke(new Action(() => txtMiss_Femfile.Text = (countDev - countBlu).ToString()));

                // DCM_ATTACH_FILE
                query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.DCM_ATTACH_FILE);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_Attachfile.Invoke(new Action(() => txtBlu_Attachfile.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_Attachfile.Invoke(new Action(() => txtDev_Attachfile.Text = countDev.ToString()));
                txtMiss_Attachfile.Invoke(new Action(() => txtMiss_Attachfile.Text = (countDev - countBlu).ToString()));

                // DCM_ACTIVITI_LOG
                query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.DCM_ACTIVITI_LOG);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_ActivitiLog.Invoke(new Action(() => txtBlu_ActivitiLog.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_ActivitiLog.Invoke(new Action(() => txtDev_ActivitiLog.Text = countDev.ToString()));
                txtMiss_ActivitiLog.Invoke(new Action(() => txtMiss_ActivitiLog.Text = (countDev - countBlu).ToString()));

                // DCM_ASSIGN
                query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.DCM_ASSIGN);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_Assign.Invoke(new Action(() => txtBlu_Assign.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_Assign.Invoke(new Action(() => txtDev_Assign.Text = countDev.ToString()));
                txtMiss_Assign.Invoke(new Action(() => txtMiss_Assign.Text = (countDev - countBlu).ToString()));

                // DCM_DONVI_NHAN
                query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.DCM_DONVI_NHAN);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_DonviNhan.Invoke(new Action(() => txtBlu_DonviNhan.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_DonviNhan.Invoke(new Action(() => txtDev_DonviNhan.Text = countDev.ToString()));
                txtMiss_DonviNhan.Invoke(new Action(() => txtMiss_DonviNhan.Text = (countDev - countBlu).ToString()));

                // DCM_LOG
                query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.DCM_LOG);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_Log.Invoke(new Action(() => txtBlu_Log.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_Log.Invoke(new Action(() => txtDev_Log.Text = countDev.ToString()));
                txtMiss_Log.Invoke(new Action(() => txtMiss_Log.Text = (countDev - countBlu).ToString()));

                // DCM_LOG_READ
                query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, configs.Old_schema, Common.TABLE.DCM_LOG_READ);
                countBlu = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                txtBlu_LogRead.Invoke(new Action(() => txtBlu_LogRead.Text = countBlu.ToString()));
                countDev = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                txtDev_LogRead.Invoke(new Action(() => txtDev_LogRead.Text = countDev.ToString()));
                txtMiss_LogRead.Invoke(new Action(() => txtMiss_LogRead.Text = (countDev - countBlu).ToString()));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                txtProgress_Checking.Invoke(new Action(() => txtProgress_Checking.Text = "Done"));
            }
        }

        private void btn_PrepareData_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => PrepareData());
            thread.Start();
        }

        private void PrepareData()
        {
            OracleConnection bluConnection = Connection.getInstance().GetBLUConnection();
            OracleConnection devConnection = Connection.getInstance().GetOracleConnection();
            txtProgress_Checking.Invoke(new Action(() => txtProgress_Checking.Text = "Deleting old data..."));
            if (Common.DeleteOldDataFromTable(devConnection))
            {
                foreach(string table in Common.table_arr)
                {
                    string query = string.Format(Constants.SQL_GET_CONVERTED_DATA, configs.Old_schema, table);
                    List<long> data = checkingConvertedData.GetDataFromTable(Connection.getInstance().GetBLUConnection(), query);
                    query = string.Format(Constants.SQL_INSERT_CONVERTED_DATA, table);
                    bool result = checkingConvertedData.ImportDataToTable(Connection.getInstance().GetOracleConnection(), query, data);
                    Console.WriteLine("Copied data from " + table + ":" + result);
                }
            }
        }
    }
}
