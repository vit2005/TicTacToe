using System;
using System.Net.Sockets;
using System.Threading;
using Server.MTv2.MyEventArgs;

namespace Server.MTv2
{
    /// <summary>
    /// Listener is listening for a new client connection. 
    /// When it appears NewConnection event is invoked.
    /// </summary>
    public class Listener
    {
        //fields
        private bool runFlag = true;
        private TcpListener tcpListener;

        //constructors
        public Listener(TcpListener tcpListener)
        {
            this.tcpListener = tcpListener;
        }

        //methods
        private void Run()
        {
            while (runFlag)
            {
                NewConnection(null, new TcpClientEventArgs(tcpListener.AcceptTcpClient()));
            }
        }

        public void Start()
        {
            runFlag = true;
            tcpListener.Start();
            new Thread(Run).Start();
        }

        public void Stop()
        {
            runFlag = false;
            tcpListener.Stop();
        }

        ~Listener()
        {
            Stop();
        }

        //events
        public event EventHandler<TcpClientEventArgs> NewConnection;
    }
}
