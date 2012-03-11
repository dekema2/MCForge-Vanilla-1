using System;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public class PlayerDisconnectEvent : Event
    {
        RemoteClient player;
        public RemoteClient Player { get { return player; } }
        internal PlayerDisconnectEvent(RemoteClient player) { this.player = player; }
        internal PlayerDisconnectEvent() { }
        internal override string name { get { return "playerdisconnect"; } }
        public void Call()
        {
            base.Call(this);
        }
        public override bool IsCancelable
        {
            get { return false; }
        }
        /// <summary>
        /// Register this event
        /// </summary>
        /// <param name="method">The method that will be called when the event is executed</param>
        /// <param name="priority">The priority of the call</param>
        public static void Register(OnCall method, Priority priority)
        {
            Cache r = new Cache();
            r.e = new PlayerDisconnectEvent();
            r.method = method;
            r.priority = priority;
            r.Push();
            Event.Organize();
        }
    }
}
