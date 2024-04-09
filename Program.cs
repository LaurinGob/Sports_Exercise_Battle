using Sports_Exercise_Battle.DATAACCESS;
using Sports_Exercise_Battle.HTTP;
using Sports_Exercise_Battle.SEB;
using System.Net;

namespace Sports_Exercise_Battle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sports Exercise Battle - Server: http://localhost:10001/");

            // ===== I. Start the HTTP-Server =====
            HttpServer httpServer = new HttpServer(IPAddress.Any, 10001);

            // register endpoints
            httpServer.RegisterEndpoint("users", new UsersEndpoint());
            httpServer.RegisterEndpoint("sessions", new SessionsEndpoint());
            httpServer.RegisterEndpoint("stats", new StatsEndpoint());
            httpServer.RegisterEndpoint("score", new ScoreEndpoint());
            httpServer.RegisterEndpoint("history", new HistoryEndpoint());
            httpServer.RegisterEndpoint("tournament", new TournamentEndpoint());

            httpServer.Run();

        }
    }
}