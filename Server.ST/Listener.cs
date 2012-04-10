using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server.ST
{
    public class Listener
    {
        private TcpListener tcpListener;

        public static List<ClientConnection> clientList = new List<ClientConnection>();

        public Listener(IPAddress ipAddr, int port)
        {
            tcpListener = new TcpListener(ipAddr, port);
            tcpListener.Start();
            Console.WriteLine("Server satrted at {0}:{1}", ipAddr, port);
            Thread listen = new Thread(new ThreadStart(Listen));
            listen.Start();
            DoEverything();
        }

        private void Listen()
        {
            while (true)
            {
                ClientConnection client = new ClientConnection(tcpListener.AcceptTcpClient().GetStream());
                clientList.Add(client);
                Console.WriteLine("Client connected");
                SendClientListToAll();
            }
        }

        private void DoEverything()
        {
            string receivedLine;
            while (true)
            {
                for (int i = 0; i < clientList.Count;i++ )
                {
                    if ((receivedLine = clientList[i].ReceiveMessage()) != string.Empty)
                    {
                        clientList[i].HandleMessage(receivedLine);
                    }
                }
                Thread.Sleep(100);
            }
        }

        public static string GetClients()
        {
            string enemies = string.Empty;
            for (int i = 0; i < clientList.Count; i++)
            {
                enemies += string.Format("Enemy:{0};", clientList[i].Name);
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
            for (int i = 0; i < clientList.Count; i++)
            {
                if (clientList[i].Name == name)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public static void SendMessageTo(string name, string msg)
        {
            for (int i = 0; i < clientList.Count; i++)
            {
                if (clientList[i].Name == name)
                {
                    clientList[i].SendMessage(msg);
                    break;
                }
            }
        }

        public static void SendClientListToAll()
        {
            for (int i = 0; i < clientList.Count; i++)
            {
                clientList[i].SendMessage(string.Format("Command:Enemies;{0}", GetClients()));
            }
        }
    }
}