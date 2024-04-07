using System;
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

        // Add properties and methods as needed
        public List<Session> OpenSessions { get; private set; } = new List<Session>();

        public void NewSession(string username, string token)
        {
            // adds new session to session pool
            Session session = new Session(username, token);
            OpenSessions.Add(session);
        }

        public string FindSessionByToken(string tokenString)
        {
            // strip first part of token
            string token = IsolateToken(tokenString);

            // finds session by token
            foreach (Session session in OpenSessions) {
                if (session.UserToken == token) return session.Username;
            }
            return null;
        }

        public string FindSessionByName(string username)
        {
            // finds session by username
            foreach (Session session in OpenSessions)
            {
                if (session.Username == username) return session.UserToken;
            }
            return null;
        }

        private string IsolateToken(string tokenString)
        {
            return tokenString.Replace("Basic ", "").Trim();
        }
    }
}
