using Convert_Data.Models;
using IronXL;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convert_Data
{
    public partial class ImportData : Form
    {

        private static string oracle_host = "123.31.40.153";
        private static string oracle_port = "1521";
        private static string oracle_service_name = "eofdichvu";
        private static string oracle_user = "CLOUD_ADMIN_DEV_BLU_2";
        private static string oracle_pass = "d3vblu@2020";
        public static string oracle_connstring = String.Format("Data Source=( DESCRIPTION = " +
            "( ADDRESS_LIST = ( ADDRESS = ( PROTOCOL = TCP )( HOST = {0} )(PORT = {1} ) ) )" +
            "(CONNECT_DATA = (SERVER = DEDICATED )(SERVICE_NAME = {2}) ) ); " +
            "User Id = {3}; Password = {4};", oracle_host, oracle_port, oracle_service_name, oracle_user, oracle_pass);

        private static readonly string GET_DATA_POSTGRES = "select \"row_number\"() OVER (ORDER BY D.doc_id), d.doc_id, o.folder, d.folder, d.yeardocument, d.filename, d.filenamecode, d.filenameoriginal from ( "
+ " SELECT i.ID doc_id, 'in' folder, i.organizationid, i.yeardocument, ia.filename, ia.filenamecode, ia.filenameoriginal FROM \"public\".\"documentincoming\" i, \"public\".documentincomingattach ia  "
+ " where i.organizationid = 3528 and i.id = ia.documentincomingid and i.yeardocument ~ E'^\\\\d+$' and cast(i.yeardocument as int) >= 2021 and cast(i.yeardocument as int) <= 2021 "
+ " UNION "
+ " SELECT (o.ID+2000000) doc_id, 'out' folder, o.organizationid, o.yeardocument, oa.filename, oa.filenamecode, oa.filenameoriginal FROM \"public\".\"documentoutgoing\" o, \"public\".documentoutgoingattach oa  "
+ " where o.organizationid = 3528 and o.id = oa.documentoutgoingid and o.yeardocument ~ E'^\\\\d+$' and cast(o.yeardocument as int) >= 2021 and cast(o.yeardocument as int) <= 2021 "
+ " ) d, \"public\".organization o where o.id = d.organizationid order by cast(d.yeardocument as int), d.doc_id, d.filename";


        private static readonly string INSERT_TO_DOC_FILE_PATH = "INSERT INTO {0}DOC_FILE_PATH(STT, DOC_ID, FOLDER, FOLDER1, YEARDOCUMENT, FILENAME, FILENAMECODE, FILENAMEORIGINAL) "
            + "VALUES(:STT, :DOC_ID, :FOLDER, :FOLDER1, :YEARDOCUMENT, :FILENAME, :FILENAMECODE, :FILENAMEORIGINAL)";

        private static readonly string GET_DATA_FILE_PATH = "select 'update qlvb_blu_tinhbaclieu.fem_file set hdd_file = ''' || a.file_path || ''' where id = '||a.fileid ||';' query from ( "
+ " select a.document_id, a.fileid, 'nfsqlvbubnd/datafile/'||a.folder||'/'||a.yeardocument||'/'||a.folder1||'/'||a.final_file file_path "
+ " from ( "
+ " select d.*, a.* "
+ " , (case when lower(cast(d.filename as varchar2(2048))) = lower(a.hdd_file) then 1 else 0 end) col1 "
+ " , (case when lower(cast(d.filenamecode as varchar2(2048))) = lower(a.hdd_file) then 1 else 0 end) col2 "
+ " , (case when lower(cast(d.filenameoriginal as varchar2(2048))) = lower(a.hdd_file) then 1 else 0 end) col3 "
+ " from cloud_admin_dev_blu_2.doc_file_path d "
+ " , (select a.doc_id document_id, f.id fileId, f.name, f.hdd_file from qlvb_Blu_tinhbaclieu_1.fem_file f, qlvb_blu_tinhbaclieu_1.dcm_attach_file a "
+ " where f.id = a.file_id) a "
+ " where d.doc_id = a.document_id) a "
+ " where (a.col1+a.col2+a.col3) >= 2 "
+ " order by a.document_id "
+ " )a";

        NpgsqlConnection postgresConnection;
        OracleConnection oracleConnection;
        public ImportData()
        {
            InitializeComponent();
        }

        private void ImportData_Load(object sender, EventArgs e)
        {
            /*postgresConnection = Connection.getInstance().GetPostgresConnection();*/
            oracleConnection = oracleConnection = new OracleConnection(oracle_connstring);
            oracleConnection.Open();
            // Get data from postgres
            /*DataTable dt = GetDataFromPostgres(postgresConnection, GET_DATA_POSTGRES);
            Console.WriteLine(dt.Rows.Count);
            ImportDataToOracle(oracleConnection, dt);*/
            //            GetDataFromQuery(oracleConnection, GET_DATA_FILE_PATH);
            saveDataToExcel(GetDataFromQuery(oracleConnection, GET_DATA_FILE_PATH));
        }

        private static DataTable GetDataFromPostgres(NpgsqlConnection connection, string query)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            DataTable dataTable = dataSet.Tables[0];

            return dataTable;
        }

        private static DataTable GetDataFromQuery(OracleConnection connection, string query)
        {
            OracleCommand cmd = new OracleCommand(query, connection);
            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            DataTable dataTable = dataSet.Tables[0];

            return dataTable;
        }

        private static void saveDataToExcel(DataTable dt)
        {
            int count = dt.Rows.Count;

            using (StreamWriter writetext = new StreamWriter("E:\\03_VNPT\\01_Working\\01_Eoffice_v5\\write.txt"))
            {
                for (int i = 0; i < count; i++)
                {
                    string result = Convert.ToString(dt.Rows[i]["QUERY"]);
                    Console.WriteLine(result);
                    writetext.WriteLine(result);
                }
            }
        }

        private static void UpdateDataToFemFile(OracleConnection connection, DataTable dt)
        {
            int count = dt.Rows.Count;
            int[] FILEIDS = new int[count];
            string[] FILE_PATHS = new string[count];
            

            for(int i = 0; i < count; i++)
            {
                FILEIDS[i] = Convert.ToInt32(dt.Rows[i]["FILEID"]);
                FILE_PATHS[i] = Convert.ToString(dt.Rows[i]["FILE_PATH"]);
            }

            OracleCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandType = CommandType.Text;

                cmd.ArrayBindCount = count;
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        private static void ImportDataToOracle(OracleConnection connection, DataTable dt)
        {
            int count = dt.Rows.Count;
            int[] ROW_NUMS = new int[count];
            int[] DOC_IDS = new int[count];
            string[] FOLDERS = new string[count];
            string[] FOLDER1S = new string[count];
            int[] YEARDOCUMENTS = new int[count];
            string[] FILENAMES = new string[count];
            string[] FILENAMECODES = new string[count];
            string[] FILENAMEORIGINALS = new string[count];
            for (int i = 0; i < count; i++)
            {
                ROW_NUMS[i] = Convert.ToInt32(dt.Rows[i]["ROW_NUMBER"]);
                DOC_IDS[i] = Convert.ToInt32(dt.Rows[i]["DOC_ID"]);
                FOLDERS[i] = Convert.ToString(dt.Rows[i]["FOLDER"]);
                FOLDER1S[i] = Convert.ToString(dt.Rows[i]["FOLDER1"]);
                YEARDOCUMENTS[i] = Convert.ToInt32(dt.Rows[i]["YEARDOCUMENT"]);
                FILENAMES[i] = Convert.ToString(dt.Rows[i]["FILENAME"]);
                FILENAMECODES[i] = Convert.ToString(dt.Rows[i]["FILENAMECODE"]);
                FILENAMEORIGINALS[i] = Convert.ToString(dt.Rows[i]["FILENAMEORIGINAL"]);
            }
            OracleCommand cmd = connection.CreateCommand();
            try
            {
                cmd.CommandType = CommandType.Text;

                cmd.ArrayBindCount = count;
                cmd.CommandText = string.Format(INSERT_TO_DOC_FILE_PATH, "CLOUD_ADMIN_DEV_BLU_2.");

                cmd.Parameters.Add("STT", OracleDbType.Int32);
                cmd.Parameters.Add("DOC_ID", OracleDbType.Int32);
                cmd.Parameters.Add("FOLDER", OracleDbType.Varchar2);
                cmd.Parameters.Add("FOLDER1", OracleDbType.Varchar2);
                cmd.Parameters.Add("YEARDOCUMENT", OracleDbType.Int32);
                cmd.Parameters.Add("FILENAME", OracleDbType.Varchar2);
                cmd.Parameters.Add("FILENAMECODE", OracleDbType.Varchar2);
                cmd.Parameters.Add("FILENAMEORIGINAL", OracleDbType.Varchar2);

                cmd.Parameters["STT"].Value =ROW_NUMS;
                cmd.Parameters["DOC_ID"].Value = DOC_IDS;
                cmd.Parameters["FOLDER"].Value = FOLDERS;
                cmd.Parameters["FOLDER1"].Value = FOLDER1S;
                cmd.Parameters["YEARDOCUMENT"].Value = YEARDOCUMENTS;
                cmd.Parameters["FILENAME"].Value = FILENAMES;
                cmd.Parameters["FILENAMECODE"].Value = FILENAMECODES;
                cmd.Parameters["FILENAMEORIGINAL"].Value = FILENAMEORIGINALS;

                int affectedRows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                Console.WriteLine("Imported data to DOC_FILE_PATH: " + affectedRows);

            } catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
