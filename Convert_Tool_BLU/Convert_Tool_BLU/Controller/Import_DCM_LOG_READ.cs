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
    class Import_DCM_LOG_READ : Import_Abstract
    {
        private List<DCM_LOG_READ> dcm_Log_Reads = new List<DCM_LOG_READ>();
        public List<DCM_LOG_READ> GetDcm_Log_Reads()
        {
            return dcm_Log_Reads;
        }
        protected override string getDataQuery(string fromSchema)
        {
            return string.Format(Constants.SQL_SELECT_ALL_DATA, fromSchema, Common.TABLE.DCM_LOG_READ);
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.sql_insert_dcm_log_read, toSchema);
                if (dcm_Log_Reads.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_log: " + dcm_Log_Reads.Count);

                    List<List<DCM_LOG_READ>> splited_data = Common.SplitList(dcm_Log_Reads);
                    Console.WriteLine("Total splited data to dcm_log: " + splited_data.Count);

                    foreach (List<DCM_LOG_READ> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2);
                        cmd.Parameters.Add("DATE_LOG", OracleDbType.Date);
                        cmd.Parameters.Add("DCM_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("IS_READ", OracleDbType.Int64);

                        cmd.Parameters["ID"].Value = data.Select(dcm_log => dcm_log.id).ToArray();
                        cmd.Parameters["USERNAME"].Value = data.Select(dcm_log => dcm_log.username).ToArray();
                        cmd.Parameters["DATE_LOG"].Value = data.Select(dcm_log => dcm_log.date_log).ToArray();
                        cmd.Parameters["DCM_ID"].Value = data.Select(dcm_log => dcm_log.dcm_id).ToArray();
                        cmd.Parameters["IS_READ"].Value = data.Select(dcm_log => dcm_log.is_read).ToArray();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_log: " + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            DCM_LOG_READ dcm_Log_Read = new DCM_LOG_READ();
            dcm_Log_Read.id = long.Parse(row["ID"].ToString());
            dcm_Log_Read.username = row["USERNAME"].ToString();
            if (!string.IsNullOrEmpty(row["DATE_LOG"].ToString()))
            {
                dcm_Log_Read.date_log = (DateTime)row["DATE_LOG"];
            }
            if (!string.IsNullOrEmpty(row["DCM_ID"].ToString()))
            {
                dcm_Log_Read.dcm_id = long.Parse(row["DCM_ID"].ToString());
            }
            if (!string.IsNullOrEmpty(row["IS_READ"].ToString()))
            {
                dcm_Log_Read.is_read = int.Parse(row["IS_READ"].ToString());
            }

            dcm_Log_Reads.Add(dcm_Log_Read);
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
            dcm_Log_Reads.Clear();
        }
    }
}
