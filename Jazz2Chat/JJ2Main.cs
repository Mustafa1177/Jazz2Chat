using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using JJ2ClientLib.JJ2;

namespace Jazz2Chat
{
    public static class JJ2Main
    {

        private static List<JJ2Client> clients = new List<JJ2Client>();

        public static List<JJ2Client> Clients { get => clients; set => clients = value; }
        public static JJ2Client Client { get; set; } //default client


        static JJ2Main()
        {
            Init();
        }

        public static void Init()
        {
            AddClient();
            Client = clients[0];
        }

        public static void AddClient()
        {
            JJ2Client client = new JJ2Client();
            client.UserData = client;
            client.Connected_Event += Client_Connected_Event;
            clients.Add(client);
        }

        private static void Client_Connected_Event(string serverIPAddrees, string serverAddress, ushort serverPort, object user)
        {

        }
    }
}
