using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WEBServer.GameProcessing;
using WEBServer.DataBaseProcessing;

namespace WEBServer
{
    /// <summary>
    /// Process a request depending on request command.
    /// </summary>
    public class RequestFactory
    {
        GameProcessor tttProcessor = new GameProcessor();
        DBProcessor dbProcessor = new DBProcessor();

        public void ProcessRequest(object sender, EventArgs e)
        {
            TcpClient htmlClient = sender as TcpClient;
            string request = ReadRequest(htmlClient);
            if (request != string.Empty)
            {
                Console.WriteLine(request.Substring(0, request.IndexOf('\n')));
                Match match = Regex.Match(request, @"\b\S+\b");
                switch (match.Value)
                {
                    case "GET":
                        Match GETmatch = Regex.Match(request, @"/\S+");
                        GetProcessor getProcessor = new GetProcessor();
                        getProcessor.Initalize(GETmatch.Value.TrimStart('/'), htmlClient);
                        new Thread(getProcessor.ProcessWebRequest).Start();
                        break;
                    case "TTT":
                        Match TTTmatch = Regex.Match(request, @"[ ]\S+");
                        tttProcessor.ProcessTttRequest(TTTmatch.Value.TrimStart('/'), htmlClient);
                        break;
                    case "DB":
                        Match DBmatch = Regex.Match(request, @"/\S+");
                        dbProcessor.ProcessRequest(DBmatch.Value.TrimStart('/'), htmlClient);
                        break;
                }
            }
        }

        string ReadRequest(TcpClient tcpClient)
        {
            int count;
            byte[] buffer = new byte[1024];
            string request = string.Empty;
            while ((count = tcpClient.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, count);
                if (request.IndexOf("\r\n\r\n") >= 0)
                {
                    break;
                }
            }
            return request;
        }
    }
}
