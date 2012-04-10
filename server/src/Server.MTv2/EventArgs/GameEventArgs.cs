using System;

namespace Server.MTv2.MyEventArgs
{
    public class GameEventArgs : EventArgs
    {
        //fields
        private Client first;
        private Client second;

        //constructors
        public GameEventArgs(Client first, Client second)
        {
            this.first = first;
            this.second = second;
        }

        //properties
        public Client First
        {
            get { return first; }
            set { first = value; }
        }

        public Client Second
        {
            get { return second; }
            set { second = value; }
        }
    }
}
