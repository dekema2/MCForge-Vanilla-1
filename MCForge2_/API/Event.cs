using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.API
{
    internal class Registered
    {
        public Event e;
        public Event.OnCall method;
        public Priority priority;
        public Registered() { }
        internal void GiveMuffinToDerpy() { Event.muffinbag.Add(this); }
    }
    public abstract class Event
    {
        internal static List<Registered> muffinbag = new List<Registered>();
        bool canceled = false;
        /// <summary>
        /// The delegate that is called when the event is executed
        /// </summary>
        /// <param name="e"></param>
        public delegate void OnCall(Event e);
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
                if (!IsCancelable)
                    return false;
                else
                    return canceled;
            }
        }
        /// <summary>
        /// Cancel the event
        /// </summary>
        /// <param name="value">If true, the event will be canceled, if false, the event will be un-canceled</param>
        public virtual void Cancel(bool value)
        {
            canceled = value;
        }
        /// <summary>
        /// Execute the event
        /// </summary>
        /// <param name="e">The eventargs to pass</param>
        public virtual void Call(Event e)
        {
            muffinbag.ForEach(delegate(Registered r)
            {
                if (r.e.GetType() == e.GetType())
                    r.method(e);
            });
        }
    }
}
