
using Server.MTv2.Messaging;
namespace Server.MTv2.Gaming
{
    public class GameEventRiser
    {
        //methods
        public void RiseEvent(Game game, Client client, Command command)
        {
            switch (command.CurrentCommand)
            {
                case "Confirm":
                    ConfirmEvent(game, client);
                    break;
                case "Reject":
                    RejectEvent(game, client);
                    break;
                case "Move":
                    MoveEvent(game, client, command.Data);
                    break;
                case "Exit":
                    ExitEvent(game, client);
                    break;
            }
        }

        //events
        public delegate void Confirm(Game game, Client client);
        public event Confirm ConfirmEvent;

        public delegate void Reject(Game game, Client client);
        public event Reject RejectEvent;

        public delegate void Move(Game game, Client client, string message);
        public event Move MoveEvent;

        public delegate void Exit(Game game, Client client);
        public event Exit ExitEvent;
    }
}