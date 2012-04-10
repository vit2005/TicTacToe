using System.Collections.Generic;
using Server.MTv2.MyEventArgs;
using Server.MTv2.Messaging;

namespace Server.MTv2.ObserverNotifier
{
    public class Notifier
    {
        private Protocol protocol;

        public Notifier(Protocol protocol)
        {
            this.protocol = protocol;
        }

        public void Notify(object sender, ClientListEventArgs e)
        {
            List<Person> personList = new List<Person>();

            foreach (KeyValuePair<string, ClientThread> keyValuePair in e.ClientList)
            {
                personList.Add(new Person(keyValuePair.Value.Client.Name, keyValuePair.Value.Client.GameType));
            }

            string peopleJSON = protocol.PersonListToMessage(personList);

            string message = protocol.ToMessage(new Command(Command.Commands.GetPlayers, peopleJSON));

            foreach (KeyValuePair<string, ClientThread> keyValuePair in e.ClientList)
            {
                keyValuePair.Value.Client.SendMessage(message);
            }
        }
    }
}