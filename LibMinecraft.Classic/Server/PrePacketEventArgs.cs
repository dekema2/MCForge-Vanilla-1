using System;
using LibMinecraft.Classic.Model.Packets;

namespace LibMinecraft.Classic.Server
{
    public class PrePacketEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the packet type
        /// </summary>
        /// <value>The state of the connection.</value>
        /// <remarks></remarks>
        public Packet Packet { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        /// <remarks></remarks>
        public RemoteClient Client { get; set; }

        public PrePacketEventArgs(Packet packet, RemoteClient client) { this.Packet = packet; this.Client = client; }
    }
}
