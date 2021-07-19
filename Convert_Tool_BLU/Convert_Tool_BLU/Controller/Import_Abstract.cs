﻿using Npgsql;
using System;
using System.Data;

namespace Convert_Data
{
    abstract class Import_Abstract
    {
        protected bool exported_error = false;
        public void exportdataFromPostgres(NpgsqlConnection postgresConnection, string query, Common.VB_TYPE type_vb)
        {
            try
            {
                exported_error = false;
                string proc = query.Substring(0, query.IndexOf("("));
                Console.WriteLine(proc);
                string param = query.Substring(query.IndexOf("(") + 1, query.IndexOf(")") - (query.IndexOf("(") + 1));
                int donvi_code = int.Parse(param.Split(';')[0]);
                string years = param.Split(';')[1];
                Console.WriteLine(param.Split(';')[0] + " - " + param.Split(';')[1]);
                var cmd = new NpgsqlCommand(proc, postgresConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("organizationid", donvi_code);
                cmd.Parameters.AddWithValue("yeardocument", years);

                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];
                resetListData();
                foreach (DataRow row in dataTable.Rows)
                {
                    ParseData(row, type_vb);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                exported_error = true;
            }
        }

        public void exportdataFromPostgres(NpgsqlConnection postgresConnection, string query, Common.VB_TYPE type_vb, DataTable dcm_type)
        {
            try
            {
                exported_error = false;
                var cmd = new NpgsqlCommand(query, postgresConnection);

                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];
                resetListData();
                foreach (DataRow row in dataTable.Rows)
                {
                    ParseData(row, type_vb, dcm_type);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                exported_error = true;
            }
        }

        protected abstract void resetListData();
        protected abstract void ParseData(DataRow row, Common.VB_TYPE type_vb);
        protected abstract void ParseData(DataRow row, Common.VB_TYPE type_vb, DataTable dcm_type);

        public bool isExportError()
        {
            return exported_error;
        }
    }
}
