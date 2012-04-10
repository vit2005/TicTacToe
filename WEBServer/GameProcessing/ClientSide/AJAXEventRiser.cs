//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using WEBServer.Messaging;
//using WEBServer.CustomEventArgs;

//namespace WEBServer.GameProcessing
//{
//    public class AJAXEventRiser
//    {
//        public void RiseEvent(string request, Client client)
//        {
//            switch (MyParser.CsvGetValue(request, "Operation"))
//            {
//                case "Connect":
//                    Connect(request, new ClientEventArgs(client));
//                    break;
//                case "Confirm":
//                    Confirm(request, new ClientEventArgs(client));
//                    break;
//                case "GetPlayers":
//                    GetPlayers(request, new ClientEventArgs(client));
//                    break;
//                case "Invite":
//                    Invite(request, new ClientEventArgs(client));
//                    break;
//                case "Reject":
//                    Reject(request, new ClientEventArgs(client));
//                    break;
//                case "StartGame":
//                    StartGame(request, new ClientEventArgs(client));
//                    break;
//                case "Move":
//                    Move(request, new ClientEventArgs(client));
//                    break;
//                case "Exit":
//                    Exit(request, new ClientEventArgs(client));
//                    break;
//                case "YourStep":
//                    YourStep(request, new ClientEventArgs(client));
//                    break;
//                case "OpponentStep":
//                    OpponentStep(request, new ClientEventArgs(client));
//                    break;
//                case "Win":
//                    Win(request, new ClientEventArgs(client));
//                    break;
//                case "Lose":
//                    Lose(request, new ClientEventArgs(client));
//                    break;
//                case "Continue":
//                    Continue(request, new ClientEventArgs(client));
//                    break;
//            }
//        }

//        //events
//        public event EventHandler<ClientEventArgs> Confirm;
//        public event EventHandler<ClientEventArgs> GetPlayers;
//        public event EventHandler<ClientEventArgs> Invite;
//        public event EventHandler<ClientEventArgs> Reject;
//        public event EventHandler<ClientEventArgs> StartGame;
//        public event EventHandler<ClientEventArgs> Move;
//        public event EventHandler<ClientEventArgs> Exit;
//        public event EventHandler<ClientEventArgs> YourStep;
//        public event EventHandler<ClientEventArgs> OpponentStep;
//        public event EventHandler<ClientEventArgs> Win;
//        public event EventHandler<ClientEventArgs> Lose;
//        public event EventHandler<ClientEventArgs> Continue;
//        public event EventHandler<ClientEventArgs> Connect;
//    }
//}
