﻿using System;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public class PlayerConnectEvent : Event
    {
        RemoteClient player;
        public RemoteClient Player { get { return player; } }
        internal PlayerConnectEvent(RemoteClient player) { this.player = player; }
        internal PlayerConnectEvent() { }
        internal override string name { get { return "playerconnect"; } }
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
            System.Reflection.ParameterInfo p = method.Method.GetParameters()[0];
            if (p.GetType() == typeof(PlayerConnectEvent))
                r.e = new PlayerConnectEvent();
            else
                throw new Exception("Invalid method");
            r.method = method;
            r.priority = priority;
            r.Push();
            Event.Organize();
        }
    }
}
