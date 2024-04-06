using Sports_Exercise_Battle.DATAACCESS;
using Sports_Exercise_Battle.HTTP;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json;
using HttpMethod = Sports_Exercise_Battle.HTTP.HttpMethod; //prevents using the wrong HttpMethod

namespace Sports_Exercise_Battle.SEB
{
    public class UsersEndpoint : IHttpEndpoint
    {
        public bool HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            if (rq.Method == HttpMethod.POST)
            {
                // BLL: Insert User into database
                this.CreateUser(rq, rs);
                return true;
            }
            else if (rq.Method == HttpMethod.GET)
            {
                // check if /users/ or /users/*
                if (rq.Path[2] != null)
                {
                    // BLL: if username was given

                    // authenticate
                    try
                    {
                        DatabaseAuthenticate auth = new DatabaseAuthenticate(rq.Path[2], rq.Headers["Authorization"]);
                        if (auth.authenticated)
                        {
                            GetUserInfo(rq, rs);
                            rs.ResponseCode = 200;
                            rs.ResponseMessage = "OK";
                            rs.Headers.Add("Content-Type", "text/html");
                        }
                        else
                        {
                            rs.ResponseCode = 401;
                            rs.ResponseMessage = "Unauthorized";
                            rs.Content = "<html><body>Access token is missing or invalid</body></html>";
                            rs.Headers.Add("Content-Type", "text/html");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error in UsersEndpoint: " + ex.Message);
                        return false;
                    }
                } else
                {
                    return false;
                }
                
                return true;
            }
            return false; // if method is not "GET" or "POST"
        }

        public void CreateUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var user = JsonSerializer.Deserialize<User>(rq.Content ?? "");
                DatabaseCreateUser dbWrite = new DatabaseCreateUser(user);

                rs.ResponseCode = 201;
                rs.ResponseMessage = "Created";
                rs.Content = "<html><body>User created!</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
            catch (Exception)
            {
                rs.ResponseCode = 409;
                rs.ResponseMessage = "Conflict";
                rs.Content = "<html><body>User already exists!</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
        }

        public void GetUserInfo(HttpRequest rq, HttpResponse rs)
        {
            try
            { 
                DatabaseGetUserInfo dbContent = new DatabaseGetUserInfo(rq.Path[2]);
                rs.Content = JsonSerializer.Serialize<UserData>(dbContent.QueryReturn);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in GetUserInfo: " + e);
                rs.ResponseCode = 400;
                rs.ResponseMessage = "Bad Request";
            }
        }
    }
}
