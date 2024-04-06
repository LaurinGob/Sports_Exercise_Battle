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
            string queryString = "SELECT bio, image FROM users WHERE username = @username";

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
                            string view_bio = "";
                            string view_image = "";

                            if (!reader.IsDBNull(reader.GetOrdinal("bio")))
                            {
                                view_bio = reader.GetString(reader.GetOrdinal("bio"));
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("image")))
                            {
                                view_image = reader.GetString(reader.GetOrdinal("image"));
                            }
                            
                            QueryReturn.Name = username;
                            QueryReturn.Bio = view_bio;
                            QueryReturn.Image = view_image;
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
