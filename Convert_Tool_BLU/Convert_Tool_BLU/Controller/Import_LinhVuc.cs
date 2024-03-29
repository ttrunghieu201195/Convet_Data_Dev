﻿using Convert_Data.Models;
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
    class Import_LinhVuc : Import_Abstract
    {
        private List<DCM_LINHVUC> DCM_LINHVUCs = new List<DCM_LINHVUC>();

        public List<DCM_LINHVUC> GetDCM_LINHVUCs()
        {
            return DCM_LINHVUCs;
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb)
        {
            DCM_LINHVUC dcm_LINHVUC = new DCM_LINHVUC();
            string cell_value = row["ID"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_LINHVUC.ID = long.Parse(cell_value) + Constants.INCREASEID_OTHERS;
            }

            cell_value = row["ten_linhvuc"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_LINHVUC.NAME = cell_value;
            }

            cell_value = row["stt"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_LINHVUC.STT_HIENTHI = int.Parse(cell_value);
            }

            cell_value = row["code"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_LINHVUC.CODE = cell_value;
            }
            DCM_LINHVUCs.Add(dcm_LINHVUC);
        }

        /*protected override void ParseData(DataRow row, Common.VB_TYPE type_vb, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }*/

        protected override void resetListData()
        {
            DCM_LINHVUCs.Clear();
        }

        public void insert_Dcm_Linhvuc(OracleConnection oracleConnection, string schema, string query)
        {
            try
            {
                if (DCM_LINHVUCs.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_linhvuc: " + DCM_LINHVUCs.Count);

                    List<List<DCM_LINHVUC>> splited_data = Common.SplitList(DCM_LINHVUCs);
                    Console.WriteLine("Total splited data to dcm_linhvuc: " + splited_data.Count);

                    foreach (List<DCM_LINHVUC> data in splited_data)
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

                        cmd.Parameters["ID"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.ID).ToArray();
                        cmd.Parameters["NAME"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NAME).ToArray();
                        cmd.Parameters["CODE"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.CODE).ToArray();
                        cmd.Parameters["NGUOI_TAO"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NGUOI_TAO).ToArray();
                        cmd.Parameters["NGAY_TAO"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NGAY_TAO).ToArray();
                        cmd.Parameters["STT_HIENTHI"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.STT_HIENTHI).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_linhvuc: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
                        timer.Reset();
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void ParseData(DataRow row)
        {

            DCM_LINHVUC dcm_LINHVUC = new DCM_LINHVUC();
            string cell_value = row["ID"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_LINHVUC.ID = long.Parse(cell_value);
            }

            cell_value = row["NAME"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_LINHVUC.NAME = cell_value;
            }

            cell_value = row["STT_HIENTHI"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_LINHVUC.STT_HIENTHI = int.Parse(cell_value);
            }

            cell_value = row["CODE"].ToString();
            if (!string.IsNullOrEmpty(cell_value))
            {
                dcm_LINHVUC.CODE = cell_value;
            }
            DCM_LINHVUCs.Add(dcm_LINHVUC);
        }

        protected override void ParseData<T>(T data, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.SQL_INSERT_DCM_LINHVUC, toSchema);
                if (DCM_LINHVUCs.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_linhvuc: " + DCM_LINHVUCs.Count);

                    List<List<DCM_LINHVUC>> splited_data = Common.SplitList(DCM_LINHVUCs);
                    Console.WriteLine("Total splited data to dcm_linhvuc: " + splited_data.Count);

                    foreach (List<DCM_LINHVUC> data in splited_data)
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

                        cmd.Parameters["ID"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.ID).ToArray();
                        cmd.Parameters["NAME"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NAME).ToArray();
                        cmd.Parameters["CODE"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.CODE).ToArray();
                        cmd.Parameters["NGUOI_TAO"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NGUOI_TAO).ToArray();
                        cmd.Parameters["NGAY_TAO"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.NGAY_TAO).ToArray();
                        cmd.Parameters["STT_HIENTHI"].Value = data.Select(dcm_linhvuc => dcm_linhvuc.STT_HIENTHI).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_linhvuc: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            return string.Format(Constants.SQL_SELECT_ALL_DATA, fromSchema, Common.TABLE.DCM_LINHVUC);
        }
    }
}
