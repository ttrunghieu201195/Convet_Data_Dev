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
    class Import_DCM_DONVI_NHAN : Import_Abstract
    {
        private List<Dcm_Donvi_Nhan> dcm_Donvi_Nhans = new List<Dcm_Donvi_Nhan>();
        //public static long SEQ_DCM_DONVI_NHAN;
        public List<Dcm_Donvi_Nhan> GetDcm_Donvi_Nhans()
        {
            return dcm_Donvi_Nhans;
        }
        protected override string getDataQuery(string fromSchema)
        {
            return string.Format(Constants.SQL_SELECT_DCM_DONVI_NHAN, fromSchema);
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.sql_insert_dcm_donvi_nhan, toSchema);
                if (dcm_Donvi_Nhans.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_donvi_nhan: " + dcm_Donvi_Nhans.Count);

                    List<List<Dcm_Donvi_Nhan>> splited_data = Common.SplitList(dcm_Donvi_Nhans);
                    Console.WriteLine("Total splited data to dcm_donvi_nhan: " + splited_data.Count);
                    foreach (List<Dcm_Donvi_Nhan> data in splited_data)
                    {
                        timer.Start();

                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

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

        protected override void ParseData(DataRow row)
        {
            Dcm_Donvi_Nhan dcm_Donvi_Nhan = new Dcm_Donvi_Nhan();
            try
            {                
                dcm_Donvi_Nhan.ID = long.Parse(row["ID"].ToString());
                dcm_Donvi_Nhan.DOC_ID = long.Parse(row["DOC_ID"].ToString());
                if (!string.IsNullOrEmpty(row["XULY_CHINH"].ToString()))
                {
                    dcm_Donvi_Nhan.XULY_CHINH = row["XULY_CHINH"].ToString();
                }
                if (!string.IsNullOrEmpty(row["AGENT_ID"].ToString()))
                {
                    dcm_Donvi_Nhan.AGENT_ID = long.Parse(row["AGENT_ID"].ToString());
                }
                if (!string.IsNullOrEmpty(row["UNIT_ID"].ToString()))
                {
                    dcm_Donvi_Nhan.UNIT_ID = long.Parse(row["UNIT_ID"].ToString());
                }
                dcm_Donvi_Nhan.ROLE_TYPE_CODE = row["ROLE_TYPE_CODE"].ToString();
                if (!string.IsNullOrEmpty(row["DONVI_TRONG_NGOAI"].ToString()))
                {
                    dcm_Donvi_Nhan.DONVI_TRONG_NGOAI = int.Parse(row["DONVI_TRONG_NGOAI"].ToString());
                }
                if (!string.IsNullOrEmpty(row["ACTIVITI_LOG_ID"].ToString()))
                {
                    dcm_Donvi_Nhan.ACTIVITI_LOG_ID = long.Parse(row["ACTIVITI_LOG_ID"].ToString());
                }
                if (!string.IsNullOrEmpty(row["ASSIGNED_DATE"].ToString()))
                {
                    dcm_Donvi_Nhan.ASSIGNED_DATE = (DateTime)row["ASSIGNED_DATE"];
                }
                if (!string.IsNullOrEmpty(row["XU_LY"].ToString()))
                {
                    dcm_Donvi_Nhan.XU_LY = int.Parse(row["XU_LY"].ToString());
                }
                if (!string.IsNullOrEmpty(row["TRUOC_BANHANH"].ToString()))
                {
                    dcm_Donvi_Nhan.TRUOC_BANHANH = int.Parse(row["TRUOC_BANHANH"].ToString());
                }
                if (!string.IsNullOrEmpty(row["TRANGTHAI_GUI"].ToString()))
                {
                    dcm_Donvi_Nhan.TRANGTHAI_GUI = int.Parse(row["TRANGTHAI_GUI"].ToString());
                }

                dcm_Donvi_Nhans.Add(dcm_Donvi_Nhan);
            } catch(Exception ex)
            {
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
            dcm_Donvi_Nhans.Clear();
        }
    }
}
