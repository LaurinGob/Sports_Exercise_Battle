using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseChangeUserInfo : BCDatabaseQuery
    {
        public DatabaseChangeUserInfo(UserData userData, string username) : base() 
        {
            string queryString = "UPDATE users SET bio = @bio, image = @image, profileName = @profileName WHERE username = @username";

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                // Add parameters to the query
                // SET
                cmd.Parameters.AddWithValue("bio", userData.Bio);
                cmd.Parameters.AddWithValue("image", userData.Image);
                cmd.Parameters.AddWithValue("profileName", userData.Name);
                // WHERE
                cmd.Parameters.AddWithValue("username", username);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) updated successfully.");

                    // update session with changed value of profile name
                    BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
                    SessionManager.UpdateSession(username, userData.Name);

                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseChangeUserInfo: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
