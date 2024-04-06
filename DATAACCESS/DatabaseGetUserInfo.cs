using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseGetUserInfo : BCDatabaseQuery, IDatabaseQuery
    {
        public UserData data { get; private set; } = new UserData();
        public DatabaseGetUserInfo(string username) : base() 
        {
            string queryString = "SELECT * FROM users WHERE username = @value1";

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                // Add parameters to the query
                cmd.Parameters.AddWithValue("value1", username);

                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int view_user_id = reader.GetInt32(reader.GetOrdinal("user_id"));
                            string view_username = reader.GetString(reader.GetOrdinal("username"));
                            string view_password = reader.GetString(reader.GetOrdinal("password"));

                            // Process the retrieved values
                            Console.WriteLine($"User ID: {view_user_id}\tUsername: {view_username}\tPassword: {view_password}");
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseGetUserInfo: " + ex.Message);

                }
            }
        }

        public void ExecuteQuery()
        {
            Console.WriteLine("DatabaseGetUser was called");
        }
    }
}
