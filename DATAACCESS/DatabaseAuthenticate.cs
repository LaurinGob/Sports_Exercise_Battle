using Npgsql;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseAuthenticate : BCDatabaseQuery
    {
        public bool authenticated { get; private set; } = false;
        public DatabaseAuthenticate(string username, string tokenString) : base()
        {
            // isolate the token string
            string token = IsolateToken(tokenString);

            // get token from DB
            string queryString = "SELECT userToken FROM users WHERE username = @username";

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
                            string view_userToken = reader.GetString(reader.GetOrdinal("userToken"));
                            if (token == view_userToken)
                            {
                                authenticated = true;
                                Console.WriteLine("Authenticated!");
                            } else
                            {
                                authenticated = false;
                                Console.WriteLine("not Authenticated!");
                            }
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseAuthenticate: " + ex.Message);
                    throw;
                }
            }

        }

        private string IsolateToken(string tokenString)
        {
            return tokenString.Replace("Basic ", "").Replace("-sebToken", "").Trim();
        }
    }
}
