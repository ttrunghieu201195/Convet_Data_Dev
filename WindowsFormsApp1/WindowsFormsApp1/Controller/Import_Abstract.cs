using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Controller
{
    abstract class Import_Abstract
    {
        public void exportdataFromPostgres(NpgsqlConnection postgresConnection, string query)
        {
            try
            {
                var cmd = new NpgsqlCommand(query, postgresConnection);

                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected abstract void resetListData();
        protected abstract void ParseData(DataRow row);
    }
}
