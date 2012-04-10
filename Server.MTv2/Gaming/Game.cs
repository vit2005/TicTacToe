
using Server.MTv2.Gaming.GameRullers;
using System;
namespace Server.MTv2.Gaming
{
    public class Game
    { 
        //fields
        private Client first;
        private Client second;
        private IGameRuller gameRuller;

        //constructors
        public Game (Client first, Client second, int order)
        {
            this.first = first;
            this.second = second;
            gameRuller = RullerFactory.GetInstance(first.GameType);
            gameRuller.Init(this);
            gameRuller.GameOverEvent += GameOver;
        }

        //properties
        public int ContinueFlag { get; set; }
        public bool IsClosed { get; set; }
        public Client First { get { return first; } }
        public Client Second { get { return second; } }
        public Client StepPlayer { get { return gameRuller.PlayerStep; } }
        public Client NextStepPlayer { get { return gameRuller.NextPlayerStep; } }
        public bool IsFinished { get { return gameRuller.GameFinished; } }
        public bool HasMessage { get { return first.Socket.Available > 0 || second.Socket.Available > 0; } }
        public string StepPlayerGameField { get { return gameRuller.StepPlayerGameField; } }
        public string NextPlayerGameField { get { return gameRuller.NextPlayerGameField; } }

        //methods
        public bool ContainUser(string name)
        {
            return first.Name == name || second.Name == name;
        }

        public bool Move(string step, string name, out string stepPlayerMessage, out string nextPlayerMessage)
        {
            bool result = gameRuller.Move(step, name, out stepPlayerMessage, out nextPlayerMessage);
            if (gameRuller.GameFinished)
            {
                result = false;
            }
            return result;
        }
        
        private void GameOver(object result, EventArgs e)
        {
            GameFinishEvent(this, result);
        }

        //enumerations 
        public enum GameOverResult
        {
            FirstWin,
            SecondWin,
            Tie
        }

        public enum GameType
        {
            TicTacToe,
            SeaBattle,
            Drafts
        }

        //events
        public delegate void GameFinish(Game game, object result);
        public event GameFinish GameFinishEvent;
    }
}