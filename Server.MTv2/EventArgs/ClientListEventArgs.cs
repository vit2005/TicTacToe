using System;
using System.Collections.Generic;

namespace Server.MTv2.MyEventArgs
{
    public class ClientListEventArgs: EventArgs
    {
        Dictionary<string, ClientThread> clientList;

        public ClientListEventArgs(Dictionary<string, ClientThread> clientList)
        {
            this.clientList = clientList;
        }

        public Dictionary<string, ClientThread> ClientList
        {
            get
            {
                return clientList;
            }
        }
    }
}
