using System;
using MCForge.Utilities;

namespace MCForge.Events
{
    /// <summary>
    ///Log event where object holding the event
    ///would get a string (the message)
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        public string Message { get; set; }
        public LogType LogType { get; set; }

        public LogEventArgs(string log, LogType logType)
        {
            Message = log;
            LogType = logType;
        }
    }
}
