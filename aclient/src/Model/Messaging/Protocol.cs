using System.Collections.Generic;

namespace DesktopClient.Model.Messaging
{
    public class Protocol
    {
        //fields
        private JsonParser jsonParser = new JsonParser();

        //methods
        public Command ToCommand(string message)
        {
            return jsonParser.JsonToCommand(message);
        }

        public string ToMessage(Command command)
        {
            return jsonParser.CommandToJson(command);
        }

        public string PersonListToMessage(List<Person> personList)
        {
            return jsonParser.PeopleToJson(personList);
        }

        public List<Person> MessageToPersonList(string message)
        {
            return jsonParser.JsonToPeople(message);
        }
    }
}
