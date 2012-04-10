
namespace ActiveXClient.Model
{
    public class Person
    {
        //fields
        private string name;

        //constructors
        public Person()
        {
            this.name = string.Empty;
        }

        public Person(string name)
        {
            this.name = name;
        }

        //properties
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
