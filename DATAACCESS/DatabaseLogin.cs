using Npgsql;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sports_Exercise_Battle.SEB;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseLogin : BCDatabaseQuery
    {
        public string userToken { get; private set; } // needs to be public so it can be returned to user via HttpResponse
        public DatabaseLogin(User userCredentials) : base() {
            // SQL Query
            string queryString = "SELECT * FROM users WHERE username = @value1";

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                // Add parameters to the query
                cmd.Parameters.AddWithValue("value1", userCredentials.Username);

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int view_user_id = reader.GetInt32(reader.GetOrdinal("user_id"));
                            string view_username = reader.GetString(reader.GetOrdinal("username"));
                            string view_passwordHash = reader.GetString(reader.GetOrdinal("passwordHash"));
                            string view_profileName = reader.GetString(reader.GetOrdinal("profileName"));
                            userToken = reader.GetString(reader.GetOrdinal("userToken"));

                            // verify the received hash against provided password
                            if (BCrypt.Net.BCrypt.Verify(userCredentials.Password, view_passwordHash)) {
                                Console.WriteLine("Login Successful!");
                                // add new session to session manager
                                BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
                                SessionManager.NewSession(view_user_id, view_username, userToken, view_profileName);
                            } else
                            {
                                throw new Exception("Login credentials not recognized");
                            }
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseLogin: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
