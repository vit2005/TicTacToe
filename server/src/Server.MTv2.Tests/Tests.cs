using System.Linq;
using System.Net;
using System.Net.Sockets;
using NUnit.Framework;
using Server.MTv2.Messaging;
using Server.MTv2.Gaming;

namespace Server.MTv2.Tests
{
    [TestFixture]
    public class Tests
    {
        IPEndPoint endPoint;
        TcpClient tcpClientA;
        TcpClient tcpClientB;
        Server server;
        SocketMessageTransmitter messenger;
        Protocol protocol;

        [SetUp]
        public void SetUp()
        {
            IPAddress ipAddr = Dns.Resolve(Dns.GetHostName()).AddressList.First();
            endPoint = new IPEndPoint(ipAddr, 10060);
            tcpClientA = new TcpClient();
            tcpClientB = new TcpClient();
            messenger = new SocketMessageTransmitter();
            protocol = new Protocol();
        }

        [Test]
        public void ServerTest()
        {
            ClientManager clientManager = new ClientManager();
            GameManager gameManager = new GameManager();
            Listener listener = new Listener(new TcpListener(endPoint));
            server = new Server(listener, clientManager, gameManager);
            server.Start();
            tcpClientA.Connect(endPoint);
            tcpClientB.Connect(endPoint);
            Assert.AreEqual(true, tcpClientA.Connected);
        }

        [Test]
        public void LoginTest()
        {
            Command commandA = new Command(Command.Commands.Connect, "AName");
            string messageA = protocol.ToMessage(commandA);
            messenger.SendMessage(tcpClientA, messageA);
            string responseA = messenger.ReadMessage(tcpClientA);
            Assert.AreEqual("123", responseA);

            Command commandB = new Command(Command.Commands.Connect, "BName");
            string messageB = protocol.ToMessage(commandB);
            string responseB = messenger.ReadMessage(tcpClientB);
            Assert.AreEqual("123", responseB);
        }

        [Test]
        public void StopServer()
        {
            Assert.Pass();
        }
    }
}
