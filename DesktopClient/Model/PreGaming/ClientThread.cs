﻿using System.Threading;
using DesktopClient.Model.Messaging;

namespace DesktopClient.Model.PreGaming
{
    public class ClientThread
    {
        //fields
        private Client client = new Client();
        private bool runFlag;
        private Protocol protocol;
        private ClientEventRiser eventRiser;

        //constructors
        public ClientThread(ClientEventRiser eventRiser)
        {
            this.eventRiser = eventRiser;
        }

        //properties
        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public Protocol Protocol
        {
            set
            {
                protocol = value;
            }
        }

        //method
        public void Start()
        {
            runFlag = true;
            new Thread(Run).Start();
        }

        public void Stop()
        {
            runFlag = false;
        }

        private void Run()
        {
            while (runFlag)
            {
                if (client.HaveMessage)
                {
                    string message = client.ReadMessage();
                    Command command = protocol.ToCommand(message);
                    eventRiser.RiseEvent(command);
                }
                Thread.Sleep(300);
            }
        }

        ~ClientThread()
        {
            Stop();
        }
    }
}
