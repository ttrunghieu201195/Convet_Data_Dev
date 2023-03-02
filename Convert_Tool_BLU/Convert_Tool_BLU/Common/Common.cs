using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Convert_Data.Controller;
using Npgsql;
using System.Data;

namespace Convert_Data
{
    class Common
    {
        public enum VB_TYPE
        {
            NONE = 0,
            VB_DEN = 1,
            VB_DI = 2
        }

        public enum TABLE
        {
            DCM_CONFIDENTIAL,
            DCM_PRIORITY,
            DCM_TYPE,
            DCM_LINHVUC,
            DCM_SOVANBAN,
            DCM_QUYTAC_NHAYSO,
            DCM_SOVB_TEMPLATESINHSO,
            DCM_DOC,
            DCM_DOC_RELATION,
            FEM_FILE,
            DCM_ATTACH_FILE,
            DCM_TRACK,
            DCM_ACTIVITI_LOG,
            DCM_ASSIGN,
            DCM_DONVI_NHAN,
            DCM_LOG,
            DCM_LOG_READ
        }

        public enum SCHEMA
        {
            CLOUD_ADMIN_DEV_BLU_2,
            CLOUD_ADMIN
        }

        public static string[] table_arr = {/*"DCM_PRIORITY", "DCM_CONFIDENTIAL",*/ "DCM_TYPE", "DCM_LINHVUC"
                , "DCM_SOVANBAN", "DCM_QUYTAC_NHAYSO", "DCM_SOVB_TEMPLATESINHSO"
                , "DCM_DOC", "DCM_DOC_RELATION", "FEM_FILE", "DCM_ATTACH_FILE", "DCM_TRACK"
                , "DCM_ACTIVITI_LOG", "DCM_ASSIGN", "DCM_DONVI_NHAN"
                , "DCM_LOG", "DCM_LOG_READ" };

        //public static string[] table_arr = { "DCM_TRACK" };

        public static long GetCurrentSeq(OracleConnection connection, string schema, string seqName)
        {
            string sql = "select " + schema + seqName + ".NextVal from dual";
            OracleCommand objCommand1 = new OracleCommand(sql, connection);
            decimal result = (decimal)objCommand1.ExecuteScalar();
            return long.Parse(result.ToString()) > Constants.INCREASEID_OTHERS ? long.Parse(result.ToString()) : Constants.INCREASEID_OTHERS;
        }

        public static long GetMaxID(OracleConnection connection, string schema, TABLE table)
        {
            decimal result = 0;
            try
            {
                string sql = "";
                if (table == Common.TABLE.DCM_ATTACH_FILE)
                {
                    sql = "SELECT MAX(ATTACHMENT_ID) FROM " + schema + table;
                } else
                {
                    sql = "SELECT MAX(ID) FROM " + schema + table;
                }
                OracleCommand cmd = new OracleCommand(sql, connection);
                if (!string.IsNullOrEmpty(cmd.ExecuteScalar().ToString()))
                {
                    result = (decimal)cmd.ExecuteScalar();
                }                
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            result = result > Constants.INCREASEID_OTHERS ? result : Constants.INCREASEID_OTHERS;
            return long.Parse(result.ToString());
        }

        //public static long GetTableID(OracleConnection connection, string schema, TABLE)

        /*public static void UpdateSeq(OracleConnection connection, string schema, string seqName, Int64 new_value)
        {
            OracleTransaction oracleTransaction = connection.BeginTransaction();
            try
            { 
                string sql = "DROP SEQUENCE " + schema + "." + seqName;
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.Transaction = oracleTransaction;
                cmd.ExecuteNonQuery();
                sql = "CREATE SEQUENCE " + schema + "." + seqName + " START WITH " + new_value;
                cmd = new OracleCommand(sql, connection);
                cmd.ExecuteNonQuery();
                oracleTransaction.Commit();
                
            } catch (Exception ex)
            {
                oracleTransaction.Rollback();
                Console.WriteLine(ex.Message);
            }
            
        }*/

        public static string Escape_String(string input)
        {
            input = input.Replace("&", "&amp;");
            input = input.Replace("'", "&apos;");
            input = input.Replace("\"", "&quot; ");
            input = input.Replace("<", "&lt;");
            input = input.Replace(">", "&gt;");
            input = input.Replace("¢", "&cent;");
            input = input.Replace("£", "&pound;");
            input = input.Replace("¥", "&yen;");
            input = input.Replace("€", "&euro;");
            input = input.Replace("©", "&copy;");
            input = input.Replace("®", "&reg;");
            input = input.Replace("=", "&equals;");
            input = input.Replace(",", "&comma;");
            input = input.Replace("\'", "\\'");
            input = input.Replace("\r\n", "<br>");
            input = input.Replace("\n", "<br>");
            return input;
        }

        public static string getFileNameFromHddFile(string str)
        {
            str = str.Substring(str.IndexOf("@") + 1, str.Length - str.IndexOf("@") - 1);
            if (Regex.IsMatch(str, @"#\d+"))
            {
                str = str.Substring(0, str.LastIndexOf("#")) + str.Substring(str.LastIndexOf("."), (str.Length - str.LastIndexOf(".")));
            }
            return str;
        }

        public static string GetExportedDataYears(string startYear, string endYear)
        {
            string years = "";
            int start = int.Parse(startYear);
            int end = int.Parse(endYear);
            while (start < end)
            {
                years += start + ",";
                start++;
            }
            years += end;

            return years;
        }

        public static string getRoleCode_NguoiNhan(string vaitro)
        {
            switch (vaitro)
            {
                case "0":
                    vaitro = "THONG_BAO";
                    break;
                case "1":
                    vaitro = "INVOLVED";
                    break;
                case "2":
                    vaitro = "RECEIVE";
                    break;
                default:
                    vaitro = "INVOLVED";
                    break;
            }
            return vaitro;
        }

        public static string getRoleCode_DonVi(string vaitro)
        {
            switch (vaitro)
            {
                case "0":
                    vaitro = "INVOLVED";
                    break;
                case "1":
                    vaitro = "RECEIVE";
                    break;
                case "2":
                    vaitro = "THONG_BAO";
                    break;
                default:
                    vaitro = "INVOLVED";
                    break;
            }
            return vaitro;
        }

        /*public static void UpdateSeqToDB(OracleConnection oracleConnection, Configs configs)
        {
            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DOC_RELATION, ++Import_VanBan.SEQ_DCM_DOC_RELATION - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_FEM_FILE, ++Import_VanBan.SEQ_FEM_FILE - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ATTACH_FILE, ++Import_VanBan.SEQ_DCM_ATTACH_FILE - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, "CLOUD_ADMIN_DEV_BLU_2", Constants.SEQ_DCM_TRACK, ++Import_VanBan.SEQ_DCM_TRACK - Constants.INCREASEID_OTHERS);

            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ACTIVITI_LOG, ++Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ASSIGN, ++Import_VanBan_Flow.SEQ_DCM_ASSIGN - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DONVI_NHAN, ++Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN - Constants.INCREASEID_OTHERS);

            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_LOG, ++Import_VanBan_Log.SEQ_DCM_LOG - Constants.INCREASEID_OTHERS);
            //UpdateSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_LOG_READ, ++Import_VanBan_Log.SEQ_DCM_LOG_READ - Constants.INCREASEID_OTHERS);

            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_SOVB_TEMPLATESINHSO, ++Import_DCM_SOVB_TEMPLATESINHSO.SEQ_DCM_SOVB_TEMPLATESINHSO - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_QUYTAC_NHAYSO, ++Import_DCM_QUYTAC_NHAYSO.SEQ_DCM_QUYTAC_NHAYSO - Constants.INCREASEID_OTHERS);
        }*/

        public static void UpdateSeqFromProcedure(OracleConnection oracleConnection, string schema, string seq_list)
        {
            try
            {
                OracleCommand cmd = oracleConnection.CreateCommand();
                cmd.CommandText = "CLOUD_ADMIN_DEV_BLU_2.SF_UPDATE_SEQ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ps_schema", OracleDbType.Varchar2).Value = schema;
                cmd.Parameters.Add("json", OracleDbType.Varchar2).Value = seq_list;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Console.WriteLine("Updated seq of " + seq_list);
            } catch (Exception ex)
            {
                Console.WriteLine("UpdateSeqFromProcedure: " + ex.Message);
            }
        }

        public static void TestCallProcFromPostgres(NpgsqlConnection connection)
        {
            NpgsqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "luong_vb_den_sobannganh";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("organizationid", NpgsqlTypes.NpgsqlDbType.Bigint).Value = 27;
            cmd.Parameters.Add("yeardocument", NpgsqlTypes.NpgsqlDbType.Text).Value = "'2014','2015'";

            /*NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataSet);

            dataTable = dataSet.Tables[0];*/
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Console.WriteLine(dr[0]);
            }

            //resetListData();
            /*foreach (DataRow row in dataTable.Rows)
            {
                ParseData(row, type_vb, dcm_type);
            }*/
        }

        public static bool DeleteOldDataFromTable(OracleConnection connection)
        {
            try
            {
                OracleDataAdapter da = new OracleDataAdapter();
                OracleCommand cmd = connection.CreateCommand();
                cmd.CommandText = "CLOUD_ADMIN_DEV_BLU_4.DELETE_DATA_FROM_TABLE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("ps_schema", OracleDbType.Varchar2).Value = "CLOUD_ADMIN_DEV_BLU_4";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        
        public static void _InitialSeqFromDB(OracleConnection oracleConnection, Configs configs)
        {
            Import_VanBan.SEQ_DCM_DOC_RELATION = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DOC_RELATION);
            Import_VanBan.SEQ_FEM_FILE = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_FEM_FILE);
            Import_VanBan.SEQ_DCM_ATTACH_FILE = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ATTACH_FILE);
            Import_VanBan.SEQ_DCM_TRACK = GetCurrentSeq(oracleConnection, "CLOUD_ADMIN_DEV_BLU_2.", Constants.SEQ_DCM_TRACK);
            
            Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ACTIVITI_LOG);
            Import_VanBan_Flow.SEQ_DCM_ASSIGN = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ASSIGN);
            Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DONVI_NHAN);
            
            Import_VanBan_Log.SEQ_DCM_LOG = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_LOG);
            //Import_VanBan_Log.SEQ_DCM_LOG_READ = getCurrentSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_LOG_READ);

            Import_DCM_SOVB_TEMPLATESINHSO.SEQ_DCM_SOVB_TEMPLATESINHSO = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_SOVB_TEMPLATESINHSO);
            Import_DCM_QUYTAC_NHAYSO.SEQ_DCM_QUYTAC_NHAYSO = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_QUYTAC_NHAYSO);
        }

        public static void InitialSeqFromDB(OracleConnection oracleConnection, Configs configs)
        {
            Import_VanBan.SEQ_DCM_DOC_RELATION = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_DOC_RELATION);
            Import_VanBan.SEQ_FEM_FILE = GetMaxID(oracleConnection, configs.Schema, TABLE.FEM_FILE);
            Import_VanBan.SEQ_DCM_ATTACH_FILE = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_ATTACH_FILE);
            Import_VanBan.SEQ_DCM_TRACK = GetMaxID(oracleConnection, "CLOUD_ADMIN_DEV_BLU_2.", TABLE.DCM_TRACK);

            Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_ACTIVITI_LOG);
            Import_VanBan_Flow.SEQ_DCM_ASSIGN = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_ASSIGN);
            Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_DONVI_NHAN);

            Import_VanBan_Log.SEQ_DCM_LOG = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_LOG);
            //Import_DCM_LOG_READ.SEQ_DCM_LOG_READ = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_LOG_READ);

            Import_DCM_SOVB_TEMPLATESINHSO.SEQ_DCM_SOVB_TEMPLATESINHSO = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_SOVB_TEMPLATESINHSO);
            Import_DCM_QUYTAC_NHAYSO.SEQ_DCM_QUYTAC_NHAYSO = GetMaxID(oracleConnection, configs.Schema, TABLE.DCM_QUYTAC_NHAYSO);
        }

        public static List<List<T>> SplitList<T>(List<T> data) where T:class
        {
            int totalRecords = data.Count;
            int totalSplittedRecords = 10000;

            int numOfSubList = totalRecords / totalSplittedRecords + (totalRecords % totalSplittedRecords > 0 ? 1 : 0);

            List<List<T>> list_obj = new List<List<T>>();
            for (int  i = 0; i < numOfSubList; i++)
            {
                list_obj.Add(data.GetRange(i * totalSplittedRecords, (totalRecords - i * totalSplittedRecords) > totalSplittedRecords ? totalSplittedRecords : (totalRecords - i * totalSplittedRecords)));
            }

            return list_obj;
        }

        public static void DeleteTable(OracleConnection connection, string schema, TABLE table)
        {
            OracleCommand cmd = connection.CreateCommand();
            try
            {
                string query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, table.Equals(TABLE.DCM_TRACK) ? SCHEMA.CLOUD_ADMIN_DEV_BLU_2 + "." : schema, table);
                if (table.Equals(TABLE.DCM_TRACK))
                {
                    query += string.Format(" WHERE SCHEMA_ID = '{0}'", schema);
                }
                cmd.CommandText = query;
                int totalRecs = int.Parse(((decimal)cmd.ExecuteScalar()).ToString());
                Console.WriteLine("Total records in " + table + ": " + totalRecs);
                int threshold = 10000;
                int count_loop = totalRecs / threshold + (totalRecs % threshold > 0 ? 1 : 0);
                query = string.Format(Constants.sql_delete_table, table.Equals(TABLE.DCM_TRACK) ? SCHEMA.CLOUD_ADMIN_DEV_BLU_2 + "." : schema, table, threshold);
                if (table.Equals(TABLE.DCM_TRACK))
                {
                    query += string.Format(" AND SCHEMA_ID = '{0}'", schema);
                }
                for (int i = 0; i < count_loop; i++)
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Deleted " + threshold + " records in " + table);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cmd.Dispose();

            }
        }

        public static DataTable GetDanhMucDonvi(NpgsqlConnection connection, string query)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataSet);

            dataTable = dataSet.Tables[0];

            return dataTable;
        }

        public static DataTable GetSchemasFromBLU(OracleConnection connection, string query)
        {
            OracleCommand cmd = new OracleCommand(query, connection);
            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            return dataSet.Tables[0];
        }

        public static DataTable GetDcm_Type(OracleConnection connection, string schema, string query)
        {
            OracleCommand cmd = new OracleCommand(string.Format(query, schema), connection);

            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            return dataTable;
        }

        public static List<long> GetDataIDFromTable(OracleConnection connection, string query)
        {
            List<long> dataList = new List<long>();
            OracleCommand cmd = new OracleCommand(query, connection);
            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            foreach (DataRow row in dataTable.Rows)
            {
                if (query.IndexOf("DCM_ATTACH_FILE") > -1 && !string.IsNullOrEmpty(row["ATTACHMENT_ID"].ToString()))
                {
                    dataList.Add(long.Parse(row["ATTACHMENT_ID"].ToString()));
                } else if (!string.IsNullOrEmpty(row["ID"].ToString()))
                {
                    dataList.Add(long.Parse(row["ID"].ToString()));
                }
            }

            return dataList;
        }
    }
}
