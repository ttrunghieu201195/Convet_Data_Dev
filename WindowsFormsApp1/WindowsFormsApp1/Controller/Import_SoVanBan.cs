using Convert_Data;
using Convert_Data.Controller;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.Controller
{
    class Import_SoVanBan : Import_Abstract
    {
        private List<Dcm_SoVanBan> dcm_SoVanBans = new List<Dcm_SoVanBan>();
        private List<DCM_QUYTAC_NHAYSO> dcm_QUYTAC_NHAYSOs = new List<DCM_QUYTAC_NHAYSO>();
        private List<DCM_SOVB_TEMPLATESINHSO> dcm_SOVB_TEMPLATESINHSOs = new List<DCM_SOVB_TEMPLATESINHSO>();

        public static long SEQ_DCM_SOVB_TEMPLATESINHSO;
        public static long SEQ_DCM_QUYTAC_NHAYSO;

        public List<Dcm_SoVanBan> GetDcm_SoVanBans()
        {
            return dcm_SoVanBans;
        }

        public List<DCM_QUYTAC_NHAYSO> GetDCM_QUYTAC_NHAYSOs()
        {
            return dcm_QUYTAC_NHAYSOs;
        }

        public List<DCM_SOVB_TEMPLATESINHSO> GetDCM_SOVB_TEMPLATESINHSOs()
        {
            return dcm_SOVB_TEMPLATESINHSOs;
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb, DataTable dcm_type)
        {
            Dcm_SoVanBan dcm_SoVanBan = new Dcm_SoVanBan();
            string cell_value = "";
            cell_value = row["ID"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.id = long.Parse(cell_value) + Constants.INCREASEID_OTHERS;
            }

            cell_value = row["ten_sovb"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.name = cell_value.Trim();
            }

            cell_value = row["loai_so"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.ma_loai_so = cell_value == "1" ? "DEN" : "DI";
            }

            cell_value = row["ngay_mo_so"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                DateTime ngay_mo_so = DateTime.ParseExact(cell_value.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_SoVanBan.ngay_batdau_sudung = ngay_mo_so;
            }

            cell_value = row["ngay_het_hieuluc"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                DateTime ngay_het_hieuluc = DateTime.ParseExact(cell_value.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_SoVanBan.ngay_dongso = ngay_het_hieuluc;
            }

            cell_value = row["nguoi_quantri"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.nguoi_quantri = cell_value;
            }

            cell_value = row["nguoi_theodoi"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.nguoi_theodoi = cell_value;
            }

            cell_value = row["unit_code"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.unit_code = "BLU_TINHBACLIEU";
            }

            cell_value = row["stt"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.stt_hienthi = int.Parse(cell_value);
            }

            dcm_SoVanBan.code = dcm_SoVanBan.id.ToString();

            dcm_SoVanBans.Add(dcm_SoVanBan);
            appendToRelatedTable(dcm_type, dcm_SoVanBan.code);
        }

        private void appendToRelatedTable(DataTable dcm_type, string sovanban_code)
        {
            foreach (DataRow row in dcm_type.Rows)
            {
                string dcm_type_code = row["CODE"].ToString();
                DCM_SOVB_TEMPLATESINHSO dcm_SOVB_TEMPLATESINHSO = new DCM_SOVB_TEMPLATESINHSO();
                dcm_SOVB_TEMPLATESINHSO.id = ++SEQ_DCM_SOVB_TEMPLATESINHSO;
                dcm_SOVB_TEMPLATESINHSO.SOVANBAN_CODE = sovanban_code;
                dcm_SOVB_TEMPLATESINHSO.TYPE_CODE = dcm_type_code;
                
                dcm_SOVB_TEMPLATESINHSOs.Add(dcm_SOVB_TEMPLATESINHSO);

                DCM_QUYTAC_NHAYSO dcm_QUYTAC_NHAYSO = new DCM_QUYTAC_NHAYSO();
                dcm_QUYTAC_NHAYSO.ID = ++SEQ_DCM_QUYTAC_NHAYSO;
                dcm_QUYTAC_NHAYSO.SOVANBAN_CODE = sovanban_code;
                dcm_QUYTAC_NHAYSO.TYPE_CODE = dcm_type_code;

                dcm_QUYTAC_NHAYSOs.Add(dcm_QUYTAC_NHAYSO);
            }
        }

        public DataTable GetDcm_Type(OracleConnection connection, string schema, string query)
        {
            OracleCommand cmd = new OracleCommand(string.Format(query, schema), connection);

            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            return dataTable;
        }

        public void insert_Dcm_SoVanBan(OracleConnection oracleConnection, Configs configs, string query, List<Dcm_SoVanBan> data_list)
        {

            try
            {
                if (data_list.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_log: " + data_list.Count);

                    List<List<Dcm_SoVanBan>> splited_data = Common.SplitList(data_list);
                    Console.WriteLine("Total splited data to dcm_log: " + splited_data.Count);

                    foreach (List<Dcm_SoVanBan> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = string.Format(query, configs.schema);

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("NAME", OracleDbType.Varchar2);
                        cmd.Parameters.Add("SOBATDAU", OracleDbType.Int64);
                        cmd.Parameters.Add("MA_LOAI_SO", OracleDbType.Varchar2);
                        cmd.Parameters.Add("NGAY_BATDAU_SUDUNG", OracleDbType.Date);
                        cmd.Parameters.Add("NGAY_DONGSO", OracleDbType.Date);
                        cmd.Parameters.Add("UNIT_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("NGUOI_THEODOI", OracleDbType.Varchar2);
                        cmd.Parameters.Add("NGUOI_QUANTRI", OracleDbType.Varchar2);
                        cmd.Parameters.Add("STT_HIENTHI", OracleDbType.Int64);

                        cmd.Parameters["ID"].Value = data.Select(dcm_sovanban => dcm_sovanban.id).ToArray();
                        cmd.Parameters["NAME"].Value = data.Select(dcm_sovanban => dcm_sovanban.name).ToArray();
                        cmd.Parameters["SOBATDAU"].Value = data.Select(dcm_sovanban => dcm_sovanban.sobatdau).ToArray();
                        cmd.Parameters["MA_LOAI_SO"].Value = data.Select(dcm_sovanban => dcm_sovanban.ma_loai_so).ToArray();
                        cmd.Parameters["NGAY_BATDAU_SUDUNG"].Value = data.Select(dcm_sovanban => dcm_sovanban.ngay_batdau_sudung).ToArray();
                        cmd.Parameters["NGAY_DONGSO"].Value = data.Select(dcm_sovanban => dcm_sovanban.ngay_dongso).ToArray();
                        cmd.Parameters["UNIT_CODE"].Value = data.Select(dcm_sovanban => dcm_sovanban.unit_code).ToArray();
                        cmd.Parameters["CODE"].Value = data.Select(dcm_sovanban => dcm_sovanban.code).ToArray();
                        cmd.Parameters["NGUOI_THEODOI"].Value = data.Select(dcm_sovanban => dcm_sovanban.nguoi_theodoi).ToArray();
                        cmd.Parameters["NGUOI_QUANTRI"].Value = data.Select(dcm_sovanban => dcm_sovanban.nguoi_quantri).ToArray();
                        cmd.Parameters["STT_HIENTHI"].Value = data.Select(dcm_sovanban => dcm_sovanban.stt_hienthi).ToArray();

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

        public void insert_Dcm_SoVB_TemplateSinhSo(OracleConnection oracleConnection, Configs configs, string query, List<DCM_SOVB_TEMPLATESINHSO> data_list)
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

                        cmd.CommandText = string.Format(query, configs.schema);

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

        public void insert_DCM_QUYTAC_NHAYSO(OracleConnection oracleConnection, Configs configs, string query, List<DCM_QUYTAC_NHAYSO> data_list)
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

                        cmd.CommandText = string.Format(query, configs.schema);

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("MA_QUYTAC", OracleDbType.Varchar2);
                        cmd.Parameters.Add("TYPE_CODE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("SOVANBAN_CODE", OracleDbType.Varchar2);

                        cmd.Parameters["ID"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.ID).ToArray();
                        cmd.Parameters["MA_QUYTAC"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.MA_QUYTAC).ToArray();
                        cmd.Parameters["TYPE_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.TYPE_CODE).ToArray();
                        cmd.Parameters["SOVANBAN_CODE"].Value = data.Select(dcm_template_sinhso => dcm_template_sinhso.SOVANBAN_CODE).ToArray();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to DCM_QUYTAC_NHAYSO : " + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            dcm_SoVanBans.Clear();
            dcm_QUYTAC_NHAYSOs.Clear();
            dcm_SOVB_TEMPLATESINHSOs.Clear();
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb)
        {
        }
    }
}
