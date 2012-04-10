using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.MTv2.Gaming.GameRullers
{
    public class SeaBattleRuller: IGameRuller
    {
        public bool GameFinished
        {
            get { throw new NotImplementedException(); }
        }

        public Client PlayerStep
        {
            get { throw new NotImplementedException(); }
        }

        public Client NextPlayerStep
        {
            get { throw new NotImplementedException(); }
        }

        public string StepPlayerGameField
        {
            get { throw new NotImplementedException(); }
        }

        public string NextPlayerGameField
        {
            get { throw new NotImplementedException(); }
        }

        public bool Move(string stepMsg, string name, out string stepPlayerMessage, out string nextPlayerMessage)
        {
            throw new NotImplementedException();
        }

        public void Init(Game game)
        {
            throw new NotImplementedException();
        }

        public event EventHandler GameOverEvent;
    }
}
