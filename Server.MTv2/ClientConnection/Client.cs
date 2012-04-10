using System.Net.Sockets;
using Server.MTv2.Messaging;

namespace Server.MTv2
{
    public class Client
    {
        //fields
        private string name;
        private string gameType;
        private TcpClient tcpClient;
        private SocketMessageTransmitter socketTransmitter = new SocketMessageTransmitter();

        //constructors
        public Client(TcpClient tcpClient, string name, string gameType)
        {
            this.tcpClient = tcpClient;
            this.name = name;
            this.gameType = gameType;
        }

        //properties
        public string Name
        {
            get { return name; }
        }
        public string GameType
        {
            get { return gameType; }
        }
        public TcpClient Socket
        {
            get { return tcpClient; }
            set { tcpClient = value; }
        }
        public bool HasMessage
        {
            get { return tcpClient.Available > 0; }
        }
        public string InviterName { get; set; }

        //methods
        public string ReadMessage()
        {
            return socketTransmitter.ReadMessage(tcpClient);
        }

        public void SendMessage(string message)
        {
            socketTransmitter.SendMessage(tcpClient, message);
        }

        ~Client()
        {
            tcpClient.GetStream().Close();
            tcpClient.Close();
        }
    }
}
