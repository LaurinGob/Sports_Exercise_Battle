﻿using System;
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
            Timer activeTimer = new Timer(SetInactive, null, 1000 * 120, Timeout.Infinite);
            Console.WriteLine("New Tournament started!");
        }

        public void AddEntry(TournamentEntry entry)
        {
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
                Participants.Add(new Participant(entry.ProfileName, entry.Count));
            }
            
            SortByWinner();
        }

        // set inactive via timer callback
        public void SetInactive(object state) { Active = false; ConcludeTournament(); }

        private void ConcludeTournament()
        {
            // remove winner from participants
            // Participants.Remove(FirstPlace); // not good weil history verfälscht
            // reduce ELO of all other -1
            // code
            // increase ELO of winner +2
            // code
        }

        private void SortByWinner()
        {
            // Sorts list based on highest count and adds first in line to firstplace 
            Participants = Participants.OrderByDescending(obj => obj.Sum).ToList();
            FirstPlace = Participants.First().ProfileName;
        }
    }
}
