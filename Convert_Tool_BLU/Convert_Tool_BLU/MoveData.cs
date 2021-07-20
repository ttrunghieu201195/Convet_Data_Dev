using Convert_Data.Controller;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convert_Data
{
    public partial class MoveData : Form
    {
        public MoveData()
        {
            InitializeComponent();
        }

        private void MoveData_Load(object sender, EventArgs e)
        {
            UpdateComboBox();
        }

        private void UpdateComboBox()
        {
            Dictionary<int, string> services = new Dictionary<int, string>
            {
                {0, "DEV" },
                {1, "BLU" },
            };
            cb_From_Server.DataSource = new BindingSource(services, null);
            cb_From_Server.DisplayMember = "Value";
            cb_From_Server.ValueMember = "Key";
            cb_From_Server.SelectedIndex = 0;

            cb_To_Server.DataSource = new BindingSource(services, null);
            cb_To_Server.DisplayMember = "Value";
            cb_To_Server.ValueMember = "Key";
            cb_To_Server.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int toServer = cb_To_Server.SelectedIndex;
            OracleConnection oracleConnection = Connection.getInstance().GetOracleConnection();
            
            string toSchema = txt_To_Schema.Text;
            string fromSchema = txt_From_Schema.Text;
            
            Thread thread = new Thread(() => executeAction(oracleConnection, fromSchema, toSchema)) ;
            thread.Start();
        }

        private void executeAction(OracleConnection oracleConnection, string fromSchema, string toSchema)
        {
            DeleteData(oracleConnection, toSchema);
            MovingData(oracleConnection, fromSchema, toSchema);
        }

        private void DeleteData(OracleConnection oracleConnection, string toSchema)
        {
            if (chkBox_Delete.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Delete Data"));
                if (chkBox_HinhThuc.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_TYPE));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_TYPE);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_TYPE));
                }

                if (chkBox_Linhvuc.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_LINHVUC));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_LINHVUC);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_LINHVUC));
                }

                if (chkBox_Book.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_SOVANBAN));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_SOVANBAN);
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_QUYTAC_NHAYSO);
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_SOVB_TEMPLATESINHSO);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_SOVANBAN));
                }

                if (chkBox_Confidential.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_CONFIDENTIAL));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_CONFIDENTIAL);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_CONFIDENTIAL));
                }

                if (chkBox_Priority.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_PRIORITY));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_PRIORITY);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_PRIORITY));
                }

                if (chkBox_DCM_DOC.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_DOC));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_DOC);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_DOC));
                }

                if (chkBox_DCM_DOC_RELATION.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_DOC_RELATION));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_DOC_RELATION);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_DOC_RELATION));
                }

                if (chkbox_FEM_FILE.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.FEM_FILE));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.FEM_FILE);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.FEM_FILE));
                }

                if (chkbox_DCM_ATTACH_FILE.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_ATTACH_FILE));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_ATTACH_FILE);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_ATTACH_FILE));
                }

                if (chkbox_DCM_ACTIVITI_LOG.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_ACTIVITI_LOG));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_ACTIVITI_LOG);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_ACTIVITI_LOG));
                }

                if (chkbox_DCM_ASSIGN.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_ASSIGN));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_ASSIGN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_ASSIGN));
                }

                if (chkBox_DCM_DONVI_NHAN.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_DONVI_NHAN));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_DONVI_NHAN);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_DONVI_NHAN));
                }

                if (chkBox_DCM_LOG.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_LOG));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_LOG);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_LOG));
                }

                if (chkBox_DCM_LOG_READ.Checked)
                {
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_LOG_READ));
                    Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_LOG_READ);
                    txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_LOG_READ));
                }

                Console.WriteLine("====Finished Deleting Data====");
            }
        }
        
        private void MovingData(OracleConnection oracleConnection, string fromSchema, string toSchema)
        {
            txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Moving Data"));
            string query = "";
            if (chkBox_HinhThuc.Checked)
            {
                Import_DCM_TYPE import_DCM_TYPE = new Import_DCM_TYPE();

                query = string.Format(Constants.SQL_SELECT_DCM_TYPE, fromSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting Data from " + Common.TABLE.DCM_TYPE));
                import_DCM_TYPE.ExportData(oracleConnection, query);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported Data from " + Common.TABLE.DCM_TYPE));

                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Importing Data to " + Common.TABLE.DCM_TYPE));
                import_DCM_TYPE.insert_Dcm_Type(oracleConnection, toSchema, Constants.SQL_INSERT_DCM_TYPE, import_DCM_TYPE.GetDCM_TYPEs());
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Imported Data to " + Common.TABLE.DCM_TYPE));
            }

            if (chkBox_Linhvuc.Checked)
            {
                Import_LinhVuc import_LinhVuc = new Import_LinhVuc();

                query = string.Format(Constants.SQL_SELECT_DCM_LINHVUC, fromSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exporting Data from " + Common.TABLE.DCM_LINHVUC));
                import_LinhVuc.ExportData(oracleConnection, query);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Exported Data from " + Common.TABLE.DCM_LINHVUC));

                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Importing Data to " + Common.TABLE.DCM_LINHVUC));
                import_LinhVuc.insert_Dcm_Linhvuc(oracleConnection, toSchema, Constants.SQL_INSERT_DCM_LINHVUC, import_LinhVuc.GetDCM_LINHVUCs());
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Imported Data to " + Common.TABLE.DCM_LINHVUC));
            }

            if (chkBox_Book.Checked)
            {
                Import_SoVanBan import_SoVanBan = new Import_SoVanBan();
                query = string.Format(Constants.SQL_SELECT__DCM_SOVANBAN, fromSchema);

                //import_SoVanBan.

                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_SOVANBAN));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_SOVANBAN);
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_QUYTAC_NHAYSO);
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_SOVB_TEMPLATESINHSO);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_SOVANBAN));
            }

            if (chkBox_Confidential.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_CONFIDENTIAL));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_CONFIDENTIAL);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_CONFIDENTIAL));
            }

            if (chkBox_Priority.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_PRIORITY));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_PRIORITY);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_PRIORITY));
            }

            if (chkBox_DCM_DOC.Checked)
            {
                Import_VanBan import_VanBan = new Import_VanBan();

                query = string.Format(Constants.SQL_SELECT_ALL_DATA, fromSchema, Common.TABLE.DCM_DOC);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_DOC));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_DOC);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_DOC));
            }

            if (chkBox_DCM_DOC_RELATION.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_DOC_RELATION));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_DOC_RELATION);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_DOC_RELATION));
            }

            if (chkbox_FEM_FILE.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.FEM_FILE));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.FEM_FILE);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.FEM_FILE));
            }

            if (chkbox_DCM_ATTACH_FILE.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_ATTACH_FILE));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_ATTACH_FILE);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_ATTACH_FILE));
            }

            if (chkbox_DCM_ACTIVITI_LOG.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_ACTIVITI_LOG));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_ACTIVITI_LOG);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_ACTIVITI_LOG));
            }

            if (chkbox_DCM_ASSIGN.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_ASSIGN));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_ASSIGN);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_ASSIGN));
            }

            if (chkBox_DCM_DONVI_NHAN.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_DONVI_NHAN));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_DONVI_NHAN);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_DONVI_NHAN));
            }

            if (chkBox_DCM_LOG.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_LOG));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_LOG);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_LOG));
            }

            if (chkBox_DCM_LOG_READ.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_LOG_READ));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_LOG_READ);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_LOG_READ));
            }
        }
    }
}
