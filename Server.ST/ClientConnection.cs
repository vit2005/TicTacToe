using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Parser;

namespace Server.ST
{
    public class ClientConnection
    {
        //fields
        private Game _game;
        private NetworkStream _networkStream;
        private string _name;
        private int _bufferSize = 1024;
        private Thread clientThreat;
        private bool goThreadGo = true;

        //properties
        public Game Game
        {
            set
            {
                _game = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }

        //constructors
        public ClientConnection(NetworkStream networkStream)
        {
            _networkStream = networkStream;
            string receivedLine = ReceiveMessage();
            _name = CSV.GetValueByName("Value", receivedLine);
            Console.WriteLine(receivedLine);
        }

        //methods
        public  void HandleMessage(string receivedString)
        {
            Console.WriteLine("{0}: Thread command: {1}", Thread.CurrentThread.ManagedThreadId, receivedString);

            switch (CSV.GetValueByName("Command", receivedString))
            {
                case "KillMe":
                    SendMessage("Command:ShutDown");
                    Listener.RemoveClient(_name);
                    break;
                case "Fight":
                    string senderName = CSV.GetValueByName("Sender", receivedString);
                    string receiverName = CSV.GetValueByName("Receiver", receivedString);
                    if (Listener.ListIsContain(senderName))
                    {
                        Listener.SendMessageTo(receiverName, string.Format("Sender:{0};Receiver:{1};Command:Fight", senderName, receiverName));
                    }
                    break;
                case "FightConfirmed":
                    ClientConnection clientA = Listener.RemoveClient(_name);
                    ClientConnection clientB = Listener.RemoveClient(CSV.GetValueByName("Receiver", receivedString));
                    _game = new Game(clientA, clientB);
                    break;
            }
        }
        
        public string ReceiveMessage()
        {
            string readResult;
            byte[] buffer = new byte[_bufferSize];
            var asyncResult = _networkStream.BeginRead(buffer, 0, buffer.Length, null, null);
            Thread.Sleep(50);
            if (asyncResult.IsCompleted)
            {
                readResult = Encoding.Unicode.GetString(buffer).TrimEnd('\0');
            }
            else
            {
                readResult = string.Empty;
            }
            return readResult;
        }

        public void SendMessage(string msg)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(msg);
            _networkStream.Write(buffer, 0, buffer.Length);
        }
    }
}
