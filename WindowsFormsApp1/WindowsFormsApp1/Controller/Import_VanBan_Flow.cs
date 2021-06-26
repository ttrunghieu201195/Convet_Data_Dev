using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convert_Data.Controller;
using Convert_Data.Models;

namespace Convert_Data
{
    class Import_VanBan_Flow : Import_Abstract
    {
        // list dcm_activiti_log
        private List<Dcm_Activiti_Log> dcm_Activiti_Logs = new List<Dcm_Activiti_Log>();
        private List<Dcm_Assign> dcm_Assigns = new List<Dcm_Assign>();
        private List<Dcm_Donvi_Nhan> dcm_Donvi_Nhans = new List<Dcm_Donvi_Nhan>();

        // SEQ
        public static long SEQ_DCM_ACTIVITI_LOG;
        public static long SEQ_DCM_ASSIGN;
        public static long SEQ_DCM_DONVI_NHAN;

        public List<Dcm_Activiti_Log> GetDcm_Activiti_Logs()
        {
            return dcm_Activiti_Logs;
        }

        public List<Dcm_Assign> GetDcm_Assigns()
        {
            return dcm_Assigns;
        }

        public List<Dcm_Donvi_Nhan> GetDcm_Donvi_Nhans()
        {
            return dcm_Donvi_Nhans;
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE vb_type)
        {
            Dcm_Activiti_Log dcm_Activiti_Log = new Dcm_Activiti_Log();
            string cell_value = "";
            // 1 - stt
             
            dcm_Activiti_Log.id = ++SEQ_DCM_ACTIVITI_LOG;

            // 2 - doc_id
            cell_value = row["id_vanban"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_Activiti_Log.doc_id = vb_type == Common.VB_TYPE.VB_DI ? long.Parse(cell_value.Trim()) + Constants.INCREASEID_VBDI : long.Parse(cell_value.Trim());
            }

            // 3 - nguoi_gui/updated_by
            cell_value = row["nguoi_gui"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_Activiti_Log.updated_by = cell_value.Trim();
            }

            // 4 - thoigian_gui/updated_date
            cell_value = row["thoigian_gui"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                DateTime updated_date = DateTime.ParseExact(cell_value.Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                dcm_Activiti_Log.updated_date = updated_date;
            }

            // 5 - nguoi_nhan
            cell_value = row["nguoi_nhan"].ToString();
            string ds_nguoinhan = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_nguoinhan = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 6 - vai_tro_nguoinhan
            cell_value = row["vai_tro_nguoinhan"].ToString();
            string ds_vaitro_nguoinhan = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_vaitro_nguoinhan = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 7 - donvi_nhan
            cell_value = row["donvi_nhan"].ToString();
            string ds_donvi_nhan = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_donvi_nhan = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 8 - vaitro_donvi
            cell_value = row["vaitro_donvi"].ToString();
            string ds_vaitro_donvi = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_vaitro_donvi = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 9 - ykien_xuly
            cell_value = row["ykien_xuly"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_Activiti_Log.comment_ = cell_value.Trim();
                dcm_Activiti_Log.comment_full = cell_value.Trim();
            }

            // 10 - agent_id
            cell_value = row["agent_id"].ToString();
            string ds_agent_id = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_agent_id = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 11 - task_key
            cell_value = row["task_key"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_Activiti_Log.task_key = cell_value.Trim();
            }

            // 12 - action_tv
            cell_value = row["action_tv"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_Activiti_Log.action = cell_value.Trim();
            }

            // 13 - approved
            cell_value = row["approved"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_Activiti_Log.approved = cell_value.Trim();
            }

            // 14 - trang_thai_xuly
            cell_value = row["trang_thai_xuly"].ToString();
            string ds_trangthai_xuly = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_trangthai_xuly = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 15 - thoigian_xuly
            cell_value = row["thoigian_xuly"].ToString();
            string ds_thoigian_xuly = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_thoigian_xuly = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 16 - trang_thai_xuly_donvi
            cell_value = row["trang_thai_xuly_donvi"].ToString();
            string ds_trangthai_xuly_donvi = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_trangthai_xuly_donvi = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 17 - thoigian_xuly_donvi
            cell_value = row["thoigian_xuly_donvi"].ToString();
            string ds_thoigian_xuly_donvi = "";
            if (cell_value != null && cell_value != string.Empty)
            {
                ds_thoigian_xuly_donvi = cell_value.Trim();
                //dcm_Activiti_Log.updated_by = nguoi_gui;
            }

            // 18 - loai_dv

            // 19 - truoc_banhanh
            int? truoc_banhanh = null;
            if (vb_type == Common.VB_TYPE.VB_DI)
            {
                cell_value = row["truoc_banhanh"].ToString();
                if (cell_value != null && cell_value != string.Empty)
                {
                    truoc_banhanh = int.Parse(cell_value.Trim());
                }
            }
            dcm_Activiti_Logs.Add(dcm_Activiti_Log);

            appendToListDcmAssign(dcm_Activiti_Log.id, dcm_Activiti_Log.doc_id, dcm_Activiti_Log.updated_by, dcm_Activiti_Log.updated_date, ds_nguoinhan, ds_vaitro_nguoinhan, ds_trangthai_xuly, ds_thoigian_xuly, truoc_banhanh);
            appendToListDcmDonviNhan(dcm_Activiti_Log.id, dcm_Activiti_Log.doc_id, ds_donvi_nhan, ds_vaitro_donvi, ds_agent_id, dcm_Activiti_Log.updated_date, ds_trangthai_xuly_donvi, ds_thoigian_xuly_donvi, truoc_banhanh);
        }

        private void appendToListDcmAssign(long id_acti_log, long idVB, string nguoi_gui, DateTime thoigian_gui, string ds_nguoinhan, string ds_vaitro_nguoinhan, string ds_trangthai_xuly, string ds_thoigian_xuly, int? truoc_banhanh)
        {
            string[] nguoi_nhan_arr = ds_nguoinhan.Split(';');
            string[] dsvaitro_nguoinhan_arr = ds_vaitro_nguoinhan.Split(';');
            string[] ds_trangthai_xuly_arr = ds_trangthai_xuly.Split(';');
            string[] ds_thoigian_xuly_arr = ds_thoigian_xuly.Split(';');

            if (nguoi_nhan_arr.Length > 0 && nguoi_nhan_arr.Length == dsvaitro_nguoinhan_arr.Length && nguoi_nhan_arr.Length == ds_trangthai_xuly_arr.Length && ds_trangthai_xuly_arr.Length == ds_thoigian_xuly_arr.Length)
            {
                for (int i = 0; i < nguoi_nhan_arr.Length; i++)
                {
                    if (nguoi_nhan_arr[i] == string.Empty)
                    {
                        continue;
                    }

                    Dcm_Assign dcm_Assign = new Dcm_Assign();

                    dcm_Assign.id = ++SEQ_DCM_ASSIGN;
                    dcm_Assign.document_id = idVB;
                    dcm_Assign.assignee = nguoi_nhan_arr[i].Trim();
                    dcm_Assign.assigner = nguoi_gui;
                    dcm_Assign.assigned_date = thoigian_gui;

                    string role_code = Common.getValue_VaiTro(dsvaitro_nguoinhan_arr.Length > 0 ? dsvaitro_nguoinhan_arr[i].Trim() : "1");
                    dcm_Assign.role_type_code = role_code;

                    dcm_Assign.xu_ly = int.Parse(ds_trangthai_xuly_arr[i].Trim());

                    if (ds_trangthai_xuly_arr[i] == "2")
                    {
                        DateTime xuly_date = DateTime.ParseExact(ds_thoigian_xuly_arr[i].Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        dcm_Assign.ngay_xuly = xuly_date;
                    }

                    dcm_Assign.activiti_log_id = dcm_Assign.assignee == dcm_Assign.assigner ? 0 : id_acti_log;
                    dcm_Assign.truoc_banhanh = truoc_banhanh;
                    
                    dcm_Assigns.Add(dcm_Assign);
                }
            }
        }

        private void appendToListDcmDonviNhan(long id_acti_log, long id_vb, string ds_donvi, string ds_vaitro_donvi, string agent_id, DateTime thoigian_gui, string ds_trangthai_xuly_donvi, string ds_thoigian_xuly_donvi, int? truoc_banhanh)
        {
            string[] ds_donvi_arr = ds_donvi.Split(';');
            string[] ds_vaitro_donvi_arr = ds_vaitro_donvi.Split(';');
            string[] ds_trangthai_xuly_donvi_arr = ds_trangthai_xuly_donvi.Split(';');
            string[] ds_thoigian_xuly_donvi_arr = ds_thoigian_xuly_donvi.Split(';');

            string[] ds_agent_id = agent_id.Split(';');

            if (ds_donvi_arr.Length > 0 && ds_donvi_arr.Length == ds_vaitro_donvi_arr.Length && ds_vaitro_donvi_arr.Length == ds_trangthai_xuly_donvi_arr.Length && ds_trangthai_xuly_donvi_arr.Length == ds_thoigian_xuly_donvi_arr.Length)
            {
                for (int i = 0; i < ds_donvi_arr.Length; i++)
                {
                    if (ds_donvi_arr[i] == string.Empty)
                    {
                        continue;
                    }

                    Dcm_Donvi_Nhan dcm_Donvi_Nhan = new Dcm_Donvi_Nhan();
                    dcm_Donvi_Nhan.ID = ++SEQ_DCM_DONVI_NHAN;
                    dcm_Donvi_Nhan.DOC_ID = id_vb;
                    dcm_Donvi_Nhan.XULY_CHINH = ds_vaitro_donvi_arr[i].Trim();
                    dcm_Donvi_Nhan.AGENT_ID = long.Parse(ds_agent_id[i].Trim());
                    dcm_Donvi_Nhan.UNIT_ID = long.Parse(ds_donvi_arr[i].Trim());

                    dcm_Donvi_Nhan.ROLE_TYPE_CODE = Common.getValue_VaiTro(ds_vaitro_donvi_arr.Length > 0 ? ds_vaitro_donvi_arr[i].Trim() : "1"); 

                    dcm_Donvi_Nhan.ACTIVITI_LOG_ID = id_acti_log;
                    dcm_Donvi_Nhan.ASSIGNED_DATE = thoigian_gui;
                    dcm_Donvi_Nhan.XU_LY = int.Parse(ds_trangthai_xuly_donvi_arr[i].Trim());

                    dcm_Donvi_Nhan.TRUOC_BANHANH = truoc_banhanh;

                    dcm_Donvi_Nhans.Add(dcm_Donvi_Nhan);
                }
            } else
            {
                MessageBox.Show("error ne");
            }

            
        }

        public void insert_Dcm_Activiti_Log(OracleConnection oracleConnection, Configs configs, string query, List<Dcm_Activiti_Log> data_list)
        {
            try
            {
                if (data_list.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_activiti_log: " + data_list.Count);

                    List<List<Dcm_Activiti_Log>> splited_data =  Common.SplitList(data_list);
                    Console.WriteLine("Total splited data to dcm_activiti_log: " + splited_data.Count);

                    foreach (List<Dcm_Activiti_Log> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = string.Format(query, configs.schema);

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("TASK_KEY", OracleDbType.Varchar2);
                        cmd.Parameters.Add("UPDATED_DATE", OracleDbType.Date);
                        cmd.Parameters.Add("UPDATED_BY", OracleDbType.Varchar2);
                        cmd.Parameters.Add("ACTION", OracleDbType.Varchar2);
                        cmd.Parameters.Add("DOC_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("APPROVED", OracleDbType.Varchar2);
                        cmd.Parameters.Add("COMMENT_", OracleDbType.Varchar2);
                        cmd.Parameters.Add("COMMENT_FULL", OracleDbType.Varchar2);
                        cmd.Parameters.Add("FORMID", OracleDbType.Varchar2);
                        cmd.Parameters.Add("ACTION_CODE", OracleDbType.Varchar2);

                        cmd.Parameters["ID"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.id).ToArray();
                        cmd.Parameters["TASK_KEY"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.task_key).ToArray();
                        cmd.Parameters["UPDATED_DATE"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.updated_date).ToArray();
                        cmd.Parameters["UPDATED_BY"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.updated_by).ToArray();
                        cmd.Parameters["ACTION"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.action).ToArray();
                        cmd.Parameters["DOC_ID"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.doc_id).ToArray();
                        cmd.Parameters["APPROVED"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.approved).ToArray();
                        cmd.Parameters["COMMENT_"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.comment_).ToArray();
                        cmd.Parameters["COMMENT_FULL"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.comment_full).ToArray();
                        cmd.Parameters["FORMID"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.formid).ToArray();
                        cmd.Parameters["ACTION_CODE"].Value = data.Select(dcm_activiti_log => dcm_activiti_log.action_code).ToArray();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_activiti_log: " + data.Count + "/" + timer.ElapsedMilliseconds/1000 + "(s)");
                        timer.Reset();
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void insert_Dcm_Assign(OracleConnection oracleConnection, Configs configs, string query, List<Dcm_Assign> data_list)
        {
            try
            {
                if (data_list.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_assign: " + data_list.Count);

                    List<List<Dcm_Assign>> splited_data = Common.SplitList(data_list);
                    Console.WriteLine("Total splited data to dcm_assign: " + splited_data.Count);
                    foreach (List<Dcm_Assign> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = string.Format(query, configs.schema);

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("DOCUMENT_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("ASSIGNEE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("ASSIGNER", OracleDbType.Varchar2);
                        cmd.Parameters.Add("ASSIGNED_DATE", OracleDbType.Date);
                        cmd.Parameters.Add("ROLE_TYPE_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("XU_LY", OracleDbType.Int64);
                        cmd.Parameters.Add("NGAY_XULY", OracleDbType.Date);
                        cmd.Parameters.Add("ACTIVITI_LOG_ID", OracleDbType.Int64);

                        cmd.Parameters["ID"].Value = data.Select(dcm_assign => dcm_assign.id).ToArray();
                        cmd.Parameters["DOCUMENT_ID"].Value = data.Select(dcm_assign => dcm_assign.document_id).ToArray();
                        cmd.Parameters["ASSIGNEE"].Value = data.Select(dcm_assign => dcm_assign.assignee).ToArray();
                        cmd.Parameters["ASSIGNER"].Value = data.Select(dcm_assign => dcm_assign.assigner).ToArray();
                        cmd.Parameters["ASSIGNED_DATE"].Value = data.Select(dcm_assign => dcm_assign.assigned_date).ToArray();
                        cmd.Parameters["ROLE_TYPE_CODE"].Value = data.Select(dcm_assign => dcm_assign.role_type_code).ToArray();
                        cmd.Parameters["XU_LY"].Value = data.Select(dcm_assign => dcm_assign.xu_ly).ToArray();
                        cmd.Parameters["NGAY_XULY"].Value = data.Select(dcm_assign => dcm_assign.ngay_xuly).ToArray();
                        cmd.Parameters["ACTIVITI_LOG_ID"].Value = data.Select(dcm_assign => dcm_assign.activiti_log_id).ToArray();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_assign: " + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
                        timer.Reset();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void insert_Dcm_Donvi_Nhan(OracleConnection oracleConnection, Configs configs, string query, List<Dcm_Donvi_Nhan> data_list)
        {
            try
            {
                if (data_list.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_donvi_nhan: " + data_list.Count);

                    List<List<Dcm_Donvi_Nhan>> splited_data = Common.SplitList(data_list);
                    Console.WriteLine("Total splited data to dcm_donvi_nhan: " + splited_data.Count);
                    foreach (List<Dcm_Donvi_Nhan> data in splited_data)
                    {
                        timer.Start();

                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = string.Format(query, configs.schema);

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("DOC_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("XULY_CHINH", OracleDbType.Varchar2);
                        cmd.Parameters.Add("AGENT_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("UNIT_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("ROLE_TYPE_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("DONVI_TRONG_NGOAI", OracleDbType.Int64);
                        cmd.Parameters.Add("ACTIVITI_LOG_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("ASSIGNED_DATE", OracleDbType.Date);
                        cmd.Parameters.Add("XU_LY", OracleDbType.Int64);
                        cmd.Parameters.Add("TRUOC_BANHANH", OracleDbType.Int64);
                        cmd.Parameters.Add("TRANGTHAI_GUI", OracleDbType.Int64);

                        cmd.Parameters["ID"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.ID).ToArray();
                        cmd.Parameters["DOC_ID"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.DOC_ID).ToArray();
                        cmd.Parameters["XULY_CHINH"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.XULY_CHINH).ToArray();
                        cmd.Parameters["AGENT_ID"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.AGENT_ID).ToArray();
                        cmd.Parameters["UNIT_ID"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.UNIT_ID).ToArray();
                        cmd.Parameters["ROLE_TYPE_CODE"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.ROLE_TYPE_CODE).ToArray();
                        cmd.Parameters["DONVI_TRONG_NGOAI"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.DONVI_TRONG_NGOAI).ToArray();
                        cmd.Parameters["ACTIVITI_LOG_ID"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.ACTIVITI_LOG_ID).ToArray();
                        cmd.Parameters["ASSIGNED_DATE"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.ASSIGNED_DATE).ToArray();
                        cmd.Parameters["XU_LY"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.XU_LY).ToArray();
                        cmd.Parameters["TRUOC_BANHANH"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.TRUOC_BANHANH).ToArray();
                        cmd.Parameters["TRANGTHAI_GUI"].Value = data.Select(dcm_donvi_nhan => dcm_donvi_nhan.TRANGTHAI_GUI).ToArray();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_donvi_nhan: " + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
                        timer.Reset();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void resetListData()
        {
            dcm_Activiti_Logs.Clear();
            dcm_Assigns.Clear();
            dcm_Donvi_Nhans.Clear();
        }
    }
}
