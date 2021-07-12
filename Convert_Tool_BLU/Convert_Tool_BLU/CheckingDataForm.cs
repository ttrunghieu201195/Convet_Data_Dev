using Convert_Data.Controller.Verify;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Convert_Data
{
    public partial class CheckingDataForm : Form
    {
        public CheckingDataForm()
        {
            InitializeComponent();
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
            try
            {
                CheckingConvertedData checkingConvertedData = new CheckingConvertedData();
                OracleConnection bluConnection = Connection.getInstance().GetBLUConnection();
                OracleConnection devConnection = Connection.getInstance().GetOracleConnection();
                txtProgress_Checking.Invoke(new Action(() => txtProgress_Checking.Text = "Deleting old data..."));
                long count = 0;
                if (Common.DeleteOldDataFromTable(devConnection))
                {
                    txtProgress_Checking.Invoke(new Action(() => txtProgress_Checking.Text = "Collecting Data..."));
                    
                    // DCM_DOC
                    string query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.DCM_DOC);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_Doc.Invoke(new Action(() => txtBlu_Doc.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_Doc.Invoke(new Action(() => txtDev_Doc.Text = count.ToString()));
                    if (txtBlu_Doc.Text.Equals(txtDev_Doc.Text))
                    {
                        txtDev_Doc.Invoke(new Action(() => txtDev_Doc.Enabled = false));
                        txtBlu_Doc.Invoke(new Action(() => txtBlu_Doc.Enabled = false));
                    }

                    // DCM_DOC_RELATION
                    query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.DCM_DOC_RELATION);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_DocRelation.Invoke(new Action(() => txtBlu_DocRelation.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_DocRelation.Invoke(new Action(() => txtDev_DocRelation.Text = count.ToString()));

                    // FEM_FILE
                    query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.FEM_FILE);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_Femfile.Invoke(new Action(() => txtBlu_Femfile.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_Femfile.Invoke(new Action(() => txtDev_Femfile.Text = count.ToString()));

                    // DCM_ATTACH_FILE
                    query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.DCM_ATTACH_FILE);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_Attachfile.Invoke(new Action(() => txtBlu_Attachfile.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_Attachfile.Invoke(new Action(() => txtDev_Attachfile.Text = count.ToString()));

                    // DCM_ACTIVITI_LOG
                    query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.DCM_ACTIVITI_LOG);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_ActivitiLog.Invoke(new Action(() => txtBlu_ActivitiLog.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_ActivitiLog.Invoke(new Action(() => txtDev_ActivitiLog.Text = count.ToString()));

                    // DCM_ASSIGN
                    query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.DCM_ASSIGN);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_Assign.Invoke(new Action(() => txtBlu_Assign.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_Assign.Invoke(new Action(() => txtDev_Assign.Text = count.ToString()));

                    // DCM_DONVI_NHAN
                    query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.DCM_DONVI_NHAN);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_DonviNhan.Invoke(new Action(() => txtBlu_DonviNhan.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_DonviNhan.Invoke(new Action(() => txtDev_DonviNhan.Text = count.ToString()));

                    // DCM_LOG
                    query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.DCM_LOG);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_Log.Invoke(new Action(() => txtBlu_Log.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_Log.Invoke(new Action(() => txtDev_Log.Text = count.ToString()));

                    // DCM_LOG_READ
                    query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, Common.TABLE.DCM_LOG_READ);
                    count = checkingConvertedData.GetDataCountFromTable(bluConnection, query);
                    txtBlu_LogRead.Invoke(new Action(() => txtBlu_LogRead.Text = count.ToString()));
                    count = checkingConvertedData.GetDataCountFromTable(devConnection, query);
                    txtDev_LogRead.Invoke(new Action(() => txtDev_LogRead.Text = count.ToString()));
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } finally
            {
                txtProgress_Checking.Invoke(new Action(() => txtProgress_Checking.Text = "Done"));
            }
        }
    }
}
