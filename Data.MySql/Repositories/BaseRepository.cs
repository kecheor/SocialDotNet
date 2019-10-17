using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Data.MySql.Repositories
{
	public abstract class BaseRepository<T>
	{
		protected string _connectionString;
		public BaseRepository(string connectionString)
		{
			_connectionString = connectionString;
		}
		protected async Task<List<T>> ExecuteQuery(string commandText, Dictionary<string, object>? parameters)
		{
			List<T> result = new List<T>();
			using (MySqlConnection conn = new MySqlConnection(_connectionString))
			{
				using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
				{
					if(parameters != null)
					{
						foreach(var p in parameters)
						{
							cmd.Parameters.AddWithValue(p.Key, p.Value);
						}
					}
					conn.Open();
					DbDataReader rdr = await cmd.ExecuteReaderAsync();
					while (rdr.Read())
					{
						result.Add(await Read(rdr));
					}
					rdr.Close();
				}
			}
			return result;
		}


		protected Task<int> ExecuteNonQuery(string commandText, Dictionary<string, object>? parameters)
		{
			List<T> result = new List<T>();
			using (MySqlConnection conn = new MySqlConnection(_connectionString))
			{
				using (MySqlCommand cmd = new MySqlCommand(commandText, conn))
				{
					if (parameters != null)
					{
						foreach (var p in parameters)
						{
							cmd.Parameters.AddWithValue(p.Key, p.Value);
						}
					}
					conn.Open();
					return cmd.ExecuteNonQueryAsync();
				}
			}
		}

		protected abstract Task<T> Read(DbDataReader rdr);
	}
}