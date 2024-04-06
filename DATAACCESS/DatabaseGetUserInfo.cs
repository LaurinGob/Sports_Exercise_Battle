using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseGetUserInfo : BCDatabaseQuery
    {
        public UserData QueryReturn { get; private set; } = new UserData();
        public DatabaseGetUserInfo(string username) : base() 
        {
            string queryString = "SELECT bio, image, profileName FROM users WHERE username = @username";

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
                            QueryReturn.Bio = reader.GetString(reader.GetOrdinal("bio"));
                            QueryReturn.Image = reader.GetString(reader.GetOrdinal("image"));
                            QueryReturn.Name = reader.GetString(reader.GetOrdinal("profileName"));
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
