﻿using Npgsql.Replication.PgOutput.Messages;
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
        public DateTime TournamentStarted { get; private set; } = DateTime.UtcNow;
        public string FirstPlace { get; private set; }
        public List<Participant> Participants { get; private set; } = new List<Participant>();
        public List<TournamentEntry> entries { get; private set; } = new List<TournamentEntry>();
        public List<String> Log { get; private set; } = new List<String>();
        Timer activeTimer;

        public Tournament(int time) 
        {
            // starts timer that deactivates tournament after *time* seconds
            activeTimer = new Timer(SetInactive, null, 1000 * time, Timeout.Infinite);
            Log.Add(DateTime.UtcNow + ": Tournament started!");
        }

        public void AddEntry(TournamentEntry entry, int userID)
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
                    // enter log for new entry
                    Log.Add(DateTime.UtcNow.ToString("g") + ": New entry by " + entry.ProfileName + ": " + entry.Count + " " + entry.Name + " for a total sum of " + participant.Sum);
                }
            }
            if (firstEntry) 
            { 
                Participants.Add(new Participant(entry.ProfileName, entry.Count, SessionManager.GetELO(userID), userID));
                // enter log for new participant
                Log.Add(DateTime.UtcNow.ToString("g") + ": " + entry.ProfileName + " has entered the tournament with: " + entry.Count + " " + entry.Name + "/s");
            }

            string tempFirstplace = FirstPlace;
            SortByWinner();
            // enter log for change in first place
            if (tempFirstplace != FirstPlace) { Log.Add(DateTime.UtcNow.ToString("g") + ": " + entry.ProfileName + " pulls ahead with an entry of: " + entry.Count); }
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
            
            // start filling tournament record
            PastTournament tournamentRecord = new PastTournament();
            tournamentRecord.TournamentStarted = TournamentStarted;
            tournamentRecord.ParticipantCount = Participants.Count;

            // iterate over participants update winner with +2 all others with minus 1
            if (numberOfWinners == 1)
            {
                Log.Add(DateTime.UtcNow.ToString("g") + ": Tournament concluded with " + FirstPlace + " as the Winner!");
                Console.WriteLine("Tournament concluded with " + FirstPlace + " as the Winner!");
                foreach (Participant participant in Participants)
                {
                    tournamentRecord.Winner = FirstPlace;
                    if (participant.ProfileName == FirstPlace)
                    {
                        participant.Elo += 2;
                        Log.Add(DateTime.UtcNow.ToString("g") + ": " + participant.ProfileName + " was awarded with 2 ELO");
                    }
                    else
                    {
                        participant.Elo -= 1;
                        Log.Add(DateTime.UtcNow.ToString("g") + ": " + participant.ProfileName + " was punished with -1 ELO");
                    }
                }
            } else
            {
                Log.Add(DateTime.UtcNow.ToString("g") + ": Tournament concluded with multiple Winners!");
                Console.WriteLine("Tournament concluded with multiple Winners!");
                Console.WriteLine("Winners: ");
                // incase of draws (works also with more than 2 winners!)
                for (int i = 0; i < Participants.Count; i++)
                {
                    if (numberOfWinners > 0)
                    {
                        Participants[i].Elo += 1;
                        Log.Add(DateTime.UtcNow.ToString("g") + ": " + Participants[i].ProfileName + " was awarded with 1 ELO");
                        numberOfWinners--;
                        if (numberOfWinners > 0)
                        {
                            tournamentRecord.Winner += Participants[i].ProfileName + ", ";
                            Console.Write(Participants[i].ProfileName + ", ");
                        }
                        else
                        {
                            tournamentRecord.Winner += Participants[i].ProfileName;
                            Console.Write(Participants[i].ProfileName);
                        }
                        
                    } else
                    {
                        Participants[i].Elo -= 1;
                        Log.Add(DateTime.UtcNow.ToString("g") + ": " + Participants[i].ProfileName + " was punished with -1 ELO");
                    }
                }
                Log.Add(DateTime.UtcNow.ToString("g") + ": Winners: " + tournamentRecord.Winner);
            }

            // insert elo in db
            foreach (Participant participant in Participants)
            {
                DatabaseUpdateElo dbWriterELO = new DatabaseUpdateElo(participant.Elo, participant.UserID);
            }

            // insert tournament record into db
            DatabaseCreateTournament dbWriterTournament = new DatabaseCreateTournament(tournamentRecord);
        }

        private void SortByWinner()
        {
            // Sorts list based on highest count and adds first in line to firstplace 
            Participants = Participants.OrderByDescending(obj => obj.Sum).ToList();
            FirstPlace = Participants.First().ProfileName;
        }
    }
}
