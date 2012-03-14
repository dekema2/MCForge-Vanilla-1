using System;
using System.Net;
using System.Text.RegularExpressions;
using LibMinecraft.Classic.Server;
using MCForge.Utilities;
using MCForge.Utilities.Settings;

namespace MCForge.Interface {
    class MCForgeConsole {
        /// <summary>
        /// Starts an instance of the MCForge Console
        /// </summary>
        /// <remarks>If using mono, this should be in use</remarks>
        public static void Start() {
            Logger.OnRecieveLog += OnLog;
            MCForgeServer.ClassicServer.OnPlayerConnectionChanged += OnPlayerConnectionChanged;

            RunInput();
        }



        private static void ProcessCommand(string all) {

        }

        private static void RunInput() {
            while (true) {
                var lineRead = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(lineRead))
                    continue;

                if (lineRead[0] == '/')
                    ProcessCommand(lineRead.Substring(1));
                else {
                    if (lineRead == "/stop") {
                        MCForgeServer.Stop();
                        return;
                    }

                    MCForgeServer.GlobalMessage(string.Format("&f[{0}{1}&f] {2}", ServerSettings.GetSettingArray("ConsoleName")[0], ServerSettings.GetSettingArray("ConsoleName")[1], lineRead));
                }
            }
        }

        #region EventHandler

        private static void OnPlayerConnectionChanged(object sender, PlayerConnectionEventArgs e) {
            Logger.Log(e.ConnectionState == ConnectionState.Connected
                           ? string.Format("{0} ({1}) connected to the server.", e.Client.UserName, e.Client.IpAddress)
                           : string.Format("{0} disconnected from the server.", e.Client.UserName));
        }
        private static void OnLog(object sender, LogEventArgs e) {
            switch (e.LogType) {
                case LogType.Error:
                    WriteLine("&4[Error]&f " + e.Message);
                    break;
                case LogType.Debug:
                    WriteLine("&6[DEBUG]&f " + e.Message);
                    break;
                case LogType.Critical:
                    WriteLine("&3[Critical]&f " + e.Message + "!!!");
                    break;
                case LogType.Warning:
                    WriteLine("&e[Warning]&f " + e.Message);
                    break;
                default:
                    WriteLine(DateTime.Now.ToString("(hh:mm:ss) ") + e.Message);
                    break;
            }
        }

        private static void WriteLine(string message) {
            if (!Regex.IsMatch(message, "%[a-f0-9]") && !Regex.IsMatch(message, "&[a-f0-9]")) {
                Console.WriteLine(message);
                return;
            }
            message = message.Replace('%', '&');

        }

        #endregion


        internal static void Close() {
            throw new NotImplementedException();
        }
    }

}
