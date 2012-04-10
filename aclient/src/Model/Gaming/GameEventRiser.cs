using DesktopClient.Model.Messaging;

namespace DesktopClient.Model.Gaming
{
    public class GameEventRiser
    {
        //methods
        public void RiseEvent(Command command)
        {
            switch (command.CurrentCommand)
            {
                case "Move":
                    MoveEvent(command.Data);
                    break;
                case "Exit":
                    ExitEvent();
                    break;
                case "StartGame":
                    StartGameEvent();
                    break;
                case "YourStep":
                    YourStepEvent(command.Data);
                    break;
                case "OpponentStep":
                    OpponentStepEvent(command.Data);
                    break;
                case "Win":
                    WinEvent();
                    break;
                case "Lose":
                    LoseEvent();
                    break;
                case "Continue":
                    ContinueEvent();
                    break;
            }
        }

        //events
        public delegate void Move(string message);
        public event Move MoveEvent;

        public delegate void Exit();
        public event Exit ExitEvent;

        public delegate void StartGame();
        public event StartGame StartGameEvent;

        public delegate void YourStep(string move);
        public event YourStep YourStepEvent;

        public delegate void OpponentStep(string move);
        public event OpponentStep OpponentStepEvent;

        public delegate void Win();
        public event Win WinEvent;

        public delegate void Lose();
        public event Lose LoseEvent;

        public delegate void Continue();
        public event Continue ContinueEvent;
    }
}
