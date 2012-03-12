using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibMinecraft.Classic.Server;
using LibMinecraft.Classic.Model;
using MCForge.Interface;
using MCForge.Utilities;
using LibMinecraft.Classic.Model.Packets;
using MCForge.Utilities.Settings;
using MCForge.API.Player;
using MCForge.API;

namespace MCForge {
    public class MCForgeServer {
        #region Properties / Members
        /// <summary>
        /// Current Version of MCForge
        /// </summary>
        public static readonly Version Version = new Version(6, 0);
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
        public static void Start() {

            //TODO init all the things
            Logger.Init();
            ServerSettings.Init();
            FileUtils.Init();

            ClassicServer = new ClassicServer();
            McServer = new MinecraftClassicServer();



            //TODO: register all events
            Logger.OnRecieveLog += OnLog;



            Logger.Log("Starting MCForge");

            McServer.MaxPlayers = ServerSettings.GetSettingInt("MaxPlayers");
            McServer.MotD = ServerSettings.GetSetting("MOTD");
            McServer.Name = ServerSettings.GetSetting("ServerName");
            McServer.Port = ServerSettings.GetSettingInt("Port");
            McServer.Private = !ServerSettings.GetSettingBoolean("Public");

            Logger.Log(ClassicServer.Start(McServer));
            Console.WriteLine("Testing event system..");
            System.IO.File.WriteAllLines("url.txt", new[] { ClassicServer.ServerUrl });
            if (ServerSettings.GetSettingBoolean("UsingConsole")) {
                Logger.OnRecieveLog -= OnLog;
                MCForgeConsole.Start();
            }
            else {
                //new gui stuff
            }

        }
        /// <summary>
        /// Stops MCForge
        /// </summary>
        /// <remarks>Kicks all players, saves all of the worlds then exits</remarks>
        public static void Stop() {

            ClassicServer.EnqueueToAllClients(new DisconnectPlayerPacket { Reason = ServerSettings.GetSetting("ShutdownMessage") });

            foreach (var world in ClassicServer.Worlds)
                world.Save();
            if (ServerSettings.GetSettingBoolean("UsingConsole"))
                MCForgeConsole.Close();
        }

        private static void OnLog(object sender, LogEventArgs args) {
            Console.WriteLine(args.Message);
        }

        public static void GlobalMessage(string format) {
            //if
            ClassicServer.Clients.ForEach(client => client.PacketQueue.Enqueue(new MessagePacket(client.PlayerID, format)));
            Logger.Log(format);
        }
        public static void LevelMessage(string message, World mWorld) {

        }
        public static void KickPlayer(RemoteClient toKick) {

        }
    }
}
