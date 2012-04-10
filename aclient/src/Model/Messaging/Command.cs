using System;

namespace DesktopClient.Model.Messaging
{
    public class Command
    {
        //fields
        //private Commands commands;

        //constructors
        public Command(Commands command, string data)
        {
            Data = data;
            CurrentCommand = Enum.GetName(typeof(Commands), command);
        }

        //properties
        public string CurrentCommand { get; set; }
        public string Data { get; set; }

        //enumerators
        public enum Commands
        {
            Connect,
            GetPlayers,
            Exit,
            Invite,
            Win,
            Lose,
            Tie,
            Confirm,
            Reject,
            Move,
            YourStep,
            OpponentStep,
            Continue,
            StartGame
        }
    }
}
