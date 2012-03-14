using System;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public class PlayerDisconnectEvent : PlayerEvent
    {
        public override EventType Type { get { return EventType.PLAYER_DISCONNECT; } }

        public PlayerDisconnectEvent(RemoteClient player)
        {
            this.player = player;
        }
    }
}
