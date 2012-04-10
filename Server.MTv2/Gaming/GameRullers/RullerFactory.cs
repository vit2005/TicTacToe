using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.MTv2.Gaming.GameRullers
{
    public class RullerFactory
    {
        public static IGameRuller GetInstance(string gameType)
        {
            switch (gameType)
            {
                case "TicTacToe":
                    return new TicTacToeRuller();
                case "SeaBattle":
                    return new SeaBattleRuller();
                case "Drafts":
                    return new DraftsRuller();
                default:
                    return null;
            }
        }
    }
}
