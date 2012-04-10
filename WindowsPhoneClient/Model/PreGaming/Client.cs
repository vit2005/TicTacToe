using System.Net;
using System.Net.Sockets;
using WindowsPhoneClient.Model.Messaging;

namespace WindowsPhoneClient.Model.PreGaming
{
    public class Client
    {
        //fields
        private Socket socket;
        private Person person = new Person();
        private SocketMessageTransmitter messager = new SocketMessageTransmitter();

        //properties
        public string Name
        {
            get { return person.Name; }
            set { person.Name = value; }
        }

        //fix this
        public bool HaveMessage
        {
            get { return true; }
        }

        public Socket Socket
        {
            get { return socket; }
        }

        //methods
        public void Connect(IPEndPoint endPoint)
        {
            messager.Connect(ref socket, endPoint);
        }

        public void SendMessage(string message)
        {
            messager.SendMessage(socket, message);
        }

        public string ReadMessage()
        {
            return messager.ReadMessage(socket);
        }

        ~Client()
        {
            if (socket != null)
            {
                socket.Close();
            }
        }
    }
}
