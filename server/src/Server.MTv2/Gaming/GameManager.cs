using System;
using System.Collections.Generic;
using System.Threading;
using Server.MTv2.Messaging;
using Server.MTv2.MyEventArgs;
using Server.MTv2.Gaming.GameRullers;

namespace Server.MTv2.Gaming
{
    public class GameManager
    {
        //fields 
        private List<Game> gameList;
        private bool runFlag;
        Protocol protocol;
        GameEventRiser eventRiser;

        //constructors
        public GameManager()
        {
            protocol = new Protocol();
            gameList = new List<Game>();
            eventRiser = new GameEventRiser();

            eventRiser.MoveEvent += new GameEventRiser.Move(Move);
            eventRiser.ConfirmEvent += new GameEventRiser.Confirm(Confirm);
            eventRiser.RejectEvent += new GameEventRiser.Reject(Reject);
            eventRiser.ExitEvent += new GameEventRiser.Exit(Exit);
        }

        //methods
        public void Start()
        {
            runFlag = true;
            new Thread(Run).Start();
        }

        public void Stop()
        {
            runFlag = false;
        }

        public void NewGame(Client first, Client second)
        {
            Random random = new Random();
            int order = random.Next(0, 2);
            first.SendMessage(protocol.ToMessage(new Command(Command.Commands.StartGame, order.ToString())));
            second.SendMessage(protocol.ToMessage(new Command(Command.Commands.StartGame, (order ^ 1).ToString())));
            
            Game game = new Game(first, second, order);
            game.GameFinishEvent += GameFinish;
            gameList.Add(game);

            //move to game
            new Thread(delegate()
                {
                    Thread.Sleep(500);

                    if (order == 1)
                    {
                        first.SendMessage(protocol.ToMessage(new Command(Command.Commands.YourStep, game.StepPlayerGameField)));
                        second.SendMessage(protocol.ToMessage(new Command(Command.Commands.OpponentStep, game.NextPlayerGameField)));
                    }
                    else
                    {
                        first.SendMessage(protocol.ToMessage(new Command(Command.Commands.OpponentStep, game.StepPlayerGameField)));
                        second.SendMessage(protocol.ToMessage(new Command(Command.Commands.YourStep, game.NextPlayerGameField)));
                    }
                }).Start();
        }

        private void Run()
        {
            while (runFlag)
            {
                try
                {
                    for (int i = 0; i < gameList.Count; i++)
                    {
                        if (gameList[i].HasMessage)
                        {
                            if (gameList[i].First.HasMessage)
                                eventRiser.RiseEvent(gameList[i], gameList[i].First, protocol.ToCommand(gameList[i].First.ReadMessage()));
                            if (gameList[i].Second.HasMessage)
                                eventRiser.RiseEvent(gameList[i], gameList[i].Second, protocol.ToCommand(gameList[i].Second.ReadMessage()));
                        }
                    }
                }
                catch { }
                Thread.Sleep(300);
            }
        }

        public bool ContainUser(string name)
        {
            bool result = false;
            foreach (Game game in gameList)
            {
                if (game.ContainUser(name))
                {
                    result = true;
                }
            }
            return result;
        }

        private void Reject(Game game, Client client)
        {
            gameList.Remove(game);
            game.Second.SendMessage(protocol.ToMessage(new Command(Command.Commands.Exit, "")));
            game.First.SendMessage(protocol.ToMessage(new Command(Command.Commands.Exit, "")));
            FreeUserEvent(client);
        }

        private void Exit(Game game, Client client)
        {
            gameList.Remove(game);
            game.First.SendMessage(protocol.ToMessage(new Command(Command.Commands.Exit, "")));
            game.Second.SendMessage(protocol.ToMessage(new Command(Command.Commands.Exit, "")));
            FreeUserEvent(game.First);
            FreeUserEvent(game.Second);
        }

        private void Confirm(Game game, Client client)
        {
            if (game.IsClosed)
            { return; }
            if (game.IsFinished)
            { game.ContinueFlag++; }
            if (game.ContinueFlag == 2)
            {
                gameList.Remove(game);
                Client first = game.First;
                Client second = game.Second;
                Thread.Sleep(50);
                NewGame(first, second);
            }
        }

        private void Move(Game game, Client client, string message)
        {
            string messageToStepPlayer, messageToNextStepPlayer;
            if (game.Move(message, client.Name, out messageToStepPlayer, out messageToNextStepPlayer))
            {
                game.StepPlayer.SendMessage(protocol.ToMessage(new Command(Command.Commands.YourStep, messageToStepPlayer)));
                game.NextStepPlayer.SendMessage(protocol.ToMessage(new Command(Command.Commands.OpponentStep, messageToNextStepPlayer)));
            }
            else
            {
                Stop();
            }
        }

        private void GameFinish(Game game, object result)
        {
            switch (result as string)
            {
                case "FirstWin":
                    game.First.SendMessage(protocol.ToMessage(new Command(Command.Commands.Win, "")));
                    game.Second.SendMessage(protocol.ToMessage(new Command(Command.Commands.Lose, "")));
                    break;
                case "SecondWin":
                    game.First.SendMessage(protocol.ToMessage(new Command(Command.Commands.Lose, "")));
                    game.Second.SendMessage(protocol.ToMessage(new Command(Command.Commands.Win, "")));
                    break;
                case "Tie":
                    game.First.SendMessage(protocol.ToMessage(new Command(Command.Commands.Tie, "")));
                    game.Second.SendMessage(protocol.ToMessage(new Command(Command.Commands.Tie, "")));
                    break;
            }

            GameOverEvent(null, new GameOverEventArgs(game));

            new Thread(delegate()
            {
                Thread.Sleep(300);
                game.First.SendMessage(protocol.ToMessage(new Command(Command.Commands.Continue, "")));
                game.Second.SendMessage(protocol.ToMessage(new Command(Command.Commands.Continue, "")));
            }).Start();
        }

        //events
        public delegate void FreeUser(Client client);
        public event FreeUser FreeUserEvent;
        public event EventHandler<GameOverEventArgs> GameOverEvent;
    }
}
