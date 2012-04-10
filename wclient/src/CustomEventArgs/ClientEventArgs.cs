using System;
using WEBServer.GameProcessing;

namespace WEBServer.CustomEventArgs
{
    public class ClientEventArgs : EventArgs
    {
        private Client client;
        public ClientEventArgs(Client client)
        {
            this.client = client;
        }
        public Client Client
        {
            get { return this.client; }
        }
    }
}
