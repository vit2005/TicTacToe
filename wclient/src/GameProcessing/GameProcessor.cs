using System;
using System.Net;
using System.Net.Sockets;
using WEBServer.CustomEventArgs;
using WEBServer.Messaging;

namespace WEBServer.GameProcessing
{
    public class GameProcessor
    {
        //fields 
        ClientList clientList;
        ServerThreadManager serverThreadManager;

        //constructorrs
        public GameProcessor()
        {
            clientList = new ClientList();
            serverThreadManager = new ServerThreadManager();
            NewClient += serverThreadManager.NewConnection;
        }

        //methods
        public void ProcessTttRequest(string request, TcpClient htmlClient)
        {
            string name = MyParser.CsvGetValue(request, "name");
            if (clientList.Contain(name))
            {
                clientList.UpdateHttpSocket(name, htmlClient);
                if (MyParser.CanGetServerCommand(request))
                {
                    clientList[name].SendServerMessage(MyParser.GetServerCommand(request));
                }
            }
            else
            {
                try
                {
                    AddNewConnection(htmlClient, request);
                    clientList[name].SendServerMessage(MyParser.GetServerCommand(request));
                }
                catch { }
            }
        }

        public void AddNewConnection(TcpClient htmlClient, string request)
        {
            int port = int.Parse(MyParser.CsvGetValue(request, "port"));
            IPAddress ipAddress = IPAddress.Parse(MyParser.CsvGetIP(request));
            string name = MyParser.CsvGetValue(request, "name");

            TcpClient serverClient = new TcpClient();
            serverClient.Connect(new IPEndPoint(ipAddress, port));
            Client client = new Client(name, htmlClient, serverClient);
            clientList.Add(client);

            NewClient(null, new ClientEventArgs(client));
        }

        //events
        public event EventHandler<ClientEventArgs> NewClient;
    }
}
