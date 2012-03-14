using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.Plugins.MCForgePlugins {
    class PluginSetting : Plugin{
        public override string name {
            get { return "settings"; }
        }

        public override Version version {
            get { return new Version(1, 0); }
        }

        public override string website {
            get { return "http://www.mcforge.net"; }
        }

        public override string creator {
            get { return "MCForge Dev Team"; }
        }

        public override void OnCommand(Client.Player p, string cmd, string[] args) {
            if (cmd == "settings") {
                
            }
        }
    }
}
