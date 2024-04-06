namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseConnection
    {
        // provides a connection to the database

        // db credentials and login data
        private string host = "localhost";
        private string port = "5432";
        private string username = "seb_connection";
        private string password = "seb_data_1234";
        private string database = "postgres";

        // concats to string
        public string connString { get; private set; }
        public DatabaseConnection()
        {
            connString = String.Concat(
                "Host =", this.host,
                "; Port =", this.port,
                "; Username =", this.username,
                "; Password =", this.password,
                "; Database =", this.database
            );
        }
    }
}
