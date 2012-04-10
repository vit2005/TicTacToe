using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WindowsPhoneClient.Model.Messaging
{
    public class SocketMessageTransmitter
    {
        //fields
        static ManualResetEvent clientDone = new ManualResetEvent(false);
        SocketAsyncEventArgs socketEventArgs;

        //methods
        public void Connect(ref Socket socket, IPEndPoint endPoint)
        {
            string result = string.Empty;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketEventArgs = new SocketAsyncEventArgs();
            socketEventArgs.RemoteEndPoint = endPoint;
            socketEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(
                delegate(object sender, SocketAsyncEventArgs e)
                {
                    result = e.SocketError.ToString();
                    clientDone.Set();
                });
            clientDone.Reset();
            socket.ConnectAsync(socketEventArgs);
            clientDone.WaitOne(5000);
        }

        public void SendMessage(Socket socket, string message)
        {

            string responce = string.Empty;
            socketEventArgs = new SocketAsyncEventArgs();
            socketEventArgs.RemoteEndPoint = socket.RemoteEndPoint;
            socketEventArgs.UserToken = null;
            socketEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(
                delegate(object sender, SocketAsyncEventArgs e)
                {
                    responce = e.SocketError.ToString();
                    clientDone.Set();
                });
            byte[] byteMessage = Encoding.Unicode.GetBytes(message);
            socketEventArgs.SetBuffer(byteMessage, 0, byteMessage.Length);
            clientDone.Reset();
            socket.SendAsync(socketEventArgs);
            clientDone.WaitOne(5000);
        }

        public string ReadMessage(Socket socket)
        {
            string message = string.Empty;
            socketEventArgs = new SocketAsyncEventArgs();
            socketEventArgs.RemoteEndPoint = socket.RemoteEndPoint;
            socketEventArgs.SetBuffer(new Byte[2048], 0, 2048);
            socketEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(
                delegate(object sender, SocketAsyncEventArgs e)
                {
                    if (e.SocketError == SocketError.Success)
                    {
                        message = Encoding.Unicode.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                        message = message.Trim('\0');
                    }
                    clientDone.Set();
                });
            clientDone.Reset();
            socket.ReceiveAsync(socketEventArgs);
            clientDone.WaitOne(5000);
            return message;
        }
    }
}
