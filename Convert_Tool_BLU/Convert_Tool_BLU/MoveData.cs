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
            int fromServer = cb_From_Server.SelectedIndex;
            /*OracleConnection devConnection = Connection.getInstance().GetOracleConnection();
            OracleConnection bluConnection = Connection.getInstance().GetBLUConnection();*/

            OracleConnection fromConnection = fromServer == 0 ? Connection.getInstance().GetOracleConnection() : Connection.getInstance().GetBLUConnection();
            OracleConnection toConnection = toServer == 0 ? Connection.getInstance().GetOracleConnection() : Connection.getInstance().GetBLUConnection();

            string toSchema = txt_To_Schema.Text;
            string fromSchema = txt_From_Schema.Text;
            
            Thread thread = new Thread(() => executeAction(fromConnection, toConnection, fromSchema, toSchema)) ;
            thread.Start();
        }

        private void executeAction(OracleConnection fromConnection, OracleConnection toConnection, string fromSchema, string toSchema)
        {
            if (chkBox_Delete.Checked)
            {
                DeleteData(toConnection, toSchema);
            }
            MovingData(fromConnection, toConnection, fromSchema, toSchema);
        }

        private void DeleteData(OracleConnection oracleConnection, string toSchema)
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

            if (chkbox_DCM_TRACK.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Deleting " + Common.TABLE.DCM_TRACK));
                Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_TRACK);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish Deleting " + Common.TABLE.DCM_TRACK));
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

        private void MovingData(OracleConnection fromConnection, OracleConnection toConnection, string fromSchema, string toSchema)
        {
            txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start Moving Data"));
            string query = "";
            if (chkBox_HinhThuc.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_TYPE"));
                Import_DCM_TYPE import_DCM_TYPE = new Import_DCM_TYPE();
                import_DCM_TYPE.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_TYPE"));
            }

            if (chkBox_Linhvuc.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_LINHVUC"));
                Import_LinhVuc import_LinhVuc = new Import_LinhVuc();
                import_LinhVuc.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_LINHVUC"));
            }

            if (chkBox_Book.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_SOVANBAN"));
                Import_SoVanBan import_SoVanBan = new Import_SoVanBan();
                //import_SoVanBan.
                import_SoVanBan.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_SOVANBAN"));

                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_QUYTAC_NHAYSO"));
                Import_DCM_QUYTAC_NHAYSO import_DCM_QUYTAC_NHAYSO = new Import_DCM_QUYTAC_NHAYSO();
                import_DCM_QUYTAC_NHAYSO.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_QUYTAC_NHAYSO"));

                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_SOVB_TEMPLATESINHSO"));
                Import_DCM_SOVB_TEMPLATESINHSO import_DCM_SOVB_TEMPLATESINHSO = new Import_DCM_SOVB_TEMPLATESINHSO();
                import_DCM_SOVB_TEMPLATESINHSO.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_SOVB_TEMPLATESINHSO"));
            }

            if (chkBox_Confidential.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_CONFIDENTIAL"));
                //Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_CONFIDENTIAL);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_CONFIDENTIAL"));
            }

            if (chkBox_Priority.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_PRIORITY"));
                //Common.DeleteTable(oracleConnection, toSchema, Common.TABLE.DCM_PRIORITY);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_PRIORITY"));
            }

            if (chkBox_DCM_DOC.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_DOC"));
                Import_VanBan import_VanBan = new Import_VanBan();
                query = string.Format(Constants.SQL_SELECT_DCM_DOC, fromSchema);
                import_VanBan.ExportData(fromConnection, query);

                query = string.Format(Constants.sql_insert_dcm_doc, toSchema);
                import_VanBan.insert_Dcm_Doc(toConnection, query, import_VanBan.GetDcm_Docs());
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_DOC"));
            }

            if (chkBox_DCM_DOC_RELATION.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_DOC_RELATION"));
                Import_DCM_DOC_RELATION import_DCM_DOC_RELATION = new Import_DCM_DOC_RELATION();
                import_DCM_DOC_RELATION.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_DOC_RELATION"));
            }

            if (chkbox_FEM_FILE.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving FEM_FILE"));
                Import_FEM_FILE import_FEM_FILE = new Import_FEM_FILE();
                import_FEM_FILE.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved FEM_FILE"));
            }

            if (chkbox_DCM_ATTACH_FILE.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_ATTACH_FILE"));
                Import_DCM_ATTACH_FILE import_DCM_ATTACH_FILE = new Import_DCM_ATTACH_FILE();
                import_DCM_ATTACH_FILE.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_ATTACH_FILE"));
            }

            if (chkbox_DCM_TRACK.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_TRACK"));
                Import_DCM_TRACK import_DCM_TRACK = new Import_DCM_TRACK();
                string fromSchema_ = "";
                string toSchema_ = "";
                if (fromConnection.ConnectionString.IndexOf("HOST = 10.163.8.36") > 0)
                {
                    fromSchema_ = Common.SCHEMA.CLOUD_ADMIN.ToString() + ".";
                } else if (fromConnection.ConnectionString.IndexOf("HOST = 123.31.40.153") > 0)
                {
                    fromSchema_ = Common.SCHEMA.CLOUD_ADMIN_DEV_BLU_2.ToString() + ".";
                }

                if (toConnection.ConnectionString.IndexOf("HOST = 123.31.40.153") > 0)
                {
                    toSchema_ = Common.SCHEMA.CLOUD_ADMIN_DEV_BLU_2.ToString() + ".";
                } else if (fromConnection.ConnectionString.IndexOf("HOST = 10.163.8.36") > 0)
                {
                    toSchema_ = Common.SCHEMA.CLOUD_ADMIN.ToString() + ".";
                }
                import_DCM_TRACK.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_TRACK"));
            }

            if (chkbox_DCM_ACTIVITI_LOG.Checked)
            {
                Import_VanBan_Flow import_VanBan_Flow = new Import_VanBan_Flow();
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_ACTIVITI_LOG"));
                import_VanBan_Flow.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_ACTIVITI_LOG"));
            }

            if (chkbox_DCM_ASSIGN.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_ASSIGN"));
                Import_DCM_ASSIGN import_DCM_ASSIGN = new Import_DCM_ASSIGN();
                import_DCM_ASSIGN.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_ASSIGN"));
            }

            if (chkBox_DCM_DONVI_NHAN.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_DONVI_NHAN"));
                Import_DCM_DONVI_NHAN import_DCM_DONVI_NHAN = new Import_DCM_DONVI_NHAN();
                import_DCM_DONVI_NHAN.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_DONVI_NHAN"));
            }

            if (chkBox_DCM_LOG.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_LOG"));
                Import_VanBan_Log import_VanBan_Log = new Import_VanBan_Log();
                import_VanBan_Log.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_LOG"));
            }

            if (chkBox_DCM_LOG_READ.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moving DCM_LOG_READ"));
                Import_DCM_LOG_READ import_DCM_LOG_READ = new Import_DCM_LOG_READ();
                import_DCM_LOG_READ.MoveData(fromConnection, toConnection, fromSchema, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Moved DCM_LOG_READ"));
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => CleanSchema());
            thread.Start();
        }

        private void CleanSchema()
        {
            OracleConnection oracleConnection = new OracleConnection(Constants.oracle_connstring);
            oracleConnection.Open();

            string toSchema = txt_To_Schema.Text;
            DeleteData(oracleConnection, toSchema);
            if (chkBox_UpdateSeq.Checked)
            {
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Start updating sequence"));
                //Common.UpdateSeqFromProcedure(oracleConnection, toSchema);
                txt_Progress.Invoke(new Action(() => txt_Progress.Text = "Finish updating sequence"));
            }
        }

        private List<Common.TABLE> CollectSelectedTables()
        {
            List<Common.TABLE> tables = new List<Common.TABLE>();
            if (chkBox_HinhThuc.Checked)
            {
                tables.Add(Common.TABLE.DCM_TYPE);
            }

            if (chkBox_Linhvuc.Checked)
            {
                tables.Add(Common.TABLE.DCM_LINHVUC);
            }

            if (chkBox_Book.Checked)
            {
                tables.Add(Common.TABLE.DCM_SOVANBAN);
            }

            if (chkBox_Confidential.Checked)
            {
                tables.Add(Common.TABLE.DCM_CONFIDENTIAL);                
            }

            if (chkBox_Priority.Checked)
            {
                tables.Add(Common.TABLE.DCM_PRIORITY);
            }

            if (chkBox_DCM_DOC.Checked)
            {
                tables.Add(Common.TABLE.DCM_DOC);
            }

            if (chkBox_DCM_DOC_RELATION.Checked)
            {
                tables.Add(Common.TABLE.DCM_DOC_RELATION);
            }

            if (chkbox_FEM_FILE.Checked)
            {
                tables.Add(Common.TABLE.FEM_FILE);
            }

            if (chkbox_DCM_ATTACH_FILE.Checked)
            {
                tables.Add(Common.TABLE.DCM_ATTACH_FILE);
            }

            if (chkbox_DCM_ACTIVITI_LOG.Checked)
            {
                tables.Add(Common.TABLE.DCM_ACTIVITI_LOG);
            }

            if (chkbox_DCM_ASSIGN.Checked)
            {
                tables.Add(Common.TABLE.DCM_ASSIGN);
            }

            if (chkBox_DCM_DONVI_NHAN.Checked)
            {
                tables.Add(Common.TABLE.DCM_DONVI_NHAN);
            }

            if (chkBox_DCM_LOG.Checked)
            {
                tables.Add(Common.TABLE.DCM_LOG);
            }

            if (chkBox_DCM_LOG_READ.Checked)
            {
                tables.Add(Common.TABLE.DCM_LOG_READ);
            }
            return tables;
        }
    }
}
