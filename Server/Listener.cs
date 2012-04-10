using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server.MT
{
    public class Listener
    {
        //fields
        private TcpListener tcpListener;
        private TcpClient tcpClient;
        public static List<ClientConnection> clientList = new List<ClientConnection>();

        //constructors
        public Listener (IPAddress ipAddr, int port)
        {
            tcpListener = new TcpListener(ipAddr, port);
            tcpListener.Start();
            Console.WriteLine("Server satrted on {0}:{1}", ipAddr, port);
            Thread listenThread = new Thread(new ThreadStart(Listen));
            listenThread.Start();
        }

        //methods
        private void Listen()
        {
            while (true)
            {
                tcpClient = tcpListener.AcceptTcpClient();
                ClientConnection client = new ClientConnection();
                clientList.Add(client);
                client.RunClient(tcpClient.GetStream());
            }
        }

        public static string GetClients()
        {
            string enemies = string.Empty;
            foreach (var client in clientList)
            {
                enemies += string.Format("Enemy:{0};", client.Name);
            }
            return enemies.TrimEnd(';');
        }

        public static ClientConnection RemoveClient(string name)
        {
            ClientConnection res = null;
            for (int i = 0; i < clientList.Count; i++)
            {
                if (clientList[i].Name == name)
                {
                    res = clientList[i];
                    clientList.Remove(clientList[i]);
                    break;
                }
            }
            return res;
        }

        public static bool ListIsContain(string name)
        {
            bool res = false;
            foreach (var client in clientList)
            {
                if (client.Name == name)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public static void SendMessageTo(string name, string msg)
        {
            foreach (var client in clientList)
            {
                if (client.Name == name)
                {
                    client.SendMessage(msg);
                    break;
                }
            }
        }

        public static void SendClientListToAll()
        {
            foreach (var client in clientList)
            {
                client.SendMessage(string.Format("Command:Enemies;{0}", GetClients()));
            }
        }
    }
}