using System.Threading;
using ActiveXClient.Model.Messaging;
using ActiveXClient.Model.PreGaming;

namespace ActiveXClient.Model.Gaming
{
    public class GameThread
    {
        //fields
        private Client client;
        private bool runFlag;
        private Protocol protocol;
        private GameEventRiser eventRiser;

        //constructors
        public GameThread(GameEventRiser eventRiser, Protocol protocol)
        {
            this.eventRiser = eventRiser;
            this.protocol = protocol;
        }

        //properties
        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

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
                if (client.HaveMessage)
                {
                    string message = client.ReadMessage();
                    Command command = protocol.ToCommand(message);
                    eventRiser.RiseEvent(command);
                }
                Thread.Sleep(300);
            }
        }

        ~GameThread()
        {
            Stop();
        }
    }
}
