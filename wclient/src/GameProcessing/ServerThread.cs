using System.Threading;

namespace WEBServer.GameProcessing
{
    public class ServerThread : IThread
    {
        //fields
        bool runFlag;
        Client client;

        //constructors
        public ServerThread(Client client)
        {
            this.client = client;
        }

        //properties
        public Client Client { get { return client; } }

        //methods
        public void Start()
        {
            runFlag = true;
            new Thread(Run).Start();
        }

        public void Stop()
        {
            runFlag = false;
        }

        void Run()
        {
            while (runFlag)
            {
                string message = client.ReadServerMessage();
                if (message != string.Empty)
                {
                    while (true)
                    {
                        if (client.HttpIsConnected)
                        {
                            client.SendHtmlMessage(message);
                            break;
                        }
                        Thread.Sleep(100);
                    }
                    
                }
                Thread.Sleep(300);
            }
        }

        ~ServerThread()
        {
            Stop();
        }
    }
}
