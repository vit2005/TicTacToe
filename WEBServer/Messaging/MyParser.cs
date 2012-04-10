using System.Text.RegularExpressions;

namespace WEBServer.Messaging
{
    public class MyParser
    {
        public static string CsvGetValue(string request, string name)
        {
            Match match = Regex.Match(request, string.Format("{0}:\\w+", name));
            return  match.Value.Substring(match.Value.IndexOf(':') + 1);
        }

        public static string CsvGetIP(string request)
        {
            Match match = Regex.Match(request, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            return match.Value;
        }

        public static string GetServerCommand(string request)
        {
            string result = request.Split('!')[1];
            result = result.Replace("%7B", "{");
            result = result.Replace("%7D", "}");
            result = result.Replace(@"'", @"""");
            result = result.Replace("%27", @"""");
            result = result.Replace("%22", @"""");
            return result;
        }

        public static bool CanGetServerCommand(string request)
        {
            return request.Split('!').Length > 1;
        }
    }
}
