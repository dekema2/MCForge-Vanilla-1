using System;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public abstract class PlayerEvent
    {
        protected RemoteClient player;
        public sealed RemoteClient Player { get { return player; } }
    }
}
