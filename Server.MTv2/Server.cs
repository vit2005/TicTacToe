using System;
using System.Net.Sockets;
using Server.MTv2.MyEventArgs;
using Server.MTv2.ObserverNotifier;
using Server.MTv2.Messaging;
using Server.MTv2.Gaming;

namespace Server.MTv2
{
    public class Server
    {
        //fields
        private SocketMessageTransmitter messager;
        private Protocol protocol;
        private Listener listener;
        private Notifier notifier;
        private ClientManager clientManager;
        private GameManager gameManager;
        private LoginEventRiser loginEventRiser;

        //constructors
        public Server(Listener listener, ClientManager clientManager, GameManager gameManager)
        {
            this.listener = listener;
            this.clientManager = clientManager;
            this.gameManager = gameManager;

            messager = new SocketMessageTransmitter();
            protocol = new Protocol();
            notifier = new Notifier(protocol);
            loginEventRiser = new LoginEventRiser();


            loginEventRiser.ConnectEvent += Connect;
            listener.NewConnection += NewConnection;
            clientManager.NewGame += NewGame;
            gameManager.GameOverEvent += GameOver;
            gameManager.FreeUserEvent += clientManager.AddClient;
        }

        //methods
        public void Start()
        {
            listener.Start();
            clientManager.Start();
            gameManager.Start();
        }

        private void Stop()
        {
            if (listener != null)
                listener.Stop();
            if (clientManager != null)
                clientManager.Stop();
            if (gameManager != null)
                gameManager.Stop();
        }

        private void Connect(string info, TcpClient socket)
        {
            string[] parts = info.Split('|');
            string name;
            string gameType;
            if (parts.Length != 2)
            {
                messager.SendMessage(socket, protocol.ToMessage(new Command(Command.Commands.Reject, "")));
            }
            else
            {
                name = parts[0];
                gameType = parts[1];
                if (clientManager.ContainUser(name))
                {
                    clientManager.SetSocket(socket, name);
                }
                else
                {
                    clientManager.AddClient(new Client(socket, name, gameType));
                }
                messager.SendMessage(socket, protocol.ToMessage(new Command(Command.Commands.Confirm, "")));
            }
        }

        private void NewConnection(object sender, TcpClientEventArgs e)
        {
            loginEventRiser.RiseEvent(e.Client, protocol.ToCommand(messager.ReadMessage(e.Client)));
            notifier.Notify(null, new ClientListEventArgs(clientManager.ClientList));
        }

        private void NewGame(object o, GameEventArgs e)
        {
            if (e.First.GameType == e.Second.GameType)
            {
                gameManager.NewGame(e.First, e.Second);
            }
        }

        private void GameOver(object o, GameOverEventArgs e)
        {
            clientManager.AddClient(e.Game.First);
            clientManager.AddClient(e.Game.Second);
        }

        ~Server()
        {
            Stop();
        }
    }
}