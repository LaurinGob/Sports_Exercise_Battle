﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using HttpMethod = Sports_Exercise_Battle.HTTP.HttpMethod;
using Sports_Exercise_Battle.DATAACCESS;
using Sports_Exercise_Battle.HTTP;

namespace Sports_Exercise_Battle.SEB
{
    public class TournamentHistoryEndpoint : IHttpEndpoint
    {
        public bool HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            if (rq.Method == HttpMethod.GET)
            {
                GetPastTournaments(rq, rs);
                return true;
            }
            return false; // if method is other than get
        }

        public void GetPastTournaments(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                // authenticate
                BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
                string username = SessionManager.FindUsernameByToken(rq.Headers["Authorization"]);
                if (username == null) { throw new Exception("User not logged in"); }

                // get and return tournament status
                DatabaseGetPastTournament pastTournaments = new DatabaseGetPastTournament();

                rs.ResponseCode = 200;
                rs.ResponseMessage = "OK";
                rs.Content = JsonSerializer.Serialize<List<PastTournament>>(pastTournaments.QueryReturn);
                rs.Headers.Add("Content-Type", "application/json");
            }
            catch (Exception)
            {
                rs.ResponseCode = 401;
                rs.ResponseMessage = "Unauthorized";
                rs.Content = "<html><body>Access token is missing or invalid</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
        }
    }
}
