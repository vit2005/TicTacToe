using System;
using System.Threading;
using Server.MTv2.Messaging;

namespace Server.MTv2
{
    public class ClientThread
    {
        //properties
        public bool IsRunning {get;set;}
        public Client Client { get; set; }
        public ClientEventRiser EventRiser { get; set; }
        public Protocol Protocol { get; set; }

        //methods
        public void Start()
        {
            IsRunning = true;
            new Thread(Run).Start();
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private void Run()
        {
            while (IsRunning)
            {
                try
                {
                    if (Client.HasMessage && ClientManager.MainFlag)
                    {
                        string message = Client.ReadMessage();
                        Command command = Protocol.ToCommand(message);
                        EventRiser.RiseEvent(Client, command);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("I am in ClientThread\n{0}", ex.Message);
                }
                Thread.Sleep(300);
            }
        }


    }
}