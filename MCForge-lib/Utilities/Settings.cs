using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCForge.Utilities
{
    /// <summary>
    /// Settings Utility
    /// </summary>
    public class Settings
    {
        private static bool _initCalled;
        private static Dictionary<string, string> _dictionary; 
        /// <summary>
        /// Starts the Settings Object
        /// </summary>
        /// <remarks>Must be called before any methods are invoked</remarks>
        public static void Init()
        {
            if (_initCalled)
                throw new ArgumentException("\"Logger.Init()\" can only be called once");

            _initCalled = true;
            _dictionary = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets a setting
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The setting value specified by the key</returns>
        public static string GetSetting(string key)
        {
            return "";
        }

        /// <summary>
        /// Gets a setting
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The setting value specified by the key</returns>
        public static int GetSettingInt(string key)
        {
            return 0;
        }

        /// <summary>
        /// Gets a setting
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The setting value specified by the key</returns>
        public static bool GetSettingBoolean(string key)
        {
            return GetSetting(key) == "true";
        }

        /// <summary>
        /// Set the setting
        /// </summary>
        /// <param name="key">key to save value to</param>
        /// <param name="discription"> </param>
        /// <param name="values">for each string in values, it will be seperated by a comma ','</param>
        /// <remarks>If the setting does not exist, it will create a new one</remarks>
        public static void SetSetting(string key, string discription = null, params string[] values)
        {
            
        }

        /// <summary>
        /// Set the setting
        /// </summary>
        /// <param name="key">key to save value to</param>
        /// <param name="value">value to set setting to</param>
        /// <remarks>If the setting does not exist, it will create a new one</remarks>
        public static void SetSetting(string key, int value)
        {

        }

        /// <summary>
        /// Set the setting
        /// </summary>
        /// <param name="key">key to save value to</param>
        /// <param name="value">value to set setting to</param>
        /// <remarks>If the setting does not exist, it will create a new one</remarks>
        public static void SetSetting(string key, bool value)
        {

        }
    }
}
