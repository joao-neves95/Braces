using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Braces.Core.ExtensionSystem
{
    /// <summary>
    /// 
    /// (Singleton)
    /// 
    /// </summary>
    public sealed class ExtensionSystem
    {
        #region SINGLETON CONTRUCTOR

        static ExtensionSystem() { }

        private ExtensionSystem() { }

        private static readonly ExtensionSystem _instance = new ExtensionSystem();

        public static ExtensionSystem Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region FIELDS

        public readonly PluginManager PluginManager = PluginManager.Instance;
        public readonly ThemeManager ThemeManager = ThemeManager.Instance;

        public readonly OSPlatform OperatingSystem = FileStorage.GetCurrentOperatingSystem();

        #endregion

        #region PROPERTIES

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region PUBLIC METHODS

        #endregion
    }
}
