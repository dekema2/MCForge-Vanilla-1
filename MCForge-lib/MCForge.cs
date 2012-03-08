using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibMinecraft.Classic.Server;
using LibMinecraft.Classic.Model;
using MCForge.Utilities;
using LibMinecraft.Classic.Model.Packets;
using MCForge.Events;

namespace MCForge
{
    public class MCForgeServer
    {
        #region Properties / Members
        /// <summary>
        /// Current Version of MCForge
        /// </summary>
        public static readonly string Version = "6.0";
        /// <summary>
        /// <see cref="LibMinecraft.Classic.Server.ClassicServer"/> Object.
        /// </summary>
        public static ClassicServer ClassicServer { get; set; }
        /// <summary>
        /// <see cref="LibMinecraft.Classic.Model.MinecraftClassicServer"/> Object.
        /// </summary>
        public static MinecraftClassicServer McServer { get; set; }
        #endregion

        /// <summary>
        /// Starts MCForge
        /// </summary>
        public static void Start()
        {
            //TODO init all the things
            Logger.Init();
            Settings.Init();
            ClassicServer = new ClassicServer();
            McServer = new MinecraftClassicServer();

            

            //TODO: register all events
            Logger.OnRecieveLog += OnLog;
            
            

            Logger.Log("Starting MCForge");
            
            McServer.MaxPlayers = Settings.GetSettingInt("MaxPlayers");
            McServer.MotD = Settings.GetSetting("MOTD");
            McServer.Name = Settings.GetSetting("ServerName");
            McServer.Port = Settings.GetSettingInt("Port");
            McServer.Private = Settings.GetSetting("Public") == "true";

            ClassicServer.Start(McServer);

            Console.ReadLine();

        }

        /// <summary>
        /// Stops MCForge
        /// </summary>
        /// <remarks>Kicks all players, saves all of the worlds then exits</remarks>
        public static void Stop(){
            foreach (var p in ClassicServer.Clients.Select(client => new DisconnectPlayerPacket{Reason = "Server Restarting"})){
                ClassicServer.EnqueueToAllClients(p);
            }

            foreach (var world in ClassicServer.Worlds)
                world.Save();
            
           // if (ClassicServer != null)
           //     ClassicServer.Stop();
        }

        private static void OnLog(object sender, LogEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
