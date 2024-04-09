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
        public string FirstPlace { get; private set; } // the first in line
        public List<TournamentEntry> entries { get; private set; } = new List<TournamentEntry>();
        
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
            // TODO: not working properly
            // Sorts list based on highest count and adds first in line to firstplace 
            entries = entries.OrderBy(obj => obj.Count).ToList();
            FirstPlace = entries.First().ProfileName;
        }
    }
}
