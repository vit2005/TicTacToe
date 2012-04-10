using System.Collections.Generic;
using System.Net.Sockets;

namespace WEBServer.GameProcessing
{
    public class ClientList
    {
        //fields
        Dictionary<string, Client> clientList = new Dictionary<string, Client>();

        //methods
        public bool Contain(string name)
        {
            return clientList.ContainsKey(name);
        }

        public void UpdateHttpSocket(string name, TcpClient httpClient)
        {
            clientList[name].HttpSocket = httpClient;
        }

        public void Add(Client client)
        {
            clientList.Add(client.Name, client);
        }

        //indexer
        public Client this[string name]
        {
            get
            {
                return clientList[name];
            }
            set
            {
                clientList.Add(name, value);
            }
        }
    }
}
