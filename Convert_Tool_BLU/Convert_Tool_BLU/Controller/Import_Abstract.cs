using Convert_Data.Models;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace Convert_Data
{
    abstract class Import_Abstract
    {
        protected bool exported_error = false;
        public void exportdataFromPostgres(NpgsqlConnection postgresConnection, CommandType commandType, string queryStr, Common.VB_TYPE type_vb)
        {
            try
            {
                exported_error = false;
                var cmd = postgresConnection.CreateCommand();
                cmd.CommandText = queryStr;
                
                if (commandType == CommandType.StoredProcedure)
                {
                    string query = queryStr.Substring(0, queryStr.IndexOf("("));
                    cmd.CommandText = query;
                    Console.WriteLine(query);
                    string param = queryStr.Substring(queryStr.IndexOf("(") + 1, queryStr.IndexOf(")") - (queryStr.IndexOf("(") + 1));
                    int donvi_code = int.Parse(param.Split(';')[0]);
                    string years = param.Split(';')[1];
                    Console.WriteLine(param.Split(';')[0] + " - " + param.Split(';')[1]);
                    
                    cmd.Parameters.AddWithValue("organizationid", donvi_code);
                    cmd.Parameters.AddWithValue("yeardocument", years);
                }
                cmd.CommandType = commandType;

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

        /*public void exportdataFromPostgres(NpgsqlConnection postgresConnection, string query, Common.VB_TYPE type_vb, DataTable dcm_type)
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
        }*/
        /*public void ExportData<T>(List<T> data, DataTable dcm_type) where T : IDCM_
        {
            ExportData_Protected(data, dcm_type);
        }*/

        public void ExportData<T>(List<T> data, DataTable dcm_type) where T:class
        {
            foreach (T t in data) {
                ParseData<T>(t, dcm_type);
            }
        }

        /*public void ExportData<T>(List<T> data) where T : class
        {
            foreach (T t in data)
            {
                //ParseData<T>(t);
            }
        }*/

        public void ExportData(OracleConnection connection, string query)
        {
            OracleCommand cmd = connection.CreateCommand();
            try
            {
                exported_error = false;
                cmd.CommandText = query;
                OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
                resetListData();
                foreach (DataRow row in dataTable.Rows)
                {
                    ParseData(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                exported_error = true;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        protected abstract void ParseData(DataRow row);
        protected abstract void ParseData<T>(T data, DataTable dcm_type) where T : class;
        //protected abstract void ParseData<T>(T data) where T : class;
        protected abstract void resetListData();
        protected abstract void ParseData(DataRow row, Common.VB_TYPE type_vb);
        //protected abstract void ParseData(DataRow row, Common.VB_TYPE type_vb, DataTable dcm_type);

        public bool isExportError()
        {
            return exported_error;
        }
    }
}
