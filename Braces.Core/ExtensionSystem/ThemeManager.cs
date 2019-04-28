using System;
using System.Collections.Generic;
using System.Text;

namespace Braces.Core.ExtensionSystem
{
    public sealed class ThemeManager
    {
        #region SINGLETON CONTRUCTOR

        static ThemeManager() { }

        private ThemeManager() { }

        private static readonly ThemeManager _instance = new ThemeManager();

        public static ThemeManager Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion
    }
}
