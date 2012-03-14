﻿using System;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public class PlayerDisconnectEvent : Event
    {
        #region Delegates
        public delegate void OnCall(PlayerDisconnectEvent e);
        #endregion

        #region Args
        RemoteClient player;
        public RemoteClient Player { get { return player; } }
        public PlayerDisconnectEvent(RemoteClient player) { this.player = player; }
        internal PlayerDisconnectEvent() { }
        #endregion

        #region Event Override
        internal override string name { get { return "playerdisconnect"; } }
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
            Cache r = new Cache();
            r.e = new PlayerConnectEvent();
            r.method = method;
            r.priority = priority;
            r.Push();
        }
        #endregion
    }
}
