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
    class Import_DCM_QUYTAC_NHAYSO : Import_Abstract
    {
        private List<DCM_QUYTAC_NHAYSO> dcm_QUYTAC_NHAYSOs = new List<DCM_QUYTAC_NHAYSO>();
        public static long SEQ_DCM_QUYTAC_NHAYSO;
        public List<DCM_QUYTAC_NHAYSO> GetDCM_QUYTAC_NHAYSOs()
        {
            return dcm_QUYTAC_NHAYSOs;
        }
        protected override void ParseData(DataRow row)
        {
            DCM_QUYTAC_NHAYSO dCM_QUYTAC_NHAYSO = new DCM_QUYTAC_NHAYSO();
            dCM_QUYTAC_NHAYSO.ID = long.Parse(row["ID"].ToString());
            dCM_QUYTAC_NHAYSO.MA_QUYTAC = row["MA_QUYTAC"].ToString();
            dCM_QUYTAC_NHAYSO.TYPE_CODE = row["TYPE_CODE"].ToString();
            dCM_QUYTAC_NHAYSO.SOVANBAN_CODE = row["SOVANBAN_CODE"].ToString();
            
            dcm_QUYTAC_NHAYSOs.Add(dCM_QUYTAC_NHAYSO);
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb)
        {
           
        }

        public void insert_DCM_QUYTAC_NHAYSO(OracleConnection oracleConnection, string query, List<DCM_QUYTAC_NHAYSO> data_list)
        {
            try
            {
                if (data_list.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to DCM_QUYTAC_NHAYSO : " + data_list.Count);

                    List<List<DCM_QUYTAC_NHAYSO>> splited_data = Common.SplitList(data_list);
                    Console.WriteLine("Total splited data to DCM_QUYTAC_NHAYSO : " + splited_data.Count);

                    foreach (List<DCM_QUYTAC_NHAYSO> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("MA_QUYTAC", OracleDbType.Varchar2);
                        cmd.Parameters.Add("TYPE_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("SOVANBAN_CODE", OracleDbType.Varchar2);

                        cmd.Parameters["ID"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.ID).ToArray();
                        cmd.Parameters["MA_QUYTAC"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.MA_QUYTAC).ToArray();
                        cmd.Parameters["TYPE_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.TYPE_CODE).ToArray();
                        cmd.Parameters["SOVANBAN_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.SOVANBAN_CODE).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to DCM_QUYTAC_NHAYSO : " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            dcm_QUYTAC_NHAYSOs.Clear();
        }

        protected override void ParseData<T>(T data, DataTable dcm_type)
        {
            Dcm_SoVanBan soVanBan = data as Dcm_SoVanBan;
            foreach (DataRow row in dcm_type.Rows)
            {
                string dcm_type_code = row["CODE"].ToString();
                DCM_QUYTAC_NHAYSO dcm_QUYTAC_NHAYSO = new DCM_QUYTAC_NHAYSO();
                dcm_QUYTAC_NHAYSO.ID = ++SEQ_DCM_QUYTAC_NHAYSO;
                dcm_QUYTAC_NHAYSO.SOVANBAN_CODE = soVanBan.code;
                dcm_QUYTAC_NHAYSO.TYPE_CODE = dcm_type_code;

                dcm_QUYTAC_NHAYSOs.Add(dcm_QUYTAC_NHAYSO);
            }
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.sql_insert_DCM_QUYTAC_NHAYSO, toSchema);
                if (dcm_QUYTAC_NHAYSOs.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to DCM_QUYTAC_NHAYSO : " + dcm_QUYTAC_NHAYSOs.Count);

                    List<List<DCM_QUYTAC_NHAYSO>> splited_data = Common.SplitList(dcm_QUYTAC_NHAYSOs);
                    Console.WriteLine("Total splited data to DCM_QUYTAC_NHAYSO : " + splited_data.Count);

                    foreach (List<DCM_QUYTAC_NHAYSO> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("MA_QUYTAC", OracleDbType.Varchar2);
                        cmd.Parameters.Add("TYPE_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("SOVANBAN_CODE", OracleDbType.Varchar2);

                        cmd.Parameters["ID"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.ID).ToArray();
                        cmd.Parameters["MA_QUYTAC"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.MA_QUYTAC).ToArray();
                        cmd.Parameters["TYPE_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.TYPE_CODE).ToArray();
                        cmd.Parameters["SOVANBAN_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.SOVANBAN_CODE).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to DCM_QUYTAC_NHAYSO : " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            return string.Format(Constants.SQL_SELECT_DCM_QUYTAC_NHAYSO, fromSchema);
        }
    }
}
