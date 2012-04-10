using System;
using System.Net.Sockets;

namespace Server.MTv2.MyEventArgs
{
    public class TcpClientEventArgs : EventArgs
    {
        //fields
        private TcpClient tcpClient;

        //constructor
        public TcpClientEventArgs(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        //properties
        public TcpClient Client { get { return tcpClient; } }
    }
}
