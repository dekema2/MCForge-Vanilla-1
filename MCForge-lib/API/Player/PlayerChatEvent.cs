using System;
using LibMinecraft.Classic.Server;
namespace MCForge.API.Player
{
    public class PlayerChatEvent : Event
    {
        public RemoteClient player;
        public string Message { get; set; }
        public PlayerChatEvent(RemoteClient player, string message) { this.Message = message; this.player = player; }
        public PlayerChatEvent() { }
        public override bool IsCancelable
        {
            get { return true; }
        }
        public override bool IsCanceled
        {
            get
            {
                return base.IsCanceled;
            }
        }
        internal override string name
        {
            get { return "messageevent"; }
        }
        public override void Call(Event e)
        {
            base.Call(e);
        }
        public override void Cancel(bool value)
        {
            base.Cancel(value);
        }
        /// <summary>
        /// Register this event
        /// </summary>
        /// <param name="method">The method that will be called when the event is executed</param>
        /// <param name="priority">The priority of the call</param>
        public static void Register(OnCall method, Priority priority)
        {
            Cache r = new Cache();
            r.e = new PlayerChatEvent();
            r.method = method;
            r.priority = priority;
            r.Push();
            Event.Organize();
        }
    }
}
