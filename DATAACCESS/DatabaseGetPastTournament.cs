using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseGetPastTournament : BCDatabaseQuery
    {
        public List<PastTournament> QueryReturn { get; private set; }
        public DatabaseGetPastTournament() : base()
        {
            string queryString = "SELECT * FROM tournaments";
            QueryReturn = new List<PastTournament>();

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PastTournament row = new PastTournament();
                            row.TournamentStarted = reader.GetDateTime(reader.GetOrdinal("tournament_started"));
                            row.Winner = reader.GetString(reader.GetOrdinal("winner"));
                            row.ParticipantCount = reader.GetInt32(reader.GetOrdinal("participant_count"));

                            QueryReturn.Add(row);
                        }
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseGetPastTournament: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
