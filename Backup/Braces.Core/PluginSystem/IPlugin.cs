// The main guidelines for a plugin.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Braces.Core.PluginSystem
{
    public interface IPlugin : IExtension
    {
        string PluginName { get; }

        void Execute();
    }
}
