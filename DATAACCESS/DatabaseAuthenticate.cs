using Npgsql;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Linq.Expressions;
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
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string view_userToken = reader.GetString(reader.GetOrdinal("userToken"));
                                // authenticate against database token
                                if (token == view_userToken)
                                {
                                    authenticated = true;
                                    Console.WriteLine("Authenticated!");
                                    break;
                                }
                                else
                                {
                                    authenticated = false;
                                    Console.WriteLine("Not Authenticated!");
                                }
                            }
                        } 
                        else 
                        {
                            authenticated = false;
                            throw new Exception("User not found");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseAuthenticate: " + ex.Message);
                    throw;
                }
                finally
                {
                    conn.Close(); // Ensure the connection is always closed
                }
            }
        }

        private string IsolateToken(string tokenString)
        {
            return tokenString.Replace("Basic ", "").Trim();
        }
    }
}
