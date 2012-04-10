
using Server.MTv2.Messaging;
namespace Server.MTv2
{
    public class ClientEventRiser
    {
        //methods
        public void RiseEvent(Client client, Command command)
        {
            switch (command.CurrentCommand)
            {
                case "GetPlayers":
                    GetPlayersEvent(client);
                    break;
                case "Invite":
                    InviteEvent(client, command.Data);
                    break;
                case "Confirm":
                    ConfirmEvent(client);
                    break;
                case "Reject":
                    RejectEvent(client);
                    break;
                case "Exit":
                    ExitEvent(client);
                    break;
            }
        }

        //events
        public delegate void GetPlayers(Client client);
        public event GetPlayers GetPlayersEvent;

        public delegate void Invite(Client client, string destinationName);
        public event Invite InviteEvent;

        public delegate void Confirm(Client client);
        public event Confirm ConfirmEvent;

        public delegate void Reject(Client client);
        public event Reject RejectEvent;

        public delegate void Exit(Client client);
        public event Exit ExitEvent;

    }
}
