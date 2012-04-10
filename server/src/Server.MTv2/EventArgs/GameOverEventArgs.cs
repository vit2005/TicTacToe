using System;
using Server.MTv2.Gaming;

namespace Server.MTv2.MyEventArgs
{
    public class GameOverEventArgs : EventArgs
    {
        Game game;
        public GameOverEventArgs(Game game)
        {
            this.game = game;
        }

        public Game Game
        {
            get { return game; }
        }
    }
}
