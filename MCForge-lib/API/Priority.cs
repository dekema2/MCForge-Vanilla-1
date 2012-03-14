using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.API {
    /// <summary>
    /// The Priority of the event
    /// Low - Low will be called first and makes any changes first
    /// Normal - Normal will be called second, this is the default
    /// High - High will be called thrid and should only be used if you need the last say
    /// System_Level - This should only be used for moderatoring actions and not change
    /// </summary>
    public enum Priority : byte {
        System_Level,
        High,
        Normal,
        Low

    }
}
