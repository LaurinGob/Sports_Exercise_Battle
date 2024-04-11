using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class PastTournament
    {
        public DateTime TournamentStarted { get; set; } = DateTime.UtcNow;
        public string Winner { get; set; } = string.Empty;
        public int ParticipantCount { get; set; } = 0;
    }
}
