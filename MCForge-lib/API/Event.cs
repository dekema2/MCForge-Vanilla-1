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
        public Event.OnCall method;
        public Priority priority;
        public Cache() { }
        internal void Push() { Event.cache.Add(this); }
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
        /// Execute the event
        /// </summary>
        /// <param name="e">The eventargs to pass</param>
        public virtual void Call(Event e)
        {
            cache.ForEach(r =>
            {
                if (r.e.name == e.name)
                    r.method(e);
            });
        }
        internal static void INIT()
        {
            MCForgeServer.ClassicServer.OnPlayerConnectionChanged += new System.EventHandler<PlayerConnectionEventArgs>(ClassicServer_OnPlayerConnectionChanged);
            MCForgeServer.ClassicServer.PrePacketEvent += new System.EventHandler<LibMinecraft.Classic.Server.PrePacketEventArgs>(ClassicServer_PrePacketEvent);
        }

        static void ClassicServer_PrePacketEvent(object sender, LibMinecraft.Classic.Server.PrePacketEventArgs e)
        {
            Event args = null;
            if (e.Packet.PacketID == PacketID.Message)
            {
                e.Packet.ReadPacket(e.Client);
                args = new PlayerChatEvent(e.Client, ((MessagePacket)e.Packet).Message);
            }
            args.Call(args);
            if (args.IsCanceled)
                MCForgeServer.ClassicServer.cancelprepacket = true;
        }

        static void ClassicServer_OnPlayerConnectionChanged(object sender, PlayerConnectionEventArgs e)
        {
            Event args = null;
            if (e.ConnectionState == LibMinecraft.Classic.Server.ConnectionState.Connected)
                args = new PlayerConnectEvent(e.Client);
            if (e.ConnectionState == LibMinecraft.Classic.Server.ConnectionState.Disconnected)
                args = new PlayerDisconnectEvent(e.Client);
            args.Call(args);
        }
        //UNTESTED CODE
        //I need to test this..but I dont have anything to test it on so I'll leave it here for now
        public static void Organize()
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
        }
    }
}
