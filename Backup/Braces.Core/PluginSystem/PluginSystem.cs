using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Braces.Core.PluginSystem
{
    class PluginSystem
    {
        public string PluginsPath { get; set; }

        public List<IPlugin> Plugins { get; set; }

        public PluginSystem()
        {
        }

        public async Task LoadPluginsAsync()
        {
        }

        public async Task ExecutePluginsAsync()
        {
        }
    }
}

//This code loads a new dll to the current AppDomain.
//Assembly.LoadFrom(@"<DLL Path>");
