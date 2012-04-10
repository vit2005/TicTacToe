using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Input;
using DesktopClient.Model;
using DesktopClient.Model.PreGaming;

namespace DesktopClient.ViewModel
{
    public class ConnectionViewModel
    {
        //fields
        private Person person = new Person("Enter your name here");
        private string ipAdderss = "127.0.0.1";
        private int port = 10060;
        private ClientManager manager = new ClientManager(new MainWindow());

        //properties
        public string Name
        {
            get
            {
                return person.Name;
            }
            set
            {
                person.Name = value;
            }
        }

        public string IPAddressProperty
        {
            get
            {
                return ipAdderss.ToString();
            }
            set
            {
                ipAdderss = value;
            }
        }

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        //commands
        private bool CanConnect()
        {
            return Regex.IsMatch(ipAdderss, @"^\d{1,3}[.]\d{1,3}[.]\d{1,3}[.]\d{1,3}$") && port >= 0 && port <= 65535; 
        }

        private void ConnectExecute()
        {
            manager.Login(new IPEndPoint(IPAddress.Parse(ipAdderss), port), person.Name);
        }

        public ICommand Connect
        {
            get
            {
                return new RelayCommand(ConnectExecute, CanConnect);
            }
        }
    }
}
