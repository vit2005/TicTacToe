using System;
using System.Linq;
using System.Net;

namespace Server.MT
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddr = Dns.Resolve(Dns.GetHostName()).AddressList.First();
            int port = 10060;
            try
            {
                Listener listener = new Listener(ipAddr, port);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}