using System;

namespace MCForge.Utilities {
    /// <summary>
    /// Logger Utility
    /// </summary>
    public class Logger {

        private static bool _initCalled;

        /// <summary>
        /// This event is called when Logger.Log() is invoked
        /// </summary>
        public static event EventHandler<LogEventArgs> OnRecieveLog;

        /// <summary>
        /// This event is called when Logger.LogError() is invoked
        /// </summary>
        public static event EventHandler<LogEventArgs> OnRecieveErrorLog;


        /// <summary>
        /// Initializes Logger object
        /// </summary>
        /// <remarks>Must be called before any of the methods are invoked</remarks>
        public static void Init() {
            if (_initCalled)
                throw new ArgumentException("\"Logger.Init()\" can only be called once");

            _initCalled = true;
        }

        /// <summary>
        /// Logs a message, to be grabbed by a log event handler
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <param name="logType">The log type</param>
        public static void Log(string message, LogType logType = LogType.Normal) {
            if (!_initCalled)
                throw new ArgumentException("You must call \"Logger.Init()\" before any logs can be created");
            if (OnRecieveLog != null)
                OnRecieveLog(null, new LogEventArgs(message, logType));
        }

        /// <summary>
        /// Logs an exception, to be grabbed by a log event handler
        /// </summary>
        /// <param name="e">Exception to be logged</param>
        public static void LogError(Exception e) {
            if (!_initCalled)
                throw new ArgumentException("You must call \"Logger.Init()\" before any logs can be created");
            if (OnRecieveErrorLog != null)
                OnRecieveErrorLog(null, new LogEventArgs(e.Message + "\n" + e.StackTrace, LogType.Error));

        }

    }

    /// <summary>
    /// Log type for the specified message
    /// </summary>
    public enum LogType {
        Normal,
        Error,
        Debug,
        Warning,
        Critical,

    }

    /// <summary>
    ///Log event where object holding the event
    ///would get a string (the message)
    /// </summary>
    public class LogEventArgs : EventArgs {
        public string Message { get; set; }
        public LogType LogType { get; set; }

        public LogEventArgs(string log, LogType logType) {
            Message = log;
            LogType = logType;
        }
    }
}
