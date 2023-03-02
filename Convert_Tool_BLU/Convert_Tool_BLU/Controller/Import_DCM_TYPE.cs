using Convert_Data.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Controller
{
    class Import_DCM_TYPE : Import_Abstract
    {
        private List<DCM_TYPE> DCM_TYPEs = new List<DCM_TYPE>();

        public List<DCM_TYPE> GetDCM_TYPEs()
        {
            return DCM_TYPEs;
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb)
        {
            DCM_TYPE dcm_TYPE = new DCM_TYPE();
            string cell_value = row["ID"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.ID = long.Parse(cell_value) + Constants.INCREASEID_OTHERS;
            }

            cell_value = row["ten_hinhthuc"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.NAME = cell_value;
            }

            cell_value = row["stt"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.STT_HIENTHI = int.Parse(cell_value);
            }

            cell_value = row["code"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.CODE = cell_value;
            }

            cell_value = row["KY_HIEU"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.KYHIEU = cell_value;
            }
            DCM_TYPEs.Add(dcm_TYPE);
        }

        /*protected override void ParseData(DataRow row, Common.VB_TYPE type_vb, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }*/

        protected override void resetListData()
        {
            DCM_TYPEs.Clear();
        }

        public void insert_Dcm_Type(OracleConnection oracleConnection, string schema, string query, List<DCM_TYPE> data_list)
        {
            try
            {
                if (data_list.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_type: " + data_list.Count);

                    List<List<DCM_TYPE>> splited_data = Common.SplitList(data_list);
                    Console.WriteLine("Total splited data to dcm_type: " + splited_data.Count);

                    foreach (List<DCM_TYPE> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = string.Format(query, schema);

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("NAME", OracleDbType.Varchar2);
                        cmd.Parameters.Add("CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("NGUOI_TAO", OracleDbType.Varchar2);
                        cmd.Parameters.Add("NGAY_TAO", OracleDbType.Date);
                        cmd.Parameters.Add("STT_HIENTHI", OracleDbType.Int64);
                        cmd.Parameters.Add("KYHIEU", OracleDbType.Varchar2);

                        cmd.Parameters["ID"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.ID).ToArray();
                        cmd.Parameters["NAME"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NAME).ToArray();
                        cmd.Parameters["CODE"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.CODE).ToArray();
                        cmd.Parameters["NGUOI_TAO"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NGUOI_TAO).ToArray();
                        cmd.Parameters["NGAY_TAO"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NGAY_TAO).ToArray();
                        cmd.Parameters["STT_HIENTHI"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.STT_HIENTHI).ToArray();
                        cmd.Parameters["KYHIEU"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.KYHIEU).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_type: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            DCM_TYPE dcm_TYPE = new DCM_TYPE();
            string cell_value = row["ID"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.ID = long.Parse(cell_value);
            }

            cell_value = row["NAME"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.NAME = cell_value;
            }

            cell_value = row["CODE"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.CODE = cell_value;
            }

            cell_value = row["STT_HIENTHI"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.STT_HIENTHI = int.Parse(cell_value);
            }

            cell_value = row["KYHIEU"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_TYPE.KYHIEU = cell_value;
            }
            DCM_TYPEs.Add(dcm_TYPE);
        }

        protected override void ParseData<T>(T data, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.SQL_INSERT_DCM_TYPE, toSchema);
                if (DCM_TYPEs.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_type: " + DCM_TYPEs.Count);

                    List<List<DCM_TYPE>> splited_data = Common.SplitList(DCM_TYPEs);
                    Console.WriteLine("Total splited data to dcm_type: " + splited_data.Count);

                    foreach (List<DCM_TYPE> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("NAME", OracleDbType.Varchar2);
                        cmd.Parameters.Add("CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("NGUOI_TAO", OracleDbType.Varchar2);
                        cmd.Parameters.Add("NGAY_TAO", OracleDbType.Date);
                        cmd.Parameters.Add("STT_HIENTHI", OracleDbType.Int64);
                        cmd.Parameters.Add("KYHIEU", OracleDbType.Varchar2);

                        cmd.Parameters["ID"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.ID).ToArray();
                        cmd.Parameters["NAME"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NAME).ToArray();
                        cmd.Parameters["CODE"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.CODE).ToArray();
                        cmd.Parameters["NGUOI_TAO"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NGUOI_TAO).ToArray();
                        cmd.Parameters["NGAY_TAO"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NGAY_TAO).ToArray();
                        cmd.Parameters["STT_HIENTHI"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.STT_HIENTHI).ToArray();
                        cmd.Parameters["KYHIEU"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.KYHIEU).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_type: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            return string.Format(Constants.SQL_SELECT_ALL_DATA, fromSchema, Common.TABLE.DCM_TYPE);
        }
        public void StandardizedData(OracleConnection oracleConnection, string schema, string table)
        {
            // Get current records
            List<long> data = Common.GetDataIDFromTable(oracleConnection, string.Format("SELECT {0} FROM {1}{2}", table.Equals(Common.TABLE.DCM_ATTACH_FILE) ? "ATTACHMENT_ID" : "ID", schema, table));
            DCM_TYPEs.RemoveAll(r => data.Any(a => a == r.ID));
        }
    }
}
