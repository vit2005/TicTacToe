using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.MTv2.Gaming.GameRullers
{
    public interface IGameRuller
    {
        /// <summary>
        /// If game is finished. Returns true.
        /// </summary>
        bool GameFinished { get; }
        /// <summary>
        /// Returns step player.
        /// </summary>
        Client PlayerStep { get; }
        /// <summary>
        /// Returns next step player.
        /// </summary>
        Client NextPlayerStep { get; }
        /// <summary>
        /// Returns randomly generated game field to step player.
        /// </summary>
        string StepPlayerGameField { get; }
        /// <summary>
        /// Returns randomly generated game field to next step player;
        /// </summary>
        string NextPlayerGameField { get; }

        /// <summary>
        /// Makes move in game field. And reverse references StepPlayer and 
        /// NextPlayer.
        /// </summary>
        /// <param name="stepMsg">Message from client</param>
        /// <param name="name">Player name who maked move.</param>
        /// <param name="stepPlayerMessage">Message to step player.</param>
        /// <param name="nextPlayerMessage">Message to next step player.</param>
        /// <returns>True when moved. False is game ends.</returns>
        bool Move(string stepMsg, string name, out string stepPlayerMessage, out string nextPlayerMessage);
        /// <summary>
        /// Initalise game ruller.
        /// </summary>
        /// <param name="game">Game class.</param>
        void Init(Game game);

        /// <summary>
        /// Fire when game overs. with "FirstWin", "SecondWin" or "Tie"
        /// instead of object sender. EventArgs can be null.
        /// </summary>
        event EventHandler GameOverEvent;
    }
}
