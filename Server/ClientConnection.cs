using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Parser;

namespace Server.MT
{
    public class ClientConnection
    {
        //fields
        private Game _game;
        private NetworkStream _networkStream;
        private string _name;
        private int _bufferSize = 1024;
        public Thread clientThreat;
        private bool inGame = false;

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

        //methods
        public void RunClient(NetworkStream networkStream)
        {
            this._networkStream = networkStream;
            clientThreat = new Thread(ClientThread);
            clientThreat.Start();
        }

        public void ReRunClient()
        {
            clientThreat.Start();
        }

        private void ClientThread()
        {
            _name = CSV.GetValueByName("Value", ReceiveMessage());
            Listener.SendClientListToAll();
            Console.WriteLine("{0}: Client {1} has been succesfully connected", Thread.CurrentThread.ManagedThreadId, _name);

            while (true)
            {
                string receivedString = ReceiveMessage();
                Console.WriteLine("{0}: Thread command: {1}", Thread.CurrentThread.ManagedThreadId, receivedString);

                switch (CSV.GetValueByName("Command", receivedString))
                {
                    case "KillMe":
                        SendMessage("Command:ShutDown");
                        Listener.RemoveClient(_name);
                        Thread.CurrentThread.Abort();
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
        }

        public string ReceiveMessage()
        {
            byte[] buffer = new byte[_bufferSize];
            _networkStream.Read(buffer, 0, buffer.Length);
            _networkStream.Flush();
            return Encoding.Unicode.GetString(buffer).TrimEnd('\0');
        }

        public void SendMessage(string msg)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(msg);
            _networkStream.Write(buffer, 0, buffer.Length);
            _networkStream.Flush();
        }

        public void Join()
        {
            clientThreat.Join();
        }

        public void Abort()
        {
            clientThreat.Abort();
        }
    }
}
