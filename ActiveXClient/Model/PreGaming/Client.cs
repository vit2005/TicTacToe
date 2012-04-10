using System.Net;
using System.Net.Sockets;
using ActiveXClient.Model.Messaging;

namespace ActiveXClient.Model.PreGaming
{
    public class Client
    {
        //fields
        private TcpClient tcpClient;
        private Person person = new Person();
        private SocketMessageTransmitter messager = new SocketMessageTransmitter();

        //constructor
        public Client()
        {
            tcpClient = new TcpClient();
        }

        //properties
        public string Name
        {
            get { return person.Name; }
            set { person.Name = value; }
        }

        public bool HaveMessage
        {
            get { return tcpClient.Available > 0; }
        }

        public TcpClient Socket
        {
            get { return tcpClient; }
        }

        //methods
        public void Connect(IPEndPoint endPoint)
        {
            tcpClient.Connect(endPoint);
        }

        public void SendMessage(string message)
        {
            messager.SendMessage(tcpClient, message);
        }

        public string ReadMessage()
        {
            return messager.ReadMessage(tcpClient);
        }

        ~Client()
        {
            if (tcpClient != null)
            {
                tcpClient.Close();
            }
        }
    }
}
