using System;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public class PlayerConnectEvent : PlayerEvent
    {
        public override EventType Type { get { return EventType.PLAYER_CONNECT; } }

        public PlayerConnectEvent(RemoteClient player)
        {
            this.player = player;
        }
    }
}
