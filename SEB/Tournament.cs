using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class Tournament
    {
        public bool active { get; set; } = true;
        List<TournamentEntry> entries;

        public Tournament() 
        {
            // starts timer that deactivates tournament after 2 minutes
            Timer activeTimer = new Timer(SetInactive, null, 1000 * 60 * 2, Timeout.Infinite);
        }

        public void AddEntry(TournamentEntry entry)
        {
            entries.Add(entry);
        }

        // set inactive via timer callback
        private void SetInactive(object state) { active = false; }
    }
}
