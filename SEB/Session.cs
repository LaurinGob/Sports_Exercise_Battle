using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sports_Exercise_Battle.SEB
{
    public class Session
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string UserToken { get; set; }
        public string ProfileName { get; set; }
        public int Elo {  get; set; }

        public Session(int user_id, string username, string token, string profileName, int elo)
        {
            UserID = user_id;
            Username = username;
            UserToken = token;
            ProfileName = profileName;
            Elo = elo;
        }
    }
}