using System.Collections.Generic;
using WEBServer.CustomEventArgs;

namespace WEBServer.GameProcessing
{
    public class ServerThreadManager
    {
        //TO DO: i don't need this dictionary
        Dictionary<string, ServerThread> threadList = new Dictionary<string, ServerThread>();

        //methods
        public void NewConnection(object sender, ClientEventArgs e)
        {
            ServerThread serverThread = new ServerThread(e.Client);
            serverThread.Start();
            threadList.Add(e.Client.Name, serverThread);
        }
    }
}
