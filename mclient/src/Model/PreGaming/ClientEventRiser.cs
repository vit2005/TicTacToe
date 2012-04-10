using WindowsPhoneClient.Model.Messaging;

namespace WindowsPhoneClient.Model.PreGaming
{
    public class ClientEventRiser
    {
        //method
        public void RiseEvent(Command command)
        {
            switch (command.CurrentCommand)
            {
                case "GetPlayers":
                    GetPlayersEvent(command.Data);
                    break;
                case "Invite":
                    InviteEvent(command.Data);
                    break;
                case "Confirm":
                    ConfirmEvent();
                    break;
                case "Reject":
                    RejectEvent();
                    break;
                case "StartGame":
                    StartGameEvent(command.Data);
                    break;
            }
        }

        //events
        public delegate void GetPlayers(string players);
        public event GetPlayers GetPlayersEvent;

        public delegate void Invite(string invitersName);
        public event Invite InviteEvent;

        public delegate void Confirm();
        public event Confirm ConfirmEvent;

        public delegate void Reject();
        public event Reject RejectEvent;

        public delegate void StartGame(string message);
        public event StartGame StartGameEvent;
    }
}
