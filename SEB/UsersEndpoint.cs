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
                if (rq.Path[2] == null)
                {
                    // BLL: get Users from database
                    this.GetUser(rq, rs);
                } else if (rq.Path[2] != null)
                {
                    // BLL: if username was given

                    // authenticate
                    DatabaseAuthenticate auth = new DatabaseAuthenticate(rq.Path[2], rq.Headers["Authorization"]);
                    if (auth.authenticated)
                    {
                        Console.WriteLine("Welcome user!");
                    } else
                    {
                        Console.WriteLine("Crap");
                    }
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
                rs.ResponseMessage = "OK";
                rs.Content = "<html><body>User created!</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
            catch (Exception)
            {
                rs.ResponseCode = 409;
                rs.ResponseMessage = "OK";
                rs.Content = "<html><body>User already exists!</body></html>";
                rs.Headers.Add("Content-Type", "text/html");
            }
        }
        public void GetUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var user = JsonSerializer.Deserialize<User>(rq.Content ?? "");
                Console.WriteLine(user);
                //DatabaseGetUser dbContent = new DatabaseGetUser(user.Username);
            } 
            catch (Exception e)
            {
                Console.WriteLine("Error in UsersEndpoint: " + e);
                rs.ResponseCode = 400;
                rs.ResponseMessage = "OK";
            }
        }
    }
}
