using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WEBServer
{
    /// <summary>
    /// Waiting for a new HTML client connection.
    /// </summary>
    public class Server : IThread
    {
        //fields
        bool runFlag;
        TcpListener tcpListener;

        //constructors
        public Server(IPEndPoint endPoint)
        {
            tcpListener = new TcpListener(endPoint);
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
            tcpListener.Start();
            while (runFlag)
            {
                NewRequest(tcpListener.AcceptTcpClient(), new EventArgs());
            }
        }

        ~Server()
        {
            Stop();
            tcpListener.Stop();
        }

        //events
        public event EventHandler NewRequest;
    }
}
