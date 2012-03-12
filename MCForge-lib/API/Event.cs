using System.Collections.Generic;
using LibMinecraft.Classic.Server;
using MCForge.API.Player;
using LibMinecraft.Classic.Model;
using LibMinecraft.Classic.Model.Packets;


namespace MCForge.API
{
    internal class Cache
    {
        public Event e;
        public object method;
        public Priority priority;
        public Cache() { }
        internal void Push()
        {
            int i = Event.cache.FindIndex(c => { return c.e.GetType() == this.e.GetType(); });//finds first element with same type
            if (i >= 0)
            {
                i = Event.cache.FindIndex(i, Event.cache.Count - i, c =>
                {
                    return (c.priority >= this.priority && c.e.GetType() == this.e.GetType()) //if c has less priority and same event type (lower value == higher priority)
                        || c.e.GetType() != this.e.GetType();                                 //or c has another type
                });
                if (i >= 0) Event.cache.Insert(i, this);                                          //this takes place in front of c
            }
            if (i < 0) Event.cache.Add(this);
        }
    }
    public abstract class Event
    {
        /// <summary>
        /// The name of the event
        /// </summary>
        internal abstract string name { get; }
        internal static List<Cache> cache = new List<Cache>();
        bool _canceled;
        /// <summary>
        /// Weather or not the event can be canceled
        /// </summary>
        public abstract bool IsCancelable { get; }
        /// <summary>
        /// Weather or not the event is canceled
        /// </summary>
        public virtual bool IsCanceled
        {
            get
            {
                return IsCancelable && _canceled;
            }
        }
        /// <summary>
        /// Cancel the event
        /// </summary>
        /// <param name="value">If true, the event will be canceled, if false, the event will be un-canceled</param>
        public virtual void Cancel(bool value)
        {
            _canceled = value;
        }
        /// <summary>
        /// Executes the registered methods of an event
        /// </summary>
        public abstract void Call();
    }
}
