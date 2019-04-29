using System;
using System.Collections.Generic;
using System.Text;

namespace Braces.Core.ConfigurationSystem
{
    public sealed class ConfigurationSystem
    {
        #region SINGLETON CONTRUCTOR

        static ConfigurationSystem() { }

        private ConfigurationSystem() { }

        private static readonly ConfigurationSystem _instance = new ConfigurationSystem();

        public static ConfigurationSystem Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion
    }
}
