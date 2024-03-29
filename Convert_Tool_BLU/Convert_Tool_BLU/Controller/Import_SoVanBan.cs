﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Convert_Data.Models;

namespace Convert_Data.Controller
{
    class Import_SoVanBan : Import_Abstract
    {
        private List<Dcm_SoVanBan> dcm_SoVanBans = new List<Dcm_SoVanBan>();

        public List<Dcm_SoVanBan> GetDcm_SoVanBans()
        {
            return dcm_SoVanBans;
        }

        public void insert_Dcm_SoVanBan(OracleConnection oracleConnection, string query)
        {
            try
            {
                if (dcm_SoVanBans.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to DCM_SOVANBAN: " + dcm_SoVanBans.Count);

                    List<List<Dcm_SoVanBan>> splited_data = Common.SplitList(dcm_SoVanBans);
                    Console.WriteLine("Total splited data to DCM_SOVANBAN: " + splited_data.Count);

                    foreach (List<Dcm_SoVanBan> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

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

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_log: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            /*dcm_QUYTAC_NHAYSOs.Clear();
            dcm_SOVB_TEMPLATESINHSOs.Clear();*/
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb)
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
        }

        protected override void ParseData(DataRow row)
        {
            Dcm_SoVanBan dcm_SoVanBan = new Dcm_SoVanBan();
            string cell_value = "";
            cell_value = row["ID"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.id = long.Parse(cell_value);
            }

            cell_value = row["NAME"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.name = cell_value.Trim();
            }

            cell_value = row["MA_LOAI_SO"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.ma_loai_so = cell_value;
            }

            cell_value = row["NGAY_BATDAU_SUDUNG"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                //DateTime ngay_mo_so = DateTime.ParseExact(cell_value.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_SoVanBan.ngay_batdau_sudung = (DateTime)row["NGAY_BATDAU_SUDUNG"];
                    //ngay_mo_so;
            }

            cell_value = row["NGAY_DONGSO"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                //DateTime ngay_het_hieuluc = DateTime.ParseExact(cell_value.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_SoVanBan.ngay_dongso = (DateTime)row["NGAY_DONGSO"];
                    //ngay_het_hieuluc;
            }

            cell_value = row["NGUOI_QUANTRI"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.nguoi_quantri = cell_value;
            }

            cell_value = row["NGUOI_THEODOI"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.nguoi_theodoi = cell_value;
            }

            cell_value = row["UNIT_CODE"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.unit_code = cell_value;
            }

            cell_value = row["STT_HIENTHI"].ToString();
            if (cell_value != null && cell_value != string.Empty)
            {
                dcm_SoVanBan.stt_hienthi = int.Parse(cell_value);
            }

            cell_value = row["CODE"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_SoVanBan.code = cell_value;
            }            

            dcm_SoVanBans.Add(dcm_SoVanBan);
            //appendToRelatedTable(dcm_type, dcm_SoVanBan.code);
        }

        protected override void ParseData<T>(T data, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.SQL_INSERT_DCM_SOVANBAN, toSchema);
                if (dcm_SoVanBans.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to DCM_SOVANBAN: " + dcm_SoVanBans.Count);

                    List<List<Dcm_SoVanBan>> splited_data = Common.SplitList(dcm_SoVanBans);
                    Console.WriteLine("Total splited data to DCM_SOVANBAN: " + splited_data.Count);

                    foreach (List<Dcm_SoVanBan> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

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

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_log: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            return string.Format(Constants.SQL_SELECT_DCM_SOVANBAN, fromSchema);
        }
    }
}
