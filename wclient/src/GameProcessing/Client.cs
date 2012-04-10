using System.Net.Sockets;
using WEBServer.Messaging;

namespace WEBServer.GameProcessing
{
    public class Client
    {
        //fields
        string name;
        TcpClient httpClient;
        TcpClient serverClient;
        SocketMessageTransmitter messenger = new SocketMessageTransmitter();

        //constructors
        public Client(string name, TcpClient httpClient, TcpClient serverClient)
        {
            this.name = name;
            this.httpClient = httpClient;
            this.serverClient = serverClient;
        }

        //properties
        public TcpClient HttpSocket
        {
            get { return httpClient; }
            set { httpClient = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool HttpIsConnected
        {
            get { return httpClient.Connected; }
        }

        //methods
        public void SendHtmlMessage(string message)
        {
            try
            {
                messenger.SendMessageASCII(httpClient, message);
                httpClient.GetStream().Close();
            }
            catch { }
        }

        public void SendServerMessage(string message)
        {
            messenger.SendMessage(serverClient, message);
        }

        public string ReadServerMessage()
        {
            return messenger.ReadMessage(serverClient);
        }
    }
}
