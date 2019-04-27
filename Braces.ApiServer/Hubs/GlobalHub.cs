using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Windows;
using Braces.Core.ExtensionSystem;
using Braces.Core.Enums;

namespace Braces.ApiServer.Hubs
{
    public class GlobalHub : Hub<Plugin>
    {
        #region PROPERTIES

        private readonly ExtensionSystem ExtSystem = ExtensionSystem.Instance;

        #endregion

        #region METHODS

        public async Task FireEvent( string eventName, string fileTypeName, object sender, RoutedEventArgs e, object args )
        {
            if (!ExtSystem.PluginManager.PluginIdsByFileType.ContainsKey( fileTypeName ))
                return;

            Plugin thisGroup = Clients.Group( fileTypeName );
            Plugin currentPlugin;

            for (int i = 0; i < ExtSystem.PluginManager.PluginIdsByFileType[fileTypeName].Count; ++i)
            {
                currentPlugin = ExtSystem.PluginManager.Plugins[ExtSystem.PluginManager.PluginIdsByFileType[fileTypeName][i]];

                if (!currentPlugin.ImplementsMethod( eventName ))
                    continue;

                switch (eventName)
                {
                    case EventName.OnFileSave:
                        await thisGroup.OnFileSave( sender, e, args );
                        break;
                    case EventName.OnFileOpen:
                        await thisGroup.OnFileOpen( sender, e, args );
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task AddEvent()
        {
            return;
        }

        #endregion
    }
}
