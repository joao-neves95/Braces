using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Windows;
using Braces.Core.Enums;
using Braces.Core.ExtensionSystem;

namespace Braces.ApiServer
{
    public static class Handlers
    {
        public static async Task FireEvent<THub, T>( IHubContext<THub, T> THubContext, string fileTypeName, string eventName, object sender, RoutedEventArgs e, object args )
            where THub : Hub<T>
            where T : Plugin
        {
            try
            {
                await Handlers.CallEventMethod( THubContext.Clients.Groups( fileTypeName ), eventName, sender, e, args );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message );
            }
        }

        private static async Task CallEventMethod( Plugin plugin, string eventName, object sender, RoutedEventArgs e, object args )
        {
            switch (eventName)
            {
                case EventName.OnFileOpen:
                    await plugin.OnFileOpen( sender, e, args );
                    break;
                case EventName.OnFileSave:
                    await plugin.OnFileSave( sender, e, args );
                    break;
                default:
                    break;
            }
        }
    }
}
