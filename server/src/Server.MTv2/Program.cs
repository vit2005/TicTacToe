#define DEBUG

using System;
using System.Net;
using System.Net.Sockets;
using Server.MTv2.Gaming;

namespace Server.MTv2
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            Listener listener = new Listener(new TcpListener(ipAddr, 10060));
            ClientManager clientManager = new ClientManager();
            GameManager gameManager = new GameManager();

            new Server(listener, clientManager, gameManager).Start();

            Console.WriteLine("Running on {0}:10060", ipAddr.ToString());
            Console.ReadKey();
        }
    }
}
