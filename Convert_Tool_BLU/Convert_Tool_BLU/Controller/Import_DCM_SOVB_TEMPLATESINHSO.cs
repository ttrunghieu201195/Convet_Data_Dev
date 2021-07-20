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
    class Import_DCM_SOVB_TEMPLATESINHSO : Import_Abstract
    {
        private List<DCM_SOVB_TEMPLATESINHSO> dcm_SOVB_TEMPLATESINHSOs = new List<DCM_SOVB_TEMPLATESINHSO>();
        public static long SEQ_DCM_SOVB_TEMPLATESINHSO;

        public List<DCM_SOVB_TEMPLATESINHSO> GetDCM_SOVB_TEMPLATESINHSOs()
        {
            return dcm_SOVB_TEMPLATESINHSOs;
        }
        protected override void ParseData(DataRow row)
        {
            throw new NotImplementedException();
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb)
        {
            throw new NotImplementedException();
        }

        /*protected override void ParseData(DataRow row, Common.VB_TYPE type_vb, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }*/

        public void ParseData(DataTable dcm_type, string sovanban_code)
        {
            foreach (DataRow row in dcm_type.Rows)
            {
                string dcm_type_code = row["CODE"].ToString();
                DCM_SOVB_TEMPLATESINHSO dcm_SOVB_TEMPLATESINHSO = new DCM_SOVB_TEMPLATESINHSO();
                dcm_SOVB_TEMPLATESINHSO.id = ++SEQ_DCM_SOVB_TEMPLATESINHSO;
                dcm_SOVB_TEMPLATESINHSO.SOVANBAN_CODE = sovanban_code;
                dcm_SOVB_TEMPLATESINHSO.TYPE_CODE = dcm_type_code;
                
                dcm_SOVB_TEMPLATESINHSOs.Add(dcm_SOVB_TEMPLATESINHSO);
            }
        }

        public void insert_Dcm_SoVB_TemplateSinhSo(OracleConnection oracleConnection, string query, List<DCM_SOVB_TEMPLATESINHSO> data_list)
        {
            try
            {
                if (data_list.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to DCM_SOVB_TEMPLATESINHSO: " + data_list.Count);

                    List<List<DCM_SOVB_TEMPLATESINHSO>> splited_data = Common.SplitList(data_list);
                    Console.WriteLine("Total splited data to DCM_SOVB_TEMPLATESINHSO: " + splited_data.Count);

                    foreach (List<DCM_SOVB_TEMPLATESINHSO> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("SOVANBAN_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("TYPE_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("TEMPLATE_SINH_SOVB_CODE", OracleDbType.Varchar2);

                        cmd.Parameters["ID"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.id).ToArray();
                        cmd.Parameters["SOVANBAN_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.SOVANBAN_CODE).ToArray();
                        cmd.Parameters["TYPE_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.TYPE_CODE).ToArray();
                        cmd.Parameters["TEMPLATE_SINH_SOVB_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.TEMPLATE_SINH_SOVB_CODE).ToArray();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to DCM_SOVB_TEMPLATESINHSO: " + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            throw new NotImplementedException();
        }

        protected override void ParseData<T>(T data, DataTable dcm_type)
        {
            Dcm_SoVanBan soVanBan = data as Dcm_SoVanBan;
            foreach (DataRow row in dcm_type.Rows)
            {
                string dcm_type_code = row["CODE"].ToString();
                DCM_SOVB_TEMPLATESINHSO dcm_SOVB_TEMPLATESINHSO = new DCM_SOVB_TEMPLATESINHSO();
                dcm_SOVB_TEMPLATESINHSO.id = ++SEQ_DCM_SOVB_TEMPLATESINHSO;
                dcm_SOVB_TEMPLATESINHSO.SOVANBAN_CODE = soVanBan.code;
                dcm_SOVB_TEMPLATESINHSO.TYPE_CODE = dcm_type_code;

                dcm_SOVB_TEMPLATESINHSOs.Add(dcm_SOVB_TEMPLATESINHSO);
            }
        }
    }
}

