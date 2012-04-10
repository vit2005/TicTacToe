//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using WEBServer.CustomEventArgs;
//using WEBServer.Messaging;

//namespace WEBServer.GameProcessing.ClientSide
//{
//    public class AJAXClientManager
//    {
//        //fields
//        AJAXEventRiser eventRiser = new AJAXEventRiser();

//        //properties
//        public AJAXEventRiser EventRiser
//        {
//            get { return eventRiser; }
//            set { eventRiser = value; }
//        }

//        //methods
//        public void NewConnection(object sender, ClientEventArgs e)
//        {
//            Client client = e.Client;
//            eventRiser.Confirm += new EventHandler<ClientEventArgs>(eventRiser_Confirm);
//            eventRiser.Continue += new EventHandler<ClientEventArgs>(eventRiser_Continue);
//            eventRiser.Exit += new EventHandler<ClientEventArgs>(eventRiser_Exit);
//            eventRiser.GetPlayers += new EventHandler<ClientEventArgs>(eventRiser_GetPlayers);
//            eventRiser.Invite += new EventHandler<ClientEventArgs>(eventRiser_Invite);
//            eventRiser.Lose += new EventHandler<ClientEventArgs>(eventRiser_Lose);
//            eventRiser.Move += new EventHandler<ClientEventArgs>(eventRiser_Move);
//            eventRiser.OpponentStep += new EventHandler<ClientEventArgs>(eventRiser_OpponentStep);
//            eventRiser.Reject += new EventHandler<ClientEventArgs>(eventRiser_Reject);
//            eventRiser.StartGame += new EventHandler<ClientEventArgs>(eventRiser_StartGame);
//            eventRiser.Win += new EventHandler<ClientEventArgs>(eventRiser_Win);
//            eventRiser.YourStep += new EventHandler<ClientEventArgs>(eventRiser_YourStep);
//            eventRiser.Connect += new EventHandler<ClientEventArgs>(eventRiser_Connect);
//        }

//        void eventRiser_Connect(object sender, ClientEventArgs e)
//        {
//            e.Client.SendServerMessage(new Command(Command.Commands.Connect, e.Client.Name));
//        }

//        void eventRiser_YourStep(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_Win(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_StartGame(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_Reject(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_OpponentStep(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_Move(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_Lose(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_Invite(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_GetPlayers(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_Exit(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_Continue(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }

//        void eventRiser_Confirm(object sender, ClientEventArgs e)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
