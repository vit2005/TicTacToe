using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Server.MTv2.Messaging;
using Server.MTv2.MyEventArgs;

namespace Server.MTv2
{
    public class ClientManager
    {
        //fields
        private Dictionary<string, ClientThread> clientList = new Dictionary<string, ClientThread>();

        //propreties
        public static bool MainFlag { get; private set; }

        public Dictionary<string, ClientThread> ClientList { get { return clientList; } }

        //methods
        public void Start()
        {
            MainFlag = true;
        }

        public void Stop()
        {
            MainFlag = false;
        }

        public void AddClient(Client client)
        {
            ClientThread clientThread = new ClientThread();
            clientThread.Client = client;
            clientThread.Protocol = new Protocol();
            clientThread.EventRiser = new ClientEventRiser();
            clientThread.EventRiser.GetPlayersEvent += new ClientEventRiser.GetPlayers(GetPlayers);
            clientThread.EventRiser.InviteEvent += new ClientEventRiser.Invite(Invite);
            clientThread.EventRiser.ConfirmEvent += new ClientEventRiser.Confirm(Confirm);
            clientThread.EventRiser.RejectEvent += new ClientEventRiser.Reject(Reject);
            clientThread.EventRiser.ExitEvent += new ClientEventRiser.Exit(Exit);
            clientList.Add(client.Name, clientThread);
            clientThread.Start();
        }

        void Exit(Client client)
        {
            clientList[client.Name].Stop();
            clientList.Remove(client.Name);
            //ClientListChanged(null, new ClientListEventArgs(clientList));
        }

        public void SetSocket(TcpClient socket, string name)
        {
            clientList[name].Client.Socket = socket;
        }

        private void Exit(string name)
        {
            ClientThread clientThread = clientList[name];
            clientThread.IsRunning = false;
            clientList.Remove(name);
            //ClientListChanged(null, new ClientListEventArgs(clientList));
        }

        public bool ContainUser(string name)
        {
            return clientList.ContainsKey(name);
        }

        private void GetPlayers(Client client)
        {
            List<Person> personList = new List<Person>();
            foreach (KeyValuePair<string, ClientThread> keyValuePair in clientList)
            {
                if (keyValuePair.Value.Client.Name != client.Name)
                {
                    personList.Add(new Person(keyValuePair.Value.Client.Name, keyValuePair.Value.Client.GameType));
                }
            }

            string data = clientList[client.Name].Protocol.PersonListToMessage(personList);
            Command command = new Command(Command.Commands.GetPlayers, data);
            string message = clientList[client.Name].Protocol.ToMessage(command);
            client.SendMessage(message);
        }

        private void Invite(Client client, string destinationName)
        {
            if (client.Name != destinationName)
            {
                clientList[destinationName].Client.InviterName = client.Name;
                Command command = new Command(Command.Commands.Invite, client.Name);
                clientList[destinationName].Client.SendMessage(clientList[destinationName].Protocol.ToMessage(command));
            }
        }

        private void Confirm(Client client)
        {
            if (client.Name != string.Empty)
            {
                Client secondClient = clientList[client.InviterName].Client;
                Exit(client.InviterName);
                Exit(client.Name);
                secondClient.InviterName = string.Empty;
                client.InviterName = string.Empty;
                NewGame(null, new GameEventArgs(client, secondClient));
            }
        }

        private void Reject(Client client)
        {
            Command command = new Command(Command.Commands.Reject, "");
            string message = clientList[client.Name].Protocol.ToMessage(command);
            clientList[client.Name].Client.SendMessage(message);
            client.InviterName = string.Empty;
        }

        //events
        public event EventHandler<GameEventArgs> NewGame;
        //public event EventHandler<ClientListEventArgs> ClientListChanged;
    }
}