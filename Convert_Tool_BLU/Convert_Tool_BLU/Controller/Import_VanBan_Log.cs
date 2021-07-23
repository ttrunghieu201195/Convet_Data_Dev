using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Convert_Data.Models;

namespace Convert_Data
{
    class Import_VanBan_Log : Import_Abstract
    {
		private List<Dcm_Log> dcm_Logs = new List<Dcm_Log>();

		private List<Dcm_Log> dcm_Log_Reads = new List<Dcm_Log>();

		public static long SEQ_DCM_LOG;

		public List<Dcm_Log> getDcm_Logs()
		{
			return dcm_Logs;
		}

		public List<Dcm_Log> getDcm_Log_Reads()
		{
			return dcm_Log_Reads;
		}

		protected override void ParseData(DataRow row, Common.VB_TYPE vb_type)
		{
			try
			{
				Dcm_Log dcm_Log = new Dcm_Log();

				string cell_value = row["id_vanban"].ToString();

				dcm_Log.id = ++SEQ_DCM_LOG;

				if (cell_value != null && cell_value != string.Empty)
				{
					dcm_Log.dcm_id = vb_type == Common.VB_TYPE.VB_DI ? long.Parse(cell_value) + Constants.INCREASEID_VBDI : long.Parse(cell_value);
				}

				cell_value = row["nguoi_xu_ly"].ToString();
				if (cell_value != null && cell_value != string.Empty)
				{
					dcm_Log.username = cell_value;
				}

				if (dcm_Log.username == string.Empty)
				{
					return;
				}

				cell_value = row["trang_thai"].ToString();
				if (cell_value != null && cell_value != string.Empty)
				{
					int tmp = int.Parse(cell_value);
					dcm_Log.is_read = tmp != 0 ? 1 : 0;
				}

				cell_value = row["thoi_gian"].ToString();
				if (cell_value != null && cell_value != string.Empty)
				{
					DateTime thoigian_xem = DateTime.ParseExact(cell_value.Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					dcm_Log.date_log = thoigian_xem;
				}

				dcm_Logs.Add(dcm_Log);
				if (dcm_Log.is_read == 1)
				{
					dcm_Log_Reads.Add(dcm_Log);
				}
			} catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
            }
		}

		public void insert_Dcm_Log(OracleConnection oracleConnection, Configs configs, string query, List<Dcm_Log> data_list)
		{
			try
			{
				if (data_list.Count > 0)
				{
					Stopwatch timer = new Stopwatch();
					Console.WriteLine("Total data to dcm_log: " + data_list.Count);

					List<List<Dcm_Log>> splited_data = Common.SplitList(data_list);
					Console.WriteLine("Total splited data to dcm_log: " + splited_data.Count);

					foreach (List<Dcm_Log> data in splited_data)
					{
						timer.Start();
						OracleCommand cmd = oracleConnection.CreateCommand();
						cmd.CommandType = CommandType.Text;

						cmd.ArrayBindCount = data.Count;

						cmd.CommandText = string.Format(query, configs.Schema);

						cmd.Parameters.Add("ID", OracleDbType.Int64);
						cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2);
						cmd.Parameters.Add("DATE_LOG", OracleDbType.Date);
						cmd.Parameters.Add("DCM_ID", OracleDbType.Int64);
						cmd.Parameters.Add("IS_READ", OracleDbType.Int64);

						cmd.Parameters["ID"].Value = data.Select(dcm_log => dcm_log.id).ToArray();
						cmd.Parameters["USERNAME"].Value = data.Select(dcm_log => dcm_log.username).ToArray();
						cmd.Parameters["DATE_LOG"].Value = data.Select(dcm_log => dcm_log.date_log).ToArray();
						cmd.Parameters["DCM_ID"].Value = data.Select(dcm_log => dcm_log.dcm_id).ToArray();
						cmd.Parameters["IS_READ"].Value = data.Select(dcm_log => dcm_log.is_read).ToArray();

						cmd.ExecuteNonQuery();
						cmd.Dispose();
						timer.Stop();
						Console.WriteLine("Imported data to dcm_log: " + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
						timer.Reset();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

        protected override void resetListData()
        {
			dcm_Logs.Clear();
			dcm_Log_Reads.Clear();
		}

        protected override void ParseData(DataRow row)
        {
			Dcm_Log dcm_Log = new Dcm_Log();
            dcm_Log.id = long.Parse(row["ID"].ToString());
            dcm_Log.username = row["USERNAME"].ToString();
			if (!string.IsNullOrEmpty(row["DATE_LOG"].ToString()))
			{
				dcm_Log.date_log = (DateTime)row["DATE_LOG"];
			}
			if (!string.IsNullOrEmpty(row["DCM_ID"].ToString()))
			{
				dcm_Log.dcm_id = long.Parse(row["DCM_ID"].ToString());
			}
			if (!string.IsNullOrEmpty(row["IS_READ"].ToString()))
			{
				dcm_Log.is_read = int.Parse(row["IS_READ"].ToString());
			}

			dcm_Logs.Add(dcm_Log);
        }

        protected override void ParseData<T>(T data, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }

        protected override void Insert(OracleConnection oracleConnection, string toSchema)
        {
			try
			{
				string query = string.Format(Constants.sql_insert_dcm_log, toSchema);
				if (dcm_Logs.Count > 0)
				{
					Stopwatch timer = new Stopwatch();
					Console.WriteLine("Total data to dcm_log: " + dcm_Logs.Count);

					List<List<Dcm_Log>> splited_data = Common.SplitList(dcm_Logs);
					Console.WriteLine("Total splited data to dcm_log: " + splited_data.Count);

					foreach (List<Dcm_Log> data in splited_data)
					{
						timer.Start();
						OracleCommand cmd = oracleConnection.CreateCommand();
						cmd.CommandType = CommandType.Text;

						cmd.ArrayBindCount = data.Count;

						cmd.CommandText = query;

						cmd.Parameters.Add("ID", OracleDbType.Int64);
						cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2);
						cmd.Parameters.Add("DATE_LOG", OracleDbType.Date);
						cmd.Parameters.Add("DCM_ID", OracleDbType.Int64);
						cmd.Parameters.Add("IS_READ", OracleDbType.Int64);

						cmd.Parameters["ID"].Value = data.Select(dcm_log => dcm_log.id).ToArray();
						cmd.Parameters["USERNAME"].Value = data.Select(dcm_log => dcm_log.username).ToArray();
						cmd.Parameters["DATE_LOG"].Value = data.Select(dcm_log => dcm_log.date_log).ToArray();
						cmd.Parameters["DCM_ID"].Value = data.Select(dcm_log => dcm_log.dcm_id).ToArray();
						cmd.Parameters["IS_READ"].Value = data.Select(dcm_log => dcm_log.is_read).ToArray();

						cmd.ExecuteNonQuery();
						cmd.Dispose();
						timer.Stop();
						Console.WriteLine("Imported data to dcm_log: " + data.Count + "/" + timer.ElapsedMilliseconds / 1000 + "(s)");
						timer.Reset();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

        protected override string getDataQuery(string fromSchema)
        {
			return string.Format(Constants.SQL_SELECT_ALL_DATA, fromSchema, Common.TABLE.DCM_LOG);
		}
    }
}
