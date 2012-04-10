using DesktopClient.Model.Messaging;
using DesktopClient.Model.MyEventArgs;

namespace DesktopClient.Model.Gaming
{
    public class GameManager
    {
        //fields
        private GameEventRiser eventRiser = new GameEventRiser();
        private Protocol protocol = new Protocol();
        private GameThread gameThread;
        private IntermidiateLayer intermidiateLayer;

        //constructors
        public GameManager(MainWindow mainWindow)
        {
            intermidiateLayer = new IntermidiateLayer(mainWindow);
            gameThread = new GameThread(eventRiser, protocol);

            eventRiser.YourStepEvent += new GameEventRiser.YourStep(YourStep);
            eventRiser.OpponentStepEvent += new GameEventRiser.OpponentStep(OpponentStep);
            eventRiser.ExitEvent += new GameEventRiser.Exit(Exit);
            eventRiser.WinEvent += new GameEventRiser.Win(Win);
            eventRiser.LoseEvent += new GameEventRiser.Lose(Lose);
            eventRiser.ContinueEvent += new GameEventRiser.Continue(Continue);
        }

        void Continue()
        {
            intermidiateLayer.DisableButtons();
            intermidiateLayer.CleanButtonsContent();
            protocol.ToMessage(new Command(Command.Commands.Connect, gameThread.Client.Name));
        }

        void Lose()
        {
            intermidiateLayer.Lose();
        }

        void Win()
        {
            intermidiateLayer.Win();
        }


        //methods
        public void NewGame(object sender, GameEventArgs e)
        {
            gameThread.Client = e.Client;
            gameThread.Start();
        }

        //merge following two methods
        void OpponentStep(string move)
        {
            intermidiateLayer.EnableButtons();
            if (move != string.Empty)
            {
                intermidiateLayer.MakeMove(move, Model.Move.MoveEnum.Opponent);
            }
        }

        void YourStep(string move)
        {
            intermidiateLayer.EnableButtons();
            if (move != string.Empty)
            {
                intermidiateLayer.MakeMove(move, Model.Move.MoveEnum.Your);
            }
        }

        public void Exit()
        {
            intermidiateLayer.DisableButtons();
            gameThread.Stop();
            intermidiateLayer.StartNonGamingClient();
        }

        public void Move(int position)
        {
            Command command = new Command(Command.Commands.Move, position.ToString());
            string message = protocol.ToMessage(command);
            gameThread.Client.SendMessage(message);
        }
    }
}
