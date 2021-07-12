using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Controller.Verify
{
    class CheckingConvertedData
    {
        public long GetDataCountFromTable(OracleConnection connection, string query)
        {
            decimal result = -1;
            try
            {
                OracleCommand cmd = new OracleCommand(query, connection);
                result = (decimal)cmd.ExecuteScalar();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return long.Parse(result.ToString());
        }

        public List<long> GetDataFromTable(OracleConnection connection, string query)
        {
            List<long> dataList = new List<long>();
            OracleCommand cmd = new OracleCommand(query, connection);
            OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataSet);
            dataTable = dataSet.Tables[0];

            foreach(DataRow row in dataTable.Rows)
            {
                dataList.Add(long.Parse(row["ID"].ToString()));
            }

            return dataList;
        }

        public bool ImportDataToTable(OracleConnection connection, string query, List<long> data)
        {
            try
            {
                OracleCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.ArrayBindCount = data.Count;

                cmd.CommandText = string.Format(query);

                cmd.Parameters.Add("ID", OracleDbType.Int64);
                cmd.Parameters["ID"].Value = data.ToArray();

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
