﻿using System;
using System.Collections.Generic;
using LibMinecraft.Classic.Server;

namespace MCForge.API.Player
{
    public class PlayerChatEvent : Event
    {
        #region Delegates
        public delegate void OnCall(PlayerChatEvent args);
        #endregion

        #region Args
        public string Message { get; set; }
        public PlayerChatEvent(RemoteClient c, string Message) { this.Message = Message; this.client = c; }
        public PlayerChatEvent() { }
        #endregion

        #region Event Overrides
        public override void Call()
        {
            cache.ForEach(r =>
            {
                if (r.e.name == name)
                    ((OnCall)(r.method))(this);
            });
        }
        public override void Cancel(bool value)
        {
            base.Cancel(value);
        }
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
            get { return "playerchatevent"; }
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
            r.e = new PlayerChatEvent();
            r.method = method;
            r.priority = priority;
            r.Push();
        }
        #endregion
    }
}
