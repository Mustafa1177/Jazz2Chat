using Jazz2Chat.JJ2.DataClasses;
using System;
using System.Net.Sockets;
using System.Text;

namespace Jazz2Chat.JJ2
{
    public static class ListClient
    {
        public static string DownloadASCIIList(string hostname = "list.jazzjackrabbit.com", int port = 10057)
        {
            TcpClient client = new TcpClient();
            client.Connect(hostname, port);
            if (client.Connected)
            {
                byte[] buffer = new byte[1024 * 4];
                var dataLength = client.Client.Receive(buffer);
                if (dataLength > 0)
                {
                    string recv = Encoding.ASCII.GetString(buffer, 0, dataLength);
                    return recv;
                }
            }
            return string.Empty;
        }

        public static List<GameServer> ParseASCIIList(string value)
        {
            string[] serverLines = value.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<GameServer> res = new List<GameServer>(serverLines.Length - 1);
            foreach (var line in serverLines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    GameServer server = GameServer.Parse(line);
                    if (server != null)
                        res.Add(server);
                }
            }
            return res;
            return null;
        }
    }
}
