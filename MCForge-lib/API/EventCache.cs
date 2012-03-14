﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.API
{
    internal sealed class EventCacheItem
    {
        public Event e;
        public Event.OnCall method;
        public Priority priority;
        public EventCacheItem() { }
        public void Push() { EventCache.cache.Add(this); }
    }

    internal sealed class EventCache
    {
        internal static List<EventCacheItem> cache = new List<EventCacheItem>();

        public static sealed void Call(Event e)
        {
            cache.ForEach(r =>
            {
                if (r.e.GetType() == e.GetType())
                    r.method(e);
            });
        }

        //UNTESTED CODE
        //I need to test this..but I dont have anything to test it on so I'll leave it here for now
        //TODO: Do something with this...
        /*public static void Organize()
        {
            //Event1 - Low
            //Event1 - Normal
            //Event1 - High
            //Event2 - Low
            //Event2 - Normal
            //Event2 - High
            List<Cache> temp = cache; //Backup for cache
            List<Cache> temp2 = new List<Cache>(); //Used for getting all the same events in 1 list
            List<Cache> temp3 = new List<Cache>(); //Used for setting the events from lowest to highest
            List<Cache> final = new List<Cache>(); //The final product
            temp.ForEach(r =>
            {
                Event current = r.e; //The current event type were on
                temp.ForEach(t => //Ok now we need a seperate list of all those types
                {
                    if (current.GetType() == t.e.GetType())
                    {
                        temp2.Add(t); //Add them to our seperate list
                        temp.Remove(t); //And remove this type from the backup list
                    }
                });
                //Now to organize them
                int i = 0;
                int ii = temp2.Count;
                Cache current1 = null;
                while (i < ii)
                {
                    temp2.ForEach(t =>
                    {
                        if (current1 == null)
                            current1 = t;
                        else if (current1.priority < t.priority)
                            current1 = t;
                    });
                    temp3.Add(current1);
                    temp2.Remove(current1);
                    current1 = null;
                    i++;
                }
                //Ok there all nice and organized so now to add them to our final product
                temp3.ForEach(t =>
                {
                    final.Add(t);
                });
                //Lets reset these 2
                temp2.Clear();
                temp3.Clear();
            });
            cache = final;
        }*/
    }
}
