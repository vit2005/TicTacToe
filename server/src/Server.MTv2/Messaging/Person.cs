
namespace Server.MTv2.Messaging
{
    public class Person
    {
        //fields
        private string name;
        private string gameType;

        //constructors
        public Person(string name, string gameType)
        {
            this.name = name;
            this.gameType = gameType;
        }

        //properties
        public string Name
        {
            get
            {
                return name;
            }
        }

        public string GameType
        {
            get
            {
                return gameType;
            }
        }
    }
}
