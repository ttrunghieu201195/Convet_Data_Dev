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
            DCM_DOC,
            DCM_DOC_RELATION,
            FEM_FILE,
            DCM_ATTACH_FILE,
            DCM_ACTIVITI_LOG,
            DCM_ASSIGN,
            DCM_DONVI_NHAN,
            DCM_LOG,
            DCM_LOG_READ
        }

        public static string[] table_arr = {"DCM_DOC", "DCM_DOC_RELATION", "FEM_FILE", "DCM_ATTACH_FILE", "DCM_ACTIVITI_LOG", "DCM_ASSIGN", "DCM_DONVI_NHAN", "DCM_LOG", "DCM_LOG_READ" };

        public static long GetCurrentSeq(OracleConnection connection, string schema, string seqName)
        {
            string sql = "select " + schema + "." + seqName + ".NextVal from dual";
            OracleCommand objCommand1 = new OracleCommand(sql, connection);
            decimal result = (decimal)objCommand1.ExecuteScalar();
            return long.Parse(result.ToString());
        }

        public static void UpdateSeq(OracleConnection connection, string schema, string seqName, Int64 new_value)
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
            
        }

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
                str = str.Substring(0, str.IndexOf("#")) + str.Substring(str.IndexOf("."), (str.Length - str.IndexOf(".")));
            }
            return str;
        }

        public static string getExportedDataYears(string input)
        {
            string[] arr = input.Split(',');
            string years = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Trim() != String.Empty)
                {
                    if (i > 0)
                    {
                        years += ",";
                    }
                    years += "'" + arr[i].Trim() + "'";
                    
                }
            }
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

        public static void UpdateSeqToDB(OracleConnection oracleConnection, Configs configs)
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

            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_SOVB_TEMPLATESINHSO, ++Import_SoVanBan.SEQ_DCM_SOVB_TEMPLATESINHSO - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_QUYTAC_NHAYSO, ++Import_SoVanBan.SEQ_DCM_QUYTAC_NHAYSO - Constants.INCREASEID_OTHERS);
        }

        public static void updateSeqFromProcedure(OracleConnection oracleConnection, Configs configs)
        {
            OracleCommand cmd = oracleConnection.CreateCommand();
            cmd.CommandText = "CLOUD_ADMIN_DEV_BLU_2.SF_UPDATE_SEQ";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("ps_schema", OracleDbType.Varchar2).Value = configs.Schema;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
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
        
        public static void InitialSeqFromDB(OracleConnection oracleConnection, Configs configs)
        {
            Import_VanBan.SEQ_DCM_DOC_RELATION = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DOC_RELATION);
            Import_VanBan.SEQ_FEM_FILE = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_FEM_FILE);
            Import_VanBan.SEQ_DCM_ATTACH_FILE = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ATTACH_FILE);
            Import_VanBan.SEQ_DCM_TRACK = GetCurrentSeq(oracleConnection, "CLOUD_ADMIN_DEV_BLU_2", Constants.SEQ_DCM_TRACK);
            
            Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ACTIVITI_LOG);
            Import_VanBan_Flow.SEQ_DCM_ASSIGN = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_ASSIGN);
            Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_DONVI_NHAN);
            
            Import_VanBan_Log.SEQ_DCM_LOG = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_LOG);
            //Import_VanBan_Log.SEQ_DCM_LOG_READ = getCurrentSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_LOG_READ);

            Import_SoVanBan.SEQ_DCM_SOVB_TEMPLATESINHSO = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_SOVB_TEMPLATESINHSO);
            Import_SoVanBan.SEQ_DCM_QUYTAC_NHAYSO = GetCurrentSeq(oracleConnection, configs.Schema, Constants.SEQ_DCM_QUYTAC_NHAYSO);
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

        private static void Delete(OracleConnection oracleConnection, string schema, string query)
        {
            
            OracleCommand cmd = new OracleCommand(string.Format(query,schema), oracleConnection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public static void DeleteAllTableRelatedToDcmDoc(OracleConnection oracleConnection, string schema)
        {
            foreach (string table in table_arr)
            {
                Delete(oracleConnection, schema, string.Format(Constants.sql_delete_table, schema, table));
            }
        }

        public static void DeleteDCM_DOC(OracleConnection connection, string schema)
        {
            OracleCommand cmd = null;
            try
            {
                foreach (string table in table_arr)
                {
                    string query = string.Format(Constants.SQL_GET_CONVERTED_DATA_COUNT, schema+".", table);
                    cmd = new OracleCommand(query, connection);
                    int totalRecords = int.Parse(((decimal)cmd.ExecuteScalar()).ToString());
                    int threshold = 10000;
                    int count_loop = totalRecords / threshold + (totalRecords % threshold > 0 ? 1 : 0);

                    for (int i = 0; i < count_loop; i++)
                    {
                        cmd = new OracleCommand(string.Format(Constants.sql_delete_table, schema, table, threshold), connection);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Deleted " + threshold + " records in "+ table);
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
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
    }
}
