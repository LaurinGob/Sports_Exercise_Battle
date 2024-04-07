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
    public class ScoreEndpoint : IHttpEndpoint
    {
        public bool HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            if (rq.Method == HttpMethod.GET)
            {
                GetUserScore(rq, rs);
                return true;
            }
            return false; // if method is other than post
        }

        public void GetUserScore(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var userCredentials = JsonSerializer.Deserialize<User>(rq.Content ?? "");
                // db access check password
                // DatabaseGetUserScore score = new DatabaseGetUserScore(userCredentials);
                rs.ResponseCode = 200;
                rs.ResponseMessage = "OK";
                // rs.Content = JsonSerializer.Serialize<UserScore>(stats.UserScore);
                rs.Headers.Add("Content-Type", "application/json");
            }
            catch (Exception)
            {
                rs.ResponseCode = 401;
                rs.ResponseMessage = "Unauthorized";
                rs.Content = "<html><body>Invalid Username/Password</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
        }
    }
}
