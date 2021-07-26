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
    class Import_FEM_FILE : Import_Abstract
    {
        // list fem_file
        private List<Fem_File> fem_Files = new List<Fem_File>();
        //public static long SEQ_FEM_FILE;
        protected override void ParseData(DataRow row)
        {
            Fem_File fem_File = new Fem_File();
            fem_File.id = long.Parse(row["ID"].ToString());
            fem_File.hdd_file = row["HDD_FILE"].ToString();
            fem_File.name = row["NAME"].ToString();
            fem_Files.Add(fem_File);
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
            fem_Files.Clear();
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.sql_insert_fem_file, toSchema);
                if (fem_Files.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to Fem_File: " + fem_Files.Count);

                    List<List<Fem_File>> splited_data = Common.SplitList(fem_Files);
                    Console.WriteLine("Total splited data to Fem_File: " + splited_data.Count);

                    foreach (List<Fem_File> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("FILE_TYPE_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("NAME", OracleDbType.Varchar2);
                        cmd.Parameters.Add("HDD_FILE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("DESCRIBE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("FILE_SIZE", OracleDbType.Int64);
                        cmd.Parameters.Add("IS_PRIVATE_FILE", OracleDbType.Int64);
                        cmd.Parameters.Add("CREATOR", OracleDbType.Varchar2);
                        cmd.Parameters.Add("IS_DELETED", OracleDbType.Int64);

                        cmd.Parameters["ID"].Value = data.Select(fem_file => fem_file.id).ToArray();
                        cmd.Parameters["FILE_TYPE_ID"].Value = data.Select(fem_file => fem_file.file_type_id).ToArray();
                        cmd.Parameters["NAME"].Value = data.Select(fem_file => fem_file.name).ToArray();
                        cmd.Parameters["HDD_FILE"].Value = data.Select(fem_file => fem_file.hdd_file).ToArray();
                        cmd.Parameters["DESCRIBE"].Value = data.Select(fem_file => fem_file.description).ToArray();
                        cmd.Parameters["FILE_SIZE"].Value = data.Select(fem_file => fem_file.file_size).ToArray();
                        cmd.Parameters["IS_PRIVATE_FILE"].Value = data.Select(fem_file => fem_file.is_private_file).ToArray();
                        cmd.Parameters["CREATOR"].Value = data.Select(fem_file => fem_file.creator).ToArray();
                        cmd.Parameters["IS_DELETED"].Value = data.Select(fem_file => fem_file.is_delete).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        timer.Stop();
                        Console.WriteLine("Imported data to Fem_File: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
                        timer.Reset();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override string getDataQuery(string fromSchema)
        {
            return string.Format(Constants.SQL_SELECT_ALL_DATA, fromSchema, Common.TABLE.FEM_FILE);
        }
    }
}
