﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports_Exercise_Battle.SEB
{
    public class BLL_SessionManager
    {
        private static readonly Lazy<BLL_SessionManager> lazyInstance = new Lazy<BLL_SessionManager>(() => new BLL_SessionManager());

        // Private constructor to prevent instantiation
        private BLL_SessionManager() { }

        public static BLL_SessionManager Instance => lazyInstance.Value;

        public List<Session> OpenSessions { get; private set; } = new List<Session>();

        public void NewSession(int user_id, string username, string token, string profileName, int elo)
        {
            // adds new session to session pool
            Session session = new Session(user_id, username, token, profileName, elo);
            OpenSessions.Add(session);
        }

        public void UpdateSession(string username, string profileName)
        {
            // finds session by username
            foreach (Session session in OpenSessions)
            {
                if (session.Username == username)
                {
                    session.ProfileName = profileName;
                }
            }
        }

        public int GetELO(int userID)
        {
            // finds session by username
            foreach (Session session in OpenSessions)
            {
                if (session.UserID == userID)
                {
                    return session.Elo;
                }
            }
            return 0;
        }

        public string GetUsername(int userID)
        {
            foreach (Session session in OpenSessions)
            {
                if (session.UserID == userID)
                {
                    return session.Username;
                }
            }
            return "";
        }

        public void UpdateElo(string username, int elo)
        {
            // finds session by username
            foreach (Session session in OpenSessions)
            {
                if (session.Username == username)
                {
                    session.Elo += elo;
                }
            }
        }

        public string FindUsernameByToken(string tokenString)
        {
            // strip first part of token
            string token = IsolateToken(tokenString);

            // finds session by token
            foreach (Session session in OpenSessions) {
                if (session.UserToken == token) return session.Username;
            }
            return null;
        }

        public string FindProfilenameByToken(string tokenString)
        {
            // strip first part of token
            string token = IsolateToken(tokenString);

            // finds session by token
            foreach (Session session in OpenSessions)
            {
                if (session.UserToken == token) return session.ProfileName;
            }
            return null;
        }

        public string FindTokenByUsername(string username)
        {
            // finds session by username
            foreach (Session session in OpenSessions)
            {
                if (session.Username == username) return session.UserToken;
            }
            return null;
        }

        public int GetUserID(string tokenString)
        {
            string token = IsolateToken(tokenString);
            // finds userid by token
            foreach (Session session in OpenSessions)
            {
                if (session.UserToken == token) return session.UserID;
            }
            return 0;
        }

        private string IsolateToken(string tokenString)
        {
            // helper function to isolate the token from the rest of the string
            return tokenString.Replace("Basic ", "").Trim();
        }
    }
}
