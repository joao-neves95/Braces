using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Braces.Core.ExtensionSystem
{
    public static class PluginExtensions
    {
        public static MethodInfo GetMethod( this IPlugin plugin, string methodName )
        {
            return plugin.GetType().GetMethod( methodName );
        }

        public static bool ImplementsMethod( this IPlugin plugin, string methodName )
        {
            return plugin.GetMethod( methodName ) != null;
        }
    }
}
