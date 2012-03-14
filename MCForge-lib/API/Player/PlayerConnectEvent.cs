using System;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public class PlayerConnectEvent : PlayerEvent
    {
        #region Delegates
        public delegate void OnCall(PlayerConnectEvent e);
        #endregion

        #region Args
        public PlayerConnectEvent(RemoteClient player) { this.player = player; }
        internal PlayerConnectEvent() { }
        #endregion

        #region Event Override
        internal override string name { get { return "playerconnect"; } }
        public override void Call()
        {
            cache.ForEach(r =>
            {
                if (r.e.name == name)
                    ((OnCall)(r.method))(this);
            });
        }
        
        public override bool IsCancelable
        {
            get { return false; }
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// Register this event
        /// The method name must be PlayerConnectEvent
        /// </summary>
        /// <param name="method">The method that will be called when the event is executed</param>
        /// <param name="priority">The priority of the call</param>
        public static void Register(OnCall method, Priority priority)
        {
            EventCacheItem r = new EventCacheItem();
            r.e = new PlayerConnectEvent();
            r.method = method;
            r.priority = priority;
            r.Push();
        }
        #endregion
    }
}
