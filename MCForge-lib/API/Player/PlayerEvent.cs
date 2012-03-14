using System;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public abstract class PlayerEvent : Event
    {
        protected RemoteClient player;
        public RemoteClient Player { get { return player; } }
    }
}
