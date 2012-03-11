using System;
using System.Collections.Generic;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace MCForge.Plugins
{
    public class Manager
    {
        internal static List<Plugin> all = new List<Plugin>();
        public static void Load(string filepath)
        {
            try
            {
                object instance = null;
                Assembly lib = null;
                using (FileStream fs = File.Open(filepath, FileMode.Open))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        byte[] buffer = new byte[1024];
                        int read = 0;
                        while ((read = fs.Read(buffer, 0, 1024)) > 0)
                            ms.Write(buffer, 0, read);
                        lib = Assembly.Load(ms.ToArray());
                        ms.Close();
                        ms.Dispose();
                    }
                    fs.Close();
                    fs.Dispose();
                }
                try
                {
                    foreach (Type t in lib.GetTypes())
                    {
                        if (t.BaseType == typeof(Plugin))
                        {
                            instance = Activator.CreateInstance(t);
                            break;
                        }
                    }
                }
                catch { }
                if (instance == null)
                {
                    //Log here
                    return;
                }
                Plugin p = (Plugin)instance;
                p.Load();
            }
            catch { }
        }
        public static void Load()
        {
            if (Directory.Exists("plugins"))
            {
                foreach (string file in Directory.GetFiles("plugins", "*.dll"))
                    Load(file);
            }
            else
                Directory.CreateDirectory("plugins");
        }
        public static void Unload(Plugin p)
        {
            p.Unload();
        }
        public static void Unload()
        {
            all.ForEach(p =>
                {
                    Unload(p);
                });
        }
    }
}
