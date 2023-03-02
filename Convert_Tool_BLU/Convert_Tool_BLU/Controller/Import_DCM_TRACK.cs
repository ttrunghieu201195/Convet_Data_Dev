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
    class Import_DCM_TRACK : Import_Abstract
    {
        private List<Dcm_Track> dcm_Tracks = new List<Dcm_Track>();

        public List<Dcm_Track> GetDcm_Tracks()
        {
            return dcm_Tracks;
        }

        protected override string getDataQuery(string fromSchema)
        {
            return string.Format(Constants.SQL_SELECT_DCM_TRACK, fromSchema);
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
            try
            {
                string query = Constants.sql_insert_dcm_track;
                if (dcm_Tracks.Count > 0)
                {
                    Stopwatch timer = new Stopwatch();
                    Console.WriteLine("Total data to dcm_assign: " + dcm_Tracks.Count);

                    List<List<Dcm_Track>> splited_data = Common.SplitList(dcm_Tracks);
                    Console.WriteLine("Total splited data to dcm_assign: " + splited_data.Count);
                    foreach (List<Dcm_Track> data in splited_data)
                    {
                        timer.Start();
                        OracleCommand cmd = oracleConnection.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.ArrayBindCount = data.Count;

                        cmd.CommandText = query;

                        cmd.Parameters.Add("ID", OracleDbType.Int64);
                        cmd.Parameters.Add("DOC_ID", OracleDbType.Int64);
                        cmd.Parameters.Add("SCHEMA_ID", OracleDbType.Varchar2);
                        cmd.Parameters.Add("DOC_ID_SOURCE", OracleDbType.Int64);
                        cmd.Parameters.Add("SCHEMA_ID_SOURCE", OracleDbType.Varchar2);
                        cmd.Parameters.Add("DATE_INS", OracleDbType.Date);
                        cmd.Parameters.Add("PARENT", OracleDbType.Varchar2);
                        cmd.Parameters.Add("CHILD", OracleDbType.Varchar2);

                        cmd.Parameters["ID"].Value = data.Select(dcm_track => dcm_track.id).ToArray();
                        cmd.Parameters["DOC_ID"].Value = data.Select(dcm_track => dcm_track.doc_id).ToArray();
                        cmd.Parameters["SCHEMA_ID"].Value = data.Select(dcm_track => dcm_track.schema_id).ToArray();
                        cmd.Parameters["DOC_ID_SOURCE"].Value = data.Select(dcm_track => dcm_track.doc_id_source).ToArray();
                        cmd.Parameters["SCHEMA_ID_SOURCE"].Value = data.Select(dcm_track => dcm_track.schema_id_source).ToArray();
                        cmd.Parameters["DATE_INS"].Value = data.Select(dcm_track => dcm_track.date_ins).ToArray();
                        cmd.Parameters["PARENT"].Value = data.Select(dcm_track => dcm_track.parent).ToArray();
                        cmd.Parameters["CHILD"].Value = data.Select(dcm_track => dcm_track.child).ToArray();

                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        timer.Stop();
                        Console.WriteLine("Imported data to dcm_assign: " + affectedRows + "/" + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
                        timer.Reset();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void ParseData(DataRow row)
        {
            Dcm_Track dcm_Track = new Dcm_Track();
            try
            {
                dcm_Track.id = long.Parse(row["ID"].ToString());
                if (!string.IsNullOrEmpty(row["DOC_ID"].ToString()))
                {
                    dcm_Track.doc_id = long.Parse(row["DOC_ID"].ToString());
                }
                if (!string.IsNullOrEmpty(row["SCHEMA_ID"].ToString()))
                {
                    dcm_Track.schema_id = row["SCHEMA_ID"].ToString();
                }
                if (!string.IsNullOrEmpty(row["DOC_ID_SOURCE"].ToString()))
                {
                    dcm_Track.doc_id_source = long.Parse(row["DOC_ID_SOURCE"].ToString());
                }
                if (!string.IsNullOrEmpty(row["SCHEMA_ID_SOURCE"].ToString()))
                {
                    dcm_Track.schema_id_source = row["SCHEMA_ID_SOURCE"].ToString();
                }
                if (!string.IsNullOrEmpty(row["DATE_INS"].ToString()))
                {
                    dcm_Track.date_ins = (DateTime)row["DATE_INS"];
                }
                if (!string.IsNullOrEmpty(row["PARENT"].ToString()))
                {
                    dcm_Track.parent = row["PARENT"].ToString();
                }
                if (!string.IsNullOrEmpty(row["CHILD"].ToString()))
                {
                    dcm_Track.child = row["CHILD"].ToString();
                }
                dcm_Tracks.Add(dcm_Track);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            dcm_Tracks.Clear();
        }
    }
}
