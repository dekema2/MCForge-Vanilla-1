using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCForge.Client;
using MCForge.Utilities;

namespace MCForge.Plugins
{
    public abstract class Plugin
    {
        bool _loaded = false;
        /// <summary>
        /// Load the plugin (Will throw an exception if the plugin is already loaded
        /// </summary>
        public virtual void Load()
        {
            if (loaded)
                throw new Exception("The plugin " + name + " is already loaded!");
            if (!Manager.all.Contains(this))
                Manager.all.Add(this);
        }
        /// <summary>
        /// Unload the plugin (Will thow an exception if the plugin is not loaded)
        /// </summary>
        public virtual void Unload()
        {
            if (!loaded)
                throw new Exception("The plugin " + name + " is not loaded!");
            if (Manager.all.Contains(this))
                Manager.all.Remove(this);
        }
        /// <summary>
        /// If true, the plugin is loaded. If false, the plugin is not loaded
        /// </summary>
        public virtual bool loaded
        {
            get
            {
                return _loaded;
            }
        }
        /// <summary>
        /// The name of the plugin
        /// </summary>
        public abstract string name { get; }
        /// <summary>
        /// The version of the plugin
        /// </summary>
        public abstract Version version { get; }
        /// <summary>
        /// The support website for the plugin
        /// </summary>
        public virtual string website { get { return ""; } }
        /// <summary>
        /// The creator of the plugin
        /// </summary>
        public virtual string creator { get { return ""; } }
        /// <summary>
        /// This method is called when a player on the server does a command
        /// </summary>
        /// <param name="p">The player who did the command</param>
        /// <param name="cmd">The command name</param>
        /// <param name="args">The args <example>(ex: /test 1 2 3 - args[0] = 1, args[1] = 2, args[2] = 3)</example></param>
        public virtual void OnCommand(Player p, string cmd, string[] args)
        {
            Logger.Log(p.name + " used /" + cmd);
        }
    }
}
