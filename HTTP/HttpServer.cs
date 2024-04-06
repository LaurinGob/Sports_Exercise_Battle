﻿using System.Net;
using System.Net.Sockets;

namespace Sports_Exercise_Battle.HTTP
{
    public class HttpServer
    {
        private readonly int port = 8000;
        private readonly IPAddress ip = IPAddress.Loopback;

        private TcpListener tcpListener;
        public Dictionary<string, IHttpEndpoint> Endpoints { get; private set; } = new Dictionary<string, IHttpEndpoint>();


        public HttpServer(IPAddress ip, int port)
        {
            this.port = port;
            this.ip = ip;

            tcpListener = new TcpListener(ip, port);
        }

        public void Run()
        {
            tcpListener.Start();
            while (true)
            {
                // ----- 0. Accept the TCP-Client and create the reader and writer -----
                var clientSocket = tcpListener.AcceptTcpClient();
                var httpProcessor = new HttpProcessor(this, clientSocket);
                // Use ThreadPool to make it multi-threaded
                ThreadPool.QueueUserWorkItem(o => httpProcessor.Process());
            }
        }

        public void RegisterEndpoint(string path, IHttpEndpoint endpoint)
        {
            Endpoints.Add(path, endpoint);
        }
    }
}
