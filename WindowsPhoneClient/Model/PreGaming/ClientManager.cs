using System;
using System.Collections.Generic;
using System.Net;
using WindowsPhoneClient.Model.Messaging;
using WindowsPhoneClient.Model.MyEventArgs;

namespace WindowsPhoneClient.Model.PreGaming
{
    public class ClientManager
    {
        //fields
        private ClientThread clientThread;
        private Protocol protocol = new Protocol();
        private SocketMessageTransmitter messanger = new SocketMessageTransmitter();
        private ClientEventRiser eventRiser = new ClientEventRiser();
        private IntermidiateLayer intermidiate;

        //constructors
        public ClientManager(MainPage mainPage)
        {
            intermidiate = new IntermidiateLayer(mainPage);
            clientThread = new ClientThread(eventRiser);
            clientThread.Protocol = protocol;

            eventRiser.ConfirmEvent += new ClientEventRiser.Confirm(Confirm);
            eventRiser.GetPlayersEvent += new ClientEventRiser.GetPlayers(GetPlayers);
            eventRiser.InviteEvent += new ClientEventRiser.Invite(Invite);
            eventRiser.RejectEvent += new ClientEventRiser.Reject(Reject);
            eventRiser.StartGameEvent += new ClientEventRiser.StartGame(StartGame);
        }

        //methods
        public bool Login(IPEndPoint ipEndPoint, string name)
        {
            clientThread.Client.Connect(ipEndPoint);
            clientThread.Client.Name = name;

            Command command = new Command(Command.Commands.Connect, clientThread.Client.Name);
            string message = protocol.ToMessage(command);
            messanger.SendMessage(clientThread.Client.Socket, message);

            message = messanger.ReadMessage(clientThread.Client.Socket);
            command = protocol.ToCommand(message);

            return command.CurrentCommand == Command.Commands.Confirm.ToString();
        }

        public void Exit()
        {
            Command command = new Command(Command.Commands.Exit, "");
            string message = protocol.ToMessage(command);
            messanger.SendMessage(clientThread.Client.Socket, message);
            clientThread.Stop();
        }

        public void SendInvite(string name)
        {
            Command command = new Command(Command.Commands.Invite, name);
            string message = protocol.ToMessage(command);
            messanger.SendMessage(clientThread.Client.Socket, message);
        }

        public void RunClientThread()
        {
            clientThread.Start();
        }

        void Invite(string invitersName)
        {
            Command command = intermidiate.HandleInvite(invitersName)
                ? new Command(Command.Commands.Confirm, invitersName)
                : new Command(Command.Commands.Reject, invitersName);
            string message = protocol.ToMessage(command);
            messanger.SendMessage(clientThread.Client.Socket, message);
        }

        void GetPlayers(string data)
        {
            List<Person> personList = protocol.MessageToPersonList(data);
            intermidiate.UpdatePlayersList(personList);
        }

        void Confirm()
        {
            throw new NotImplementedException();
        }

        void Reject()
        {
            throw new NotImplementedException();
        }

        void StartGame(string figure)
        {
            if (figure == "0")
            {
                intermidiate.SetFigures('X','0');

            }
            else
            {
                intermidiate.SetFigures('0','X');
            }
            NewGame(null, new GameEventArgs(clientThread.Client));
            clientThread.Stop();
        }

        ~ClientManager()
        {
            clientThread.Stop();
        }

        //events
        public event EventHandler<GameEventArgs> NewGame;
    }
}
