using Npgsql;
using Sports_Exercise_Battle.SEB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.DATAACCESS
{
    public class DatabaseCreateTournament : BCDatabaseQuery
    {
        public DatabaseCreateTournament(PastTournament tournamentRecord) : base()
        {
            string queryString = "INSERT INTO tournaments (tournament_started, winner, participant_count) VALUES (@tournament_started, @winner, @participant_count);";

            using (var cmd = new NpgsqlCommand(queryString, this.conn))
            {
                // Add parameters to the query
                cmd.Parameters.AddWithValue("tournament_started", tournamentRecord.TournamentStarted);
                cmd.Parameters.AddWithValue("winner", tournamentRecord.Winner);
                cmd.Parameters.AddWithValue("participant_count", tournamentRecord.ParticipantCount);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted successfully.");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in DatabaseCreateTournament: " + ex.Message);
                    throw;
                }
            }
        }
    }
}
