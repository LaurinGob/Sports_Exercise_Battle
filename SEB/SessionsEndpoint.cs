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
    public class SessionsEndpoint : IHttpEndpoint
    {
        public bool HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            if (rq.Method == HttpMethod.POST)
            {
                LoginUsers(rq, rs);
                return true;
            }
            return false; // if method is other than post
        }

        public void LoginUsers(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var userCredentials = JsonSerializer.Deserialize<User>(rq.Content ?? "");
                // db access check passwort
                DatabaseLogin login = new DatabaseLogin(userCredentials);
                rs.ResponseCode = 201;
                rs.ResponseMessage = "OK";
                rs.Content = "<html><body>Successfully logged in!</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
            catch (Exception)
            {
                rs.ResponseCode = 401;
                rs.ResponseMessage = "OK";
                rs.Content = "<html><body>Invalid username/password!</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
        }
    }
}
