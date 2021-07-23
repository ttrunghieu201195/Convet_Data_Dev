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
    class Import_DCM_DOC_RELATION : Import_Abstract
    {
        // list dcm_doc_relation
        private List<Dcm_Doc_Relation> dcm_Doc_Relations = new List<Dcm_Doc_Relation>();
        //public static long SEQ_DCM_DOC_RELATION;
        protected override void ParseData(DataRow row)
        {
            Dcm_Doc_Relation dcm_Doc_Relation = new Dcm_Doc_Relation();
            dcm_Doc_Relation.id = long.Parse(row["ID"].ToString());
            dcm_Doc_Relation.dcm_id = long.Parse(row["DCM_ID"].ToString());
            dcm_Doc_Relation.dcm_document_id = long.Parse(row["DCM_DOCUMENT_ID"].ToString());
            dcm_Doc_Relations.Add(dcm_Doc_Relation);
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
            dcm_Doc_Relations.Clear();
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = string.Format(Constants.sql_insert_dcm_doc_relation, toSchema);
                if (dcm_Doc_Relations.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to Dcm_Doc_Relation: " + dcm_Doc_Relations.Count);

                    List<List<Dcm_Doc_Relation>> splited_data = Common.SplitList(dcm_Doc_Relations);
                    Console.WriteLine("Total splited data to Dcm_Doc_Relation: " + splited_data.Count);

                    foreach (List<Dcm_Doc_Relation> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("DCM_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("DCM_DOCUMENT_ID", OracleDbType.Int64);

                        cmd.Parameters["ID"].Value = data.Select(dcm_doc_relation => dcm_doc_relation.id).ToArray();
                        cmd.Parameters["DCM_ID"].Value = data.Select(dcm_doc_relation => dcm_doc_relation.dcm_id).ToArray();
                        cmd.Parameters["DCM_DOCUMENT_ID"].Value = data.Select(dcm_doc_relation => dcm_doc_relation.dcm_document_id).ToArray();

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        timer.Stop();
                        Console.WriteLine("Imported data to Dcm_Doc_Relation: " + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
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
            return string.Format(Constants.SQL_SELECT_ALL_DATA, fromSchema, Common.TABLE.DCM_DOC_RELATION);
        }
    }
}
