using Convert_Data.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Controller
{
    class Import_DCM_ASSIGN : Import_Abstract
    {
        private List<Dcm_Assign> dcm_Assigns = new List<Dcm_Assign>();
        //public static long SEQ_DCM_ASSIGN;
        public List<Dcm_Assign> GetDcm_Assigns()
        {
            return dcm_Assigns;
        }
        protected override void ParseData(DataRow row)
        {
            Dcm_Assign dcm_Assign = new Dcm_Assign();
            try
            {
                dcm_Assign.id = long.Parse(row["ID"].ToString());
                if (!string.IsNullOrEmpty(row["DOCUMENT_ID"].ToString()))
                {
                    dcm_Assign.document_id = long.Parse(row["DOCUMENT_ID"].ToString());
                }
                dcm_Assign.assignee = row["ASSIGNEE"].ToString();
                dcm_Assign.assigner = row["ASSIGNER"].ToString();
                if (!string.IsNullOrEmpty(row["ASSIGNED_DATE"].ToString()))
                {
                    dcm_Assign.assigned_date = (DateTime)row["ASSIGNED_DATE"];
                }                
                dcm_Assign.role_type_code = row["ROLE_TYPE_CODE"].ToString();
                dcm_Assign.xu_ly = int.Parse(row["XU_LY"].ToString());
                if (!string.IsNullOrEmpty(row["NGAY_XULY"].ToString()))
                {
                    dcm_Assign.ngay_xuly = (DateTime)row["NGAY_XULY"];
                }
                if (!string.IsNullOrEmpty(row["ACTIVITI_LOG_ID"].ToString()))
                {
                    dcm_Assign.activiti_log_id = long.Parse(row["ACTIVITI_LOG_ID"].ToString());
                }
                if (!string.IsNullOrEmpty(row["TRUOC_BANHANH"].ToString()))
                {
                    dcm_Assign.truoc_banhanh = int.Parse(row["TRUOC_BANHANH"].ToString());
                }

                dcm_Assigns.Add(dcm_Assign);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void ParseData<T>(T data, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb)
        {
            throw new NotImplementedException();
        }

        protected override void resetListData()
        {
            dcm_Assigns.Clear();
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.sql_insert_dcm_assign, toSchema);
                if (dcm_Assigns.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_assign: " + dcm_Assigns.Count);

                    List<List<Dcm_Assign>> splited_data = Common.SplitList(dcm_Assigns);
                    Console.WriteLine("Total splited data to dcm_assign: " + splited_data.Count);
                    foreach (List<Dcm_Assign> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("DOCUMENT_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("ASSIGNEE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("ASSIGNER", OracleDbType.Varchar2);
                        cmd.Parameters.Add("ASSIGNED_DATE", OracleDbType.Date);
                        cmd.Parameters.Add("ROLE_TYPE_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("XU_LY", OracleDbType.Int64);
                        cmd.Parameters.Add("NGAY_XULY", OracleDbType.Date);
                        cmd.Parameters.Add("ACTIVITI_LOG_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("TRUOC_BANHANH", OracleDbType.Int64);

                        cmd.Parameters["ID"].Value = data.Select(dcm_assign => dcm_assign.id).ToArray();
                        cmd.Parameters["DOCUMENT_ID"].Value = data.Select(dcm_assign => dcm_assign.document_id).ToArray();
                        cmd.Parameters["ASSIGNEE"].Value = data.Select(dcm_assign => dcm_assign.assignee).ToArray();
                        cmd.Parameters["ASSIGNER"].Value = data.Select(dcm_assign => dcm_assign.assigner).ToArray();
                        cmd.Parameters["ASSIGNED_DATE"].Value = data.Select(dcm_assign => dcm_assign.assigned_date).ToArray();
                        cmd.Parameters["ROLE_TYPE_CODE"].Value = data.Select(dcm_assign => dcm_assign.role_type_code).ToArray();
                        cmd.Parameters["XU_LY"].Value = data.Select(dcm_assign => dcm_assign.xu_ly).ToArray();
                        cmd.Parameters["NGAY_XULY"].Value = data.Select(dcm_assign => dcm_assign.ngay_xuly).ToArray();
                        cmd.Parameters["ACTIVITI_LOG_ID"].Value = data.Select(dcm_assign => dcm_assign.activiti_log_id).ToArray();
                        cmd.Parameters["TRUOC_BANHANH"].Value = data.Select(dcm_assign => dcm_assign.truoc_banhanh).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_assign: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
                        timer.Reset();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override string getDataQuery(string fromSchema)
        {
            return string.Format(Constants.SQL_SELECT_DCM_ASSIGN, fromSchema);
        }
    }
}
