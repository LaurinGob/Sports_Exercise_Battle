using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class Tournament
    {
        public bool Active { get; private set; } = true;
        public DateTime TournamentStarted { get; private set; } = DateTime.Now;
        public List<TournamentEntry> entries { get; private set; } = new List<TournamentEntry>();
        public string Winner { get; private set; }

        public Tournament() 
        {
            // starts timer that deactivates tournament after 2 minutes
            Timer activeTimer = new Timer(SetInactive, null, 1000 * 120, Timeout.Infinite);
            Console.WriteLine("New Tournament started!");
        }

        public void AddEntry(TournamentEntry entry)
        {
            entries.Add(entry);
            SortByWinner(); // TODO: not working properly
        }

        // set inactive via timer callback
        public void SetInactive(object state) { Active = false; } // TODO: change elo values

        private void SortByWinner()
        {
            entries = entries.OrderBy(obj => obj.Count).ToList();
            Winner = entries.First().ProfileName;
        }
    }
}
