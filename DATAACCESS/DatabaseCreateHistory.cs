using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseCreateHistory : BCDatabaseQuery
    {
        public DatabaseCreateHistory(UserHistory historyEntry, int UserID) : base()
        {
            string queryString = "INSERT INTO history (fk_user_id, exercisetype, count, duration) VALUES (@userid, @exercisetype, @count, @duration);";

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                // Add parameters to the query
                cmd.Parameters.AddWithValue("userid", UserID);
                cmd.Parameters.AddWithValue("exercisetype", historyEntry.Name);
                cmd.Parameters.AddWithValue("count", historyEntry.Count);
                cmd.Parameters.AddWithValue("duration", historyEntry.DurationInSeconds);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseCreateHistory: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
