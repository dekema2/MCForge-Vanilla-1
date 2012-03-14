using System;
using LibMinecraft.Classic.Server;

namespace MCForge.Client
{
    public class Player
    {
        RemoteClient Client;
        public string name
        {
            get { return client.UserName; }
            set { client.UserName = value; }
        }
        public RemoteClient client
        {
            get { return Client; }
        }
        public Player(RemoteClient client) { this.Client = client; }
    }
}
