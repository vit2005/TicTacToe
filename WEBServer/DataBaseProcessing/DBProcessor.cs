using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using WEBServer.Messaging;
using System.IO;
using System.Text.RegularExpressions;

namespace WEBServer.DataBaseProcessing
{
    public class DBProcessor
    {
        SocketMessageTransmitter messenger = new SocketMessageTransmitter();


        public void ProcessRequest(string request, TcpClient htmlClient)
        {
            switch (request)
            {
                case "all":
                    string message = string.Empty;
                    using (StreamReader sr = new StreamReader("names.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            message += sr.ReadLine() + "<br />";
                        }
                    }
                    messenger.SendMessageASCII(htmlClient, message);
                    break;
                default:
                    string message2 = string.Empty;
                    using (StreamReader sr = new StreamReader("names.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            string tempMsg = sr.ReadLine() + "<br />";
                            if (Regex.IsMatch(tempMsg, string.Format("\\S*{0}\\S*", request)))
                            {
                                message2 += tempMsg;
                            }
                        }
                    }
                    messenger.SendMessageASCII(htmlClient, message2);
                    break;
            }
        }
    }
}
