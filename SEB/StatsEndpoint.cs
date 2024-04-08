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
    public class StatsEndpoint : IHttpEndpoint
    {
        public bool HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            if (rq.Method == HttpMethod.GET)
            {
                GetUserStats(rq, rs);
                return true;
            }
            return false; // if method is other than get
        }

        public void GetUserStats(HttpRequest rq, HttpResponse rs)
        { 
            try
            {
                // get session manager
                BLL_SessionManager SessionManager = BLL_SessionManager.Instance;
                string username = SessionManager.FindUsernameByToken(rq.Headers["Authorization"]);
                if (username == null) { throw new Exception("User not logged in"); }

                DatabaseGetUserStats stats = new DatabaseGetUserStats(username);
                rs.ResponseCode = 200;
                rs.ResponseMessage = "OK";
                rs.Content = JsonSerializer.Serialize<UserStats>(stats.QueryReturn);
                rs.Headers.Add("Content-Type", "application/json");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in StatsEndpoint: " + ex.Message);
                rs.ResponseCode = 401;
                rs.ResponseMessage = "Unauthorized";
                rs.Content = "<html><body>Access token is missing or invalid</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
        }
    }
}
