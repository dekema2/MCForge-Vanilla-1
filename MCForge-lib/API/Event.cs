using System.Collections.Generic;
using LibMinecraft.Classic.Server;
using MCForge.API.Player;
using LibMinecraft.Classic.Server;
using LibMinecraft.Classic.Model;


namespace MCForge.API
{
    public abstract class Event
    {
        /// <summary>
        /// The delegate that is called when the event is executed
        /// </summary>
        /// <param name="e"></param>
        public delegate void OnCall(Event e);

        internal static sealed void INIT()
        {
            MCForgeServer.ClassicServer.OnPlayerConnectionChanged += new System.EventHandler<PlayerConnectionEventArgs>(ClassicServer_OnPlayerConnectionChanged);
        }

        static void ClassicServer_OnPlayerConnectionChanged(object sender, PlayerConnectionEventArgs e)
        {
            Event args = null;
            if (e.ConnectionState == LibMinecraft.Classic.Server.ConnectionState.Connected)
                args = new PlayerConnectEvent(e.Client);
            if (e.ConnectionState == LibMinecraft.Classic.Server.ConnectionState.Disconnected)
                args = new PlayerDisconnectEvent(e.Client);
            EventCache.Call(args);
        }
    }
}
