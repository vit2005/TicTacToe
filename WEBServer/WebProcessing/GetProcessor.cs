using System.IO;
using System.Net.Sockets;
using System.Text;

namespace WEBServer
{
    public class GetProcessor
    {
        /// <summary>
        /// Sending HTML pages to client
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="tcpClient"></param>
        string fileName;
        TcpClient tcpClient;

        public void Initalize(string fileName, TcpClient tcpClient)
        {
            this.fileName = fileName;
            this.tcpClient = tcpClient;
        }

        public void ProcessWebRequest()
        {
            try
            {
                string contentType = ContectTypeFactory(Path.GetExtension(fileName));

                FileStream fs = GetFileStream(fileName);

                string headers = string.Format("HTTP/1.1 200 OK\nContent-Type: {0}\nContent-Length: {1}\n\n", contentType, fs.Length);
                byte[] headersBuffer = Encoding.ASCII.GetBytes(headers);

                tcpClient.GetStream().Write(headersBuffer, 0, headersBuffer.Length);

                while (fs.Position < fs.Length)
                {
                    byte[] buffer = new byte[1024];
                    int count = fs.Read(buffer, 0, buffer.Length);
                    tcpClient.GetStream().Write(buffer, 0, count);
                } 
                fs.Close();
            }
            catch { }
            tcpClient.Close();
        }

        string ContectTypeFactory(string extension)
        {
            string result = string.Empty;
            switch (extension.ToLower())
            {
                case ".htm":
                case ".html":
                case "":
                    result = "text/html";
                    break;
                case ".css":
                    result = "text/css";
                    break;
                case ".js":
                    result = "text/javascript";
                    break;
                case ".jpg":
                    result = "image/jpeg";
                    break;
                case ".jpeg":
                case ".png":
                case ".gif":
                    result = "image/" + extension.Substring(1);
                    break;
                default:
                    result = extension.Length > 1 ? string.Format("application/{0}", extension) : "application/unknown";
                    break;
            }
            return result;
        }

        FileStream GetFileStream(string name)
        {
            FileStream fs;
            if (File.Exists(string.Format("..\\..\\HTML\\{0}", name)))
            {
                fs = new FileStream(@"..\..\HTML\" + name, FileMode.Open);
            }
            else
            {
                fs = new FileStream(@"..\..\HTML\NotFound.htm", FileMode.Open);
            }
            return fs;
        }
    }
}
