using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Common
    {
        public enum VB_TYPE
        {
            NONE = 0,
            VB_DEN = 1,
            VB_DI = 2
        }

        public static Int64 getCurrentSeq(OracleConnection connection, string schema, string seqName)
        {
            string sql = "select " + schema + "." + seqName + ".NextVal from dual";
            OracleCommand objCommand1 = new OracleCommand(sql, connection);
            decimal result = (decimal)objCommand1.ExecuteScalar();
            return Int64.Parse(result.ToString());
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

        public static string escape_Trichyeu(string trich_yeu)
        {
            trich_yeu = trich_yeu.Replace("&", "&amp;");
            trich_yeu = trich_yeu.Replace("'", "&apos;");
            trich_yeu = trich_yeu.Replace("\"", "&quot; ");
            trich_yeu = trich_yeu.Replace("<", "&lt;");
            trich_yeu = trich_yeu.Replace(">", "&gt;");
            trich_yeu = trich_yeu.Replace("¢", "&cent;");
            trich_yeu = trich_yeu.Replace("£", "&pound;");
            trich_yeu = trich_yeu.Replace("¥", "&yen;");
            trich_yeu = trich_yeu.Replace("€", "&euro;");
            trich_yeu = trich_yeu.Replace("©", "&copy;");
            trich_yeu = trich_yeu.Replace("®", "&reg;");
            trich_yeu = trich_yeu.Replace("=", "&equals;");
            trich_yeu = trich_yeu.Replace(",", "&comma;");
            trich_yeu = trich_yeu.Replace("\'", "\\'");
            trich_yeu = trich_yeu.Replace("\r\n", "<br>");
            trich_yeu = trich_yeu.Replace("\n", "<br>");
            return trich_yeu;
        }

        public static string getFileNameFromHddFile(string str)
        {
            string result = str;
            if (Regex.IsMatch(str, @"#\d+"))
            {
                result = str.Substring(0, str.IndexOf("#")) + str.Substring(str.IndexOf("."), (str.Length - str.IndexOf(".")));
            }
            return result;
        }

        public static string getExportedDataYears(string input)
        {
            string[] arr = input.Split(',');
            string years = "";
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Trim() != String.Empty)
                {
                    years += "'" + arr[i].Trim() + "'";
                    if (i > 0 && i < arr.Length - 1)
                    {
                        years += ",";
                    }
                }
            }
            return years;
        }

        public static string getValue_VaiTro(string vaitro)
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

        public static void UpdateSeqToDB(OracleConnection oracleConnection, Configs configs)
        {
            UpdateSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_DOC_RELATION, ++Import_VanBan.SEQ_DCM_DOC_RELATION - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.schema, Constants.SEQ_FEM_FILE, ++Import_VanBan.SEQ_FEM_FILE - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_ATTACH_FILE, ++Import_VanBan.SEQ_DCM_ATTACH_FILE - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, "CLOUD_ADMIN_DEV_BLU_2", Constants.SEQ_DCM_TRACK, ++Import_VanBan.SEQ_DCM_TRACK - Constants.INCREASEID_OTHERS);

            UpdateSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_ACTIVITI_LOG, ++Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_ASSIGN, ++Import_VanBan_Flow.SEQ_DCM_ASSIGN - Constants.INCREASEID_OTHERS);
            UpdateSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_DONVI_NHAN, ++Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN - Constants.INCREASEID_OTHERS);
        }
        
        public static void InitialSeqFromDB(OracleConnection oracleConnection, Configs configs)
        {
            Import_VanBan.SEQ_DCM_DOC_RELATION = getCurrentSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_DOC_RELATION) + Constants.INCREASEID_OTHERS;
            Import_VanBan.SEQ_FEM_FILE = getCurrentSeq(oracleConnection, configs.schema, Constants.SEQ_FEM_FILE) + Constants.INCREASEID_OTHERS;
            Import_VanBan.SEQ_DCM_ATTACH_FILE = getCurrentSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_ATTACH_FILE) + Constants.INCREASEID_OTHERS;
            Import_VanBan.SEQ_DCM_TRACK = getCurrentSeq(oracleConnection, "CLOUD_ADMIN_DEV_BLU_2", Constants.SEQ_DCM_TRACK) + Constants.INCREASEID_OTHERS;

            Import_VanBan_Flow.SEQ_DCM_ACTIVITI_LOG = getCurrentSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_ACTIVITI_LOG) + Constants.INCREASEID_OTHERS;
            Import_VanBan_Flow.SEQ_DCM_ASSIGN = getCurrentSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_ASSIGN) + Constants.INCREASEID_OTHERS;
            Import_VanBan_Flow.SEQ_DCM_DONVI_NHAN = getCurrentSeq(oracleConnection, configs.schema, Constants.SEQ_DCM_DONVI_NHAN) + Constants.INCREASEID_OTHERS;
        }
    }
}
