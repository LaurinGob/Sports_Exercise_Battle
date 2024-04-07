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
    public class DatabaseCreateUser : BCDatabaseQuery
    {
        public DatabaseCreateUser(User userCredentials) : base() 
        {

            // hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userCredentials.Password);
            string queryString = "INSERT INTO users (username, passwordHash, userToken) VALUES (@username, @passwordHash, @usertoken)";

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                // Add parameters to the query
                cmd.Parameters.AddWithValue("username", userCredentials.Username);
                cmd.Parameters.AddWithValue("passwordHash", hashedPassword);
                cmd.Parameters.AddWithValue("usertoken", userCredentials.Username);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseCreateUser: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
