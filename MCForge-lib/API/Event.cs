using System.Collections.Generic;
using LibMinecraft.Classic.Server;
using MCForge.API.Player;
using LibMinecraft.Classic.Model;
using LibMinecraft.Classic.Model.Packets;


namespace MCForge.API
{
    public abstract class Event
    {
        public abstract EventType Type { get; }
    }

    public enum EventType
    {
        PLAYER_CONNECT,
        PLAYER_DISCONNECT,
        PLAYER_CHAT
    }

    public delegate void EventCall(Event e);
}
