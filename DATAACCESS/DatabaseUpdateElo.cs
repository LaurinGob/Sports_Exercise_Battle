using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseUpdateElo : BCDatabaseQuery
    {
        public DatabaseUpdateElo(int userelo, string username) : base()
        {
            string queryString = "UPDATE users SET userELO = @userELO WHERE username = @username";

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                // Add parameters to the query
                // SET
                cmd.Parameters.AddWithValue("userELO", userelo);
                // WHERE
                cmd.Parameters.AddWithValue("username", username);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) updated successfully.");

                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseUpdateElo: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
