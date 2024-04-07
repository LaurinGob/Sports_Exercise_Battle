using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sports_Exercise_Battle.SEB
{
    public class Session
    {
        public string Username { get; set; }
        public string UserToken { get; set; }
        public Session(string username, string token)
        {
            Username = username;
            UserToken = token;
        }
    }
}