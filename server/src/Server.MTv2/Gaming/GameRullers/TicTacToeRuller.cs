
using System;
namespace Server.MTv2.Gaming.GameRullers
{
    class TicTacToeRuller: IGameRuller
    {
        //fields
        private bool gameFinish;
        private int figure = -1;
        private int stepCount = 0;
        private int[] gamePole = new int[9];
        private Game game;
        private Client stepPlayer;
        private Client nextStepPlayer;

        //properties
        public bool GameFinished { get { return gameFinish; } }
        public Client PlayerStep { get { return stepPlayer; } }
        public Client NextPlayerStep { get { return nextStepPlayer; } }
        public string StepPlayerGameField { get { return string.Empty; } }
        public string NextPlayerGameField { get { return string.Empty; } }

        //methods
        public void Init(Game game)
        {
            this.game = game;
            stepPlayer = game.First;
            nextStepPlayer = game.Second;
        }

        public bool Move(string stepMsg, string name, out string stepPlayerMessage, out string nextPlayerMessage)
        {
            bool result = false;
            stepPlayerMessage = stepMsg;
            nextPlayerMessage = stepMsg;
            int step = int.Parse(stepMsg);
            if (step < 9 && step > -1 && gamePole[step] == 0 && !gameFinish)
            {
                gamePole[step] = figure;
                figure = -figure;
                stepCount++;
                CheckStatus();
                result = true;
            }
            return result;
        }

        private void CheckStatus()
        {
            if (gamePole[0] != 0 && gamePole[0] == gamePole[1] && gamePole[1] == gamePole[2])
            {
                GameFinish();
            }
            else if (gamePole[3] != 0 && gamePole[3] == gamePole[4] && gamePole[4] == gamePole[5])
            {
                GameFinish();
            }
            else if (gamePole[6] != 0 && gamePole[6] == gamePole[7] && gamePole[7] == gamePole[8])
            {
                GameFinish();
            }
            else if (gamePole[0] != 0 && gamePole[0] == gamePole[3] && gamePole[3] == gamePole[6])
            {
                GameFinish();
            }
            else if (gamePole[1] != 0 && gamePole[1] == gamePole[4] && gamePole[4] == gamePole[7])
            {
                GameFinish();
            }
            else if (gamePole[2] != 0 && gamePole[2] == gamePole[5] && gamePole[5] == gamePole[8])
            {
                GameFinish();
            }
            else if (gamePole[0] != 0 && gamePole[0] == gamePole[4] && gamePole[4] == gamePole[8])
            {
                GameFinish();
            }
            else if (gamePole[6] != 0 && gamePole[6] == gamePole[4] && gamePole[4] == gamePole[2])
            {
                GameFinish();
            }
            else if (stepCount == 9)
            {
                gameFinish = true;
                GameOverEvent(Game.GameOverResult.Tie, null);
            }
            else
            {
                Client tmp = PlayerStep;
                stepPlayer = nextStepPlayer;
                nextStepPlayer = tmp;
            }
        }

        private void GameFinish()
        {
            gameFinish = true;
            if (stepPlayer.Name == game.First.Name)
            {
                GameOverEvent(Game.GameOverResult.FirstWin, null);
            }
            else
            {
                GameOverEvent(Game.GameOverResult.SecondWin, null);
            }
        }

        //events
        //public delegate void GameOver(Game.GameOverResult result);
        public event EventHandler GameOverEvent;
    }
}
