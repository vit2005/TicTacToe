using System;
using System.Threading;
using Parser;

namespace Server.MT
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
            
            clientB.Abort();
            Console.WriteLine("B stoped");
            playerB.Game = this;

            gameThread = new Thread(TheGame);
            gameThread.Start();
            gameThread.Join();

            Listener.clientList.Add(clientA);
            Listener.clientList.Add(clientB);
            gameThread.Abort();
            playerB.ReRunClient();
        }

        //methods
        private void TheGame()
        {
            string recievedLine = string.Empty;
            while (true)
            {
                switch (whosTurn)
                {
                    case true:
                        clientA.SendMessage(string.Format("Command:YouCanTurn"));
                        recievedLine = clientA.ReceiveMessage();
                        break;
                    case false:
                        clientB.SendMessage(string.Format("Command:YouCanTurn"));
                        recievedLine = clientB.ReceiveMessage();
                        break;
                }

                Console.WriteLine("In Game Command: {0}", recievedLine);

                byte x = Convert.ToByte(CSV.GetValueByName("xAxis", recievedLine));
                byte y = Convert.ToByte(CSV.GetValueByName("yAxis", recievedLine));

                switch (whosTurn)
                {
                    case true:
                        DoMove(x, y, 'c', clientA, clientB);
                        break;
                    case false:
                        DoMove(x, y, 'z', clientB, clientA);
                        break;
                }

                whosTurn = !whosTurn;
            }
        }

        private void DoMove(byte x, byte y, char figure, ClientConnection sender, ClientConnection receiver)
        {
            field[x, y] = figure;
            sender.SendMessage(string.Format("Command:Turn;xAxis:{0};yAxis:{1};Figure:{2}", x, y, figure));
            receiver.SendMessage(string.Format("Command:Turn;xAxis:{0};yAxis:{1};Figure:{2}", x, y, figure));

            if (CheckResult(figure))
            {
                sender.SendMessage("Command:Win");
                receiver.SendMessage("Command:Lose");
            }
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
