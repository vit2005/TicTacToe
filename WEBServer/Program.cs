using System;
using System.Net;

namespace WEBServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Command command = new Command(Command.Commands.Move, "0");
            //string message = new Protocol().ToMessage(command);




            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8088);
            Console.WriteLine("Running on {0}", endPoint.ToString());

            Server server = new Server(endPoint);
            RequestFactory requestFactory = new RequestFactory();
            server.NewRequest += requestFactory.ProcessRequest;
            server.Start();
        }
    }
}
