using System;
using System.Collections.Generic;

namespace Server.MTv2.Messaging
{
    public class Protocol
    {
        //fields
        private JsonParser jsonParser = new JsonParser();

        //methods
        public Command ToCommand(string message)
        {
#if DEBUG
            Console.WriteLine("In: {0}", message);
#endif
            return jsonParser.JsonToCommand(message);
        }

        public string ToMessage(Command command)
        {
            string message = jsonParser.CommandToJson(command);
#if DEBUG
            Console.WriteLine("Out: {0}", message);
#endif
            return message;
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
