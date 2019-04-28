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
        }

        public async Task AddEvent()
        {
            return;
        }

        public async Task AddComponent(object req)
        {
        }

        #endregion
    }
}
