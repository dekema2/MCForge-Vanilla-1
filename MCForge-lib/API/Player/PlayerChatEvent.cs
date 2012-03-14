using System;
using System.Collections.Generic;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public class PlayerChatEvent : PlayerEvent, Cancellable
    {
        public override EventType Type { get { return EventType.PLAYER_CHAT; } }
        private bool _cancelled;
        public string Message { get; set; }

        public PlayerChatEvent(RemoteClient player, string Message)
        {
            this.player = player;
            this.Message = Message;
        }

        /// <summary>
        /// Gets/sets weather or not this event is canceled.
        /// </summary>
        public bool IsCancelled { get { return _cancelled; } set { _cancelled = value; } }
    }
}
