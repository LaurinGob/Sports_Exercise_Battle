using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseGetHistory : BCDatabaseQuery
    {
        public List<UserHistory> QueryReturn { get; private set; }
        public DatabaseGetHistory(string username) : base()
        {
            string queryString = "SELECT * FROM get_history WHERE username = @username";
            QueryReturn = new List<UserHistory>();
            
            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                cmd.Parameters.AddWithValue("username", username);
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserHistory row = new UserHistory();
                            row.Name = reader.GetString(reader.GetOrdinal("username"));
                            row.ExerciseType = reader.GetString(reader.GetOrdinal("exerciseType"));
                            row.Duration = reader.GetTimeSpan(reader.GetOrdinal("duration"));
                            row.Count = reader.GetInt32(reader.GetOrdinal("count"));

                            QueryReturn.Add(row);
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseGetHistory: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
