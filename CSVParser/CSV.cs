using System;

namespace Parser
{
    public class CSV
    {
        public static string GetNodeOf(int index, string CsvString)
        {
            string[] nodes = CsvString.Split(';');
            if (nodes.Length < index + 1)
            {
                throw new IndexOutOfRangeException("Index of node is out of range");
            }
            return nodes[index];
        }

        public static string GetName(string node)
        {
            string[] parts = node.Split(':');
            if (parts.Length != 2)
            {
                throw new IndexOutOfRangeException(string.Format("Node {0} is incorrect format", node));
            }
            return parts[0];
        }

        public static string GetValue(string node)
        {
            string[] parts = node.Split(':');
            if (parts.Length != 2)
            {
                throw new IndexOutOfRangeException(string.Format("Node {0} is incorrect format", node));
            }
            return parts[1];
        }

        public static string GetNodeNameOf(int index, string CsvString)
        {
            return GetName(GetNodeOf(index, CsvString));
        }

        public static string GetNodeValueOf(int index, string CsvString)
        {
            return GetValue(GetNodeOf(index, CsvString));
        }

        public static string GetValueByName(string value, string csvString)
        {
            string result = string.Empty;

            if (csvString.Contains(value))
            {
                string[] nodes = csvString.Split(';');
                foreach (string node in nodes)
                {
                    if (GetName(node) == value)
                    {
                        result = GetValue(node);
                        break;
                    }
                }
            }

            return result;
        }
    }
}
