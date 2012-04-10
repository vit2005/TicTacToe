using System;
using System.Net.Sockets;
using Server.MTv2.Messaging;

namespace Server.MTv2
{
    public class LoginEventRiser
    {
        //fields
        public void RiseEvent(TcpClient client, Command command)
        {
            switch (command.CurrentCommand)
            {
                case "Connect":
                    ConnectEvent(command.Data, client);
                    break;
            }
        }

        //events
        public delegate void Connect(string info, TcpClient socket);
        public event Connect ConnectEvent;
    }
}
