using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class TournamentEntry
    {
        public string ProfileName { get; set; } = "";
        public string Name { get; set; } = "";
        public int Count { get; set; } = 0;
        public int DurationInSeconds { get; set; } = 0;

        public TournamentEntry(UserHistory entry, string profileName) 
        {
            ProfileName = profileName;
            Name = entry.Name;
            Count = entry.Count;
            DurationInSeconds = entry.DurationInSeconds;
        }
    }
}
