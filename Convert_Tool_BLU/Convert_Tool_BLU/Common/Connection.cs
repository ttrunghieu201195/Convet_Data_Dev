using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data
{
    class Connection
    {
        private NpgsqlConnection postgresConnection;
        private OracleConnection oracleConnection;
        private OracleConnection bluConnection;
        private OracleConnection devBlu2Connection;
        private static Connection connection;
        private Connection()
        {
            postgresConnection = new NpgsqlConnection(Constants.postgres_connstring);
            oracleConnection = new OracleConnection(Constants.oracle_connstring);
            bluConnection = new OracleConnection(Constants.BLU_CONNSTRING);
            devBlu2Connection = new OracleConnection(Constants.DEV_BLU_2_CONNSTR);
        }

        public static Connection getInstance()
        {
            if (connection == null)
            {
                connection = new Connection();
            }
            return connection;
        }

        public OracleConnection GetOracleConnection()
        {
            if (oracleConnection.State != ConnectionState.Open)
            {
                //postgresConnection = new NpgsqlConnection(Constants.postgres_connstring);
                oracleConnection.Open();
            }
            return oracleConnection;
        }

        public OracleConnection GetBLUConnection()
        {
            if (bluConnection.State != ConnectionState.Open)
            {
                bluConnection.Open();
            }
            return bluConnection;
        }

        public OracleConnection GetDevBlu2Connection()
        {
            if (devBlu2Connection.State != ConnectionState.Open)
            {
                devBlu2Connection.Open();
            }
            return devBlu2Connection;
        }

        public NpgsqlConnection GetPostgresConnection()
        {
            if (postgresConnection.State != ConnectionState.Open)
            {
                //oracleConnection = new OracleConnection(Constants.oracle_connstring);
                postgresConnection.Open();
            }
            return postgresConnection;
        }

        public void CloseConnection()
        {
            postgresConnection.Close();
            oracleConnection.Close();
        }
    }
}
