using Npgsql.Replication.PgOutput.Messages;
using Sports_Exercise_Battle.DATAACCESS;
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
        public string FirstPlace { get; private set; }
        public List<Participant> Participants { get; private set; } = new List<Participant>();
        public List<TournamentEntry> entries { get; private set; } = new List<TournamentEntry>();
        
        public Tournament() 
        {
            // starts timer that deactivates tournament after 2 minutes
            Timer activeTimer = new Timer(SetInactive, null, 1000 * 119, Timeout.Infinite);
            Console.WriteLine("New Tournament started!");
        }

        public void AddEntry(TournamentEntry entry)
        {
            // using session manager to get the current elo
            BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
            entries.Add(entry);

            // add participant to list of participant if not yet added and enters entry into tournament
            // since the instance of the participant object is not known this is the only way to do this

            // if the participant is found, add the entry.count directly to sum
            bool firstEntry = true;
            foreach (Participant participant in Participants)
            {
                if (participant.ProfileName == entry.ProfileName) 
                { 
                    firstEntry = false; 
                    participant.Sum += entry.Count; 
                }
            }
            if (firstEntry) 
            { 
                Participants.Add(new Participant(entry.ProfileName, entry.Count, SessionManager.GetELO(entry.ProfileName)));
            }
            
            SortByWinner();
        }

        // set inactive via timer callback
        public void SetInactive(object state) { Active = false; ConcludeTournament(); }

        private void ConcludeTournament()
        {
            // decide if there are multiple first places
            int firstPlaceSum = Participants.First().Sum;
            int numberOfWinners = 0;

            foreach (Participant participant in Participants)
            {
                if (participant.Sum == firstPlaceSum)
                {
                    numberOfWinners++;
                } else
                {
                    break;
                }
            }

            // iterate over participants update winner with +2 all others with minus 1
            if (numberOfWinners == 1)
            {
                foreach (Participant participant in Participants)
                {
                    if (participant.ProfileName == FirstPlace)
                    {
                        participant.Elo += 2;
                    }
                    else
                    {
                        participant.Elo -= 1;
                    }
                }
            } else
            {
                for (int i = 0; i < Participants.Count; i++)
                {
                    if (numberOfWinners > 0)
                    {
                        Participants[i].Elo += 1;
                        numberOfWinners--;
                    } else
                    {
                        Participants[i].Elo -= 1;
                    }
                }
            }
            

            // using session manager to get the current elo
            BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
            string username;

            // insert elo in db
            foreach (Participant participant in Participants)
            {
                username = SessionManager.GetUsername(participant.ProfileName);
                DatabaseUpdateElo dbwriter = new DatabaseUpdateElo(participant.Elo, username);
            }
        }

        private void SortByWinner()
        {
            // Sorts list based on highest count and adds first in line to firstplace 
            Participants = Participants.OrderByDescending(obj => obj.Sum).ToList();
            FirstPlace = Participants.First().ProfileName;
        }
    }
}
