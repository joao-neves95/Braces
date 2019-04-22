using System;
using System.Collections.Generic;
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

        private ExtensionSystem()
        {
            // SET THE CURRENT OPERATING SYSTEM.
            this.OperatingSystem = RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) ? OSPlatform.Windows :
                                   RuntimeInformation.IsOSPlatform( OSPlatform.Linux ) ? OSPlatform.Linux :
                                   RuntimeInformation.IsOSPlatform( OSPlatform.OSX ) ? OSPlatform.OSX :
                                   throw new NotSupportedException( "Unrecognized OSPlatform." );
        }

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

        public readonly OSPlatform OperatingSystem;

        #endregion

        #region PROPERTIES

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region PUBLIC METHODS

        #endregion
    }
}
