﻿using System;
using System.Net.Sockets;
using System.Text;

namespace Server.MTv2.Messaging
{
    public class SocketMessageTransmitter
    {
        //methods
        public void SendMessage(TcpClient tcpClient, string message)
        {
            try
            {
                byte[] buffer = Encoding.Unicode.GetBytes(message);
                tcpClient.GetStream().Write(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("I am in SocketTransmitter.\n{0}", e.Message);
            }
        }

        public string ReadMessage(TcpClient tcpClient)
        {
            byte[] buffer = new byte[1024];
            try
            {
                tcpClient.GetStream().Read(buffer, 0, buffer.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("I am in SocketTransmitter.\n{0}", e.Message);
            }

            return Encoding.Unicode.GetString(buffer).TrimEnd('\0');
        }
    }
}
