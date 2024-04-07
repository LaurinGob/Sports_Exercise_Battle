using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseGetUserStats : BCDatabaseQuery
    {
        public UserStats QueryReturn { get; private set; } = new UserStats();
        public DatabaseGetUserStats(string username) : base()
        {
            string queryString = "SELECT * FROM get_stats WHERE username = @username";

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                // Add parameters to the query
                cmd.Parameters.AddWithValue("username", username);

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QueryReturn.Elo = reader.GetInt32(reader.GetOrdinal("userelo"));
                            QueryReturn.TotalCount = reader.GetInt32(reader.GetOrdinal("totalcount"));
                            QueryReturn.Name = reader.GetString(reader.GetOrdinal("username"));
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseGetUserInfo: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
