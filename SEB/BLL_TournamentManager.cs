﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        List<Tournament> tournaments = new List<Tournament>();

        public void NewTournamentEntry(TournamentEntry entry, int userID)
        {
            // adds new entry to tournament
            Tournament currentTournament = GetTournament();

            currentTournament.AddEntry(entry, userID);
        }

        private Tournament GetTournament()
        {
            // returns running tournament or creates new one
            foreach (Tournament joust in tournaments)
            {
                if (joust.Active) { return joust; }
            }
            Tournament newTournament = new Tournament(120);
            tournaments.Add(newTournament);
            return newTournament;
        }

        public Tournament GetLatestTournament()
        {
            // returns the currently active tournament
            if (!tournaments.Any()) return null;
            return tournaments.Last();
        }
    }
}
