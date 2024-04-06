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
    public class DatabaseLogin : BCDatabaseQuery, IDatabaseQuery
    {
        public DatabaseLogin(User userCredentials) {
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

                            //Console.WriteLine($"User ID: {view_user_id}\tUsername: {view_username}\tPassword: {view_passwordHash}");

                            // verify the received hash against provided password
                            if (BCrypt.Net.BCrypt.Verify(userCredentials.Password, view_passwordHash)) {
                                Console.WriteLine("Login Successful!");
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

        public void ExecuteQuery()
        {
            Console.WriteLine("DatabaseGetUser was called");
        }
    }
}
