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
    class Import_DCM_ATTACH_FILE : Import_Abstract
    {
        // list dcm_attach_file
        private List<Dcm_Attach_File> dcm_Attach_Files = new List<Dcm_Attach_File>();
        //public static long SEQ_DCM_ATTACH_FILE;
        public List<Dcm_Attach_File> GetDcm_Attach_Files()
        {
            return dcm_Attach_Files;
        }
        protected override void ParseData(DataRow row)
        {
            Dcm_Attach_File dcm_Attach_File = new Dcm_Attach_File();
            dcm_Attach_File.id = long.Parse(row["ATTACHMENT_ID"].ToString());
            dcm_Attach_File.id_vb = long.Parse(row["DOC_ID"].ToString());
            dcm_Attach_File.file_id = long.Parse(row["FILE_ID"].ToString());
            dcm_Attach_Files.Add(dcm_Attach_File);
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
            dcm_Attach_Files.Clear();
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.sql_insert_dcm_attach_file, toSchema);
                if (dcm_Attach_Files.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_attach_file: " + dcm_Attach_Files.Count);

                    List<List<Dcm_Attach_File>> splited_data = Common.SplitList(dcm_Attach_Files);
                    Console.WriteLine("Total splited data to dcm_attach_file: " + splited_data.Count);

                    foreach (List<Dcm_Attach_File> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ATTACHMENT_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("DOC_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("TRANG_THAI", OracleDbType.Int64);
                        cmd.Parameters.Add("FILE_ID", OracleDbType.Int64);

                        cmd.Parameters["ATTACHMENT_ID"].Value = data.Select(dcm_doc_attach_file => dcm_doc_attach_file.id).ToArray();
                        cmd.Parameters["DOC_ID"].Value = data.Select(dcm_doc_attach_file => dcm_doc_attach_file.id_vb).ToArray();
                        cmd.Parameters["TRANG_THAI"].Value = data.Select(dcm_doc_attach_file => dcm_doc_attach_file.trang_thai).ToArray();
                        cmd.Parameters["FILE_ID"].Value = data.Select(dcm_doc_attach_file => dcm_doc_attach_file.file_id).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        timer.Stop();
                        Console.WriteLine("Imported data to Dcm_Attach_File: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            return string.Format(Constants.SQL_SELECT_ALL_DATA, fromSchema, Common.TABLE.DCM_ATTACH_FILE);
        }
    }
}
