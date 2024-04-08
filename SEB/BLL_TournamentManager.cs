using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class BLL_TournamentManager
    {
        private static readonly Lazy<BLL_TournamentManager> lazyInstance = new Lazy<BLL_TournamentManager>(() => new BLL_TournamentManager());

        // Private constructor to prevent instantiation
        private BLL_TournamentManager() { }

        public static BLL_TournamentManager Instance => lazyInstance.Value;

        List<Tournament> tournaments;

        public void NewTournamentEntry(TournamentEntry entry)
        {
            // adds new entry to tournament
            Tournament currentTournament = GetTournament();
            currentTournament.AddEntry(entry);
        }

        private Tournament GetTournament()
        {
            // returns running tournament or creates new one
            foreach (Tournament joust in tournaments)
            {
                if (joust.active) { return joust; }
            }
            Tournament newTournament = new Tournament();
            tournaments.Add(newTournament);
            Console.WriteLine("New Tournament started!");
            return newTournament;
        }
    }
}
