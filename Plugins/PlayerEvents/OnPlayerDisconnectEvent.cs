﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge
{
    /// <summary>
    /// This event is called whenever a player connects to the server
    /// </summary>
    public class OnPlayerDisconnectEvent
    {
        internal static List<OnPlayerDisconnectEvent> events = new List<OnPlayerDisconnectEvent>();
        Plugin plugin;
        Player.OnPlayerDisconnect method;
        Priority priority;
        internal OnPlayerDisconnectEvent(Player.OnPlayerDisconnect method, Priority priority, Plugin plugin) { this.plugin = plugin; this.priority = priority; this.method = method; }
        public static void Call(Player p, string reason)
        {
            events.ForEach(delegate(OnPlayerDisconnectEvent p1)
            {
                try
                {
                    p1.method(p,reason);
                }
                catch (Exception e) { Server.s.Log("The plugin " + p1.plugin.name + " errored when calling the PlayerDisconnect Event!"); Server.ErrorLog(e); }
            });
        }
        static void Organize()
        {
            List<OnPlayerDisconnectEvent> temp = new List<OnPlayerDisconnectEvent>();
            List<OnPlayerDisconnectEvent> temp2 = events;
            OnPlayerDisconnectEvent temp3 = null;
            int i = 0;
            int ii = temp2.Count;
            while (i < ii)
            {
                foreach (OnPlayerDisconnectEvent p in temp2)
                {
                    if (temp3 == null)
                        temp3 = p;
                    else if (temp3.priority < p.priority)
                        temp3 = p;
                }
                temp.Add(temp3);
                temp2.Remove(temp3);
                temp3 = null;
                i++;
            }
            events = temp;
        }
        public static OnPlayerDisconnectEvent Find(Plugin plugin)
        {
            foreach (OnPlayerDisconnectEvent p in events.ToArray())
            {
                if (p.plugin == plugin)
                    return p;
            }
            return null;
        }
        /// <summary>
        /// Register this event
        /// This will call the method "method" everytime this event is triggered
        /// </summary>
        /// <param name="method">This is the delegate that will get called when this event occurs</param>
        /// <param name="priority">The priority (imporantce) of this call</param>
        /// <param name="plugin">The plugin object that is registering the event</param>
        public static void Register(Player.OnPlayerDisconnect method, Priority priority, Plugin plugin, bool bypass = false)
        {
            if (Find(plugin) != null)
                if (!bypass)
                throw new Exception("The user tried to register 2 of the same event!");
            events.Add(new OnPlayerDisconnectEvent(method, priority, plugin));
            Organize();
        }
        /// <summary>
        /// UnRegister this event
        /// </summary>
        /// <param name="plugin">The plugin object that has this event registered</param>
        public static void UnRegister(Plugin plugin)
        {
            if (Find(plugin) == null)
                throw new Exception("This plugin doesnt have this event registered!");
            else
                events.Remove(Find(plugin));
        }
    }
}
