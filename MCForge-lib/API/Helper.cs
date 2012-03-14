using System;
using LibMinecraft.Classic.Server;
using LibMinecraft.Classic.Model.Packets;
using LibMinecraft.Classic.Model;
using MCForge.API.Player;

namespace MCForge.API
{
    public static class EventHelper
    {
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
            args.Call();
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
            args.Call();
        }
    }
}
