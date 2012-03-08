using System;

namespace MCForge.API
{
    public class PlayerConnectEvent : Event
    {
        //TODO
        //Add more events and complete this one
        //Once all the server stuff is finished
        public void Call()
        {
            base.Call(this);
        }
        public override bool IsCancelable
        {
            get { return false; }
        }
        public override bool IsCanceled
        {
            get
            {
                return base.IsCanceled;
            }
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
            Registered r = new Registered();
            System.Reflection.ParameterInfo p = method.Method.GetParameters()[0];
            if (p.GetType() == typeof(PlayerConnectEvent))
                r.e = new PlayerConnectEvent();
            else
                throw new Exception("Invalid method");
            r.method = method;
            r.priority = priority;
            r.GiveMuffinToDerpy();
        }
    }
}
