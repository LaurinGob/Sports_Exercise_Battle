using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseGetScore : BCDatabaseQuery
    {
        public List<UserStats> QueryReturn { get; private set; }
        public DatabaseGetScore() : base()
        {
            string queryString = "SELECT * FROM get_score";
            QueryReturn = new List<UserStats>();
            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserStats row = new UserStats();
                            row.Name = reader.GetString(reader.GetOrdinal("profilename"));
                            row.Elo = reader.GetInt32(reader.GetOrdinal("userelo"));
                            row.TotalCount = reader.GetInt32(reader.GetOrdinal("totalcount"));

                            QueryReturn.Add(row);
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
