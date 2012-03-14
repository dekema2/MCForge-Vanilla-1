using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibMinecraft.Classic.Model;
using LibMinecraft.Classic.Server;
using MCForge.Utilities;

namespace MCForge.Plugins.MCForgePlugins {

    class PluginCuboid : Plugin {
        public override string name {
            get { return "cuboid"; }
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

        public override void Load() {
            base.Load();
            ClassicServer.Worlds.ForEach(lvl => {
                if (lvl == null) return;
                lvl.OnBlockChanged += OnBlockChanged;
            });
        }
        public override void Unload() {
            base.Unload();

            //Todo: Save all of the cuboids
        }
        public override void OnCommand(Client.Player p, string cmd, string[] args) {
            if (cmd == "cuboid") {
                _getLocations = true;
            }
        }

        void OnBlockChanged(object sender, BlockSetEventArgs args) {
            if (!_getLocations)
                return;

            args.Block = new WoodenPlankBlock();

        }

        private bool _getLocations;
    }
}
