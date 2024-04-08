using System;
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
    public class HistoryEndpoint : IHttpEndpoint
    {
        public bool HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            if (rq.Method == HttpMethod.GET)
            {
                GetUserHistory(rq, rs);
                return true;
            } else if (rq.Method == HttpMethod.POST)
            {
                NewHistoryEntry(rq, rs);
                return true;
            }
            return false; // if method is other than post or get
        }

        public void GetUserHistory(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                // get session manager
                BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
                string username = SessionManager.FindUsernameByToken(rq.Headers["Authorization"]);
                if (username == null) { throw new Exception("User not logged in"); }

                DatabaseGetHistory score = new DatabaseGetHistory(username);
                rs.ResponseCode = 200;
                rs.ResponseMessage = "OK";
                rs.Content = JsonSerializer.Serialize<List<UserHistory>>(score.QueryReturn);
                rs.Headers.Add("Content-Type", "application/json");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in HistoryEndpoint GET: " + ex.Message);
                rs.ResponseCode = 401;
                rs.ResponseMessage = "Unauthorized";
                rs.Content = "<html><body>Access token is missing or invalid</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
        }

        public void NewHistoryEntry(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                // check authentification
                BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
                string username = SessionManager.FindUsernameByToken(rq.Headers["Authorization"]);
                if (username == null) { throw new Exception("User not logged in"); }

                // get userid
                int user_id = SessionManager.GetUserID(rq.Headers["Authorization"]);
                if (user_id == 0) { throw new Exception("User not logged in"); }

                var userHistory = JsonSerializer.Deserialize<UserHistory>(rq.Content ?? "");
                DatabaseCreateHistory dbWrite = new DatabaseCreateHistory(userHistory, user_id);

                // enter entry into tournament
                string profileName = SessionManager.FindProfilenameByToken(rq.Headers["Authorization"]);
                if (profileName == null) { throw new Exception("User not logged in"); }

                BLL_TournamentManager TournamentManager = BLL_TournamentManager.Instance;
                TournamentManager.NewTournamentEntry(new TournamentEntry(userHistory, profileName));

                rs.ResponseCode = 201;
                rs.ResponseMessage = "Created";
                rs.Content = "<html><body>History entry added!</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in HistoryEndpoint POST: " + ex.Message);
                rs.ResponseCode = 401;
                rs.ResponseMessage = "Unauthorized";
                rs.Content = "<html><body>Access token is missing or invalid</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
        }
    }
}
