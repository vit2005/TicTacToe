using System;
using System.Threading;

namespace Server.ST
{
    public class Game
    {
        //fields
        private ClientConnection clientA;
        private ClientConnection clientB;
        private char[,] field = new char[3, 3];
        private bool whosTurn;
        private Thread gameThread;

        //constructors
        public Game(ClientConnection playerA, ClientConnection playerB)
        {
            Console.WriteLine("Game created!");
            Random rnd = new Random();
            whosTurn = Convert.ToBoolean(rnd.Next(2));

            this.clientA = playerA;
            this.clientB = playerB;
            playerB.Game = this;

            StartThread();
        }

        //methods
        private void TheGame()
        {
            while (true)
            {
                string coords = whosTurn ? clientA.ReceiveMessage() : clientB.ReceiveMessage();

                string[] coordsArr = coords.Split(';');
                byte x = Convert.ToByte(coordsArr[0]);
                byte y = Convert.ToByte(coordsArr[1]);

                if (whosTurn)
                {
                    DoMove(x, y, 'c', clientA, clientB);
                }
                else
                {
                    DoMove(x, y, 'z', clientB, clientA);
                }

                whosTurn = !whosTurn;
            }
        }

        private void DoMove(byte x, byte y, char figure, ClientConnection sender, ClientConnection receiver)
        {
            field[x, y] = figure;
            sender.SendMessage(string.Format("Turn?{0};{1}?{2}", x, y, figure));
            receiver.SendMessage(string.Format("Turn?{0};{1}?{2}", x, y, figure));

            if (CheckResult(figure))
            {
                sender.SendMessage("Win");
                receiver.SendMessage("Lose");
                StopThread();
            }
        }

        private void StartThread()
        {
            gameThread = new Thread(TheGame);
            gameThread.Start();
        }

        private void StopThread()
        {
            Listener.clientList.Add(clientA);
            Listener.clientList.Add(clientB);
            gameThread.Abort();
        }

        private bool CheckResult(char figure)
        {
            bool res = false;
            for (byte i = 0; i < 3; i++)
            {
                if (field[i, 0] == field[i, 1] && field[i, 1] == field[i, 2] && field[i, 2] == figure)
                {
                    res = true;
                }
            }
            for (byte i = 0; i < 3; i++)
            {
                if (field[0, i] == field[1, i] && field[1, i] == field[2, i] && field[2, i] == figure)
                {
                    res = true;
                }
            }

            if (field[0, 0] == field[1, 1] && field[1, 1] == field[2, 2] && field[2, 2] == figure)
            { res = true; }

            if (field[2, 0] == field[1, 1] && field[1, 1] == field[0, 2] && field[2, 2] == figure)
            { res = true; }

            return res;
        }
    }
}
