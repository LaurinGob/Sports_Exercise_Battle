using Npgsql;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class BCDatabaseQuery
    {
        private DatabaseConnection connection = new DatabaseConnection();
        public NpgsqlConnection conn { get; set; }


        public BCDatabaseQuery()
        {
            this.conn = new NpgsqlConnection(connection.connString);
            {
                try
                {
                    // Open the connection
                    this.conn.Open();
                    // Console.WriteLine("Connected to PostgreSQL!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database connection error: " + ex.Message);
                }
            }
        }
    }
}
