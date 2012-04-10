using System.Collections.Generic;
using Newtonsoft.Json;

namespace DesktopClient.Model.Messaging
{
    public class JsonParser
    {
        //methods
        public string CommandToJson(Command command)
        {
            return JsonConvert.SerializeObject(command);
        }

        public Command JsonToCommand(string data)
        {
            int size = data.LastIndexOf('}') + 1;
            data = data.Substring(0, size);
            return JsonConvert.DeserializeObject(data, typeof(Command)) as Command;
        }

        public string PeopleToJson(List<Person> personList)
        {
            Person[] people = personList.ToArray();
            return JsonConvert.SerializeObject(people);
        }

        public List<Person> JsonToPeople(string data)
        {
            Person[] people = JsonConvert.DeserializeObject(data, typeof(Person[])) as Person[];
            return new List<Person>(people);
        }
    }
}
