using System;
using WindowsPhoneClient.Model.PreGaming;

namespace WindowsPhoneClient.Model.MyEventArgs
{
    public class GameEventArgs : EventArgs
    {
        //field
        private Client client;
        
        //constructors
        public GameEventArgs(Client client)
        {
            this.client = client;
        }

        //properties
        public Client Client
        {
            get { return client; }
        }
    }
}
