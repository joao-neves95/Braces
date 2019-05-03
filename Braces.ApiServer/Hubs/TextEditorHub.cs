using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Braces.Core.Enums;
using Braces.ApiServer.Interfaces;

namespace Braces.ApiServer.Hubs
{
    public class TextEditorHub : Hub
    {
        public async Task Join( string fileTypeName )
        {
            Console.WriteLine( $"Adding plugin for {fileTypeName} file types to hub..." );
            await Groups.AddToGroupAsync( Context.ConnectionId, fileTypeName );
        }

        public async Task BindUI()
        {
            Console.WriteLine( "Binding UI..." );
            await Groups.AddToGroupAsync( Context.ConnectionId, SignalRGroupNames.UI );
            Console.WriteLine( "UI binding complete." );
        }
        
        public async Task AddNewLineToEnd( string content )
        {
            await Clients.Group( SignalRGroupNames.UI ).SendAsync( APIMethods.AddNewLineToEndOfFile, content );
        }

        public async Task AddNewLineAfterCaretPosition( string content )
        {
            await Clients.Group( SignalRGroupNames.UI ).SendAsync( APIMethods.AddNewLineAfterCaretPosition, content );
        }

        public override Task OnDisconnectedAsync( Exception exception )
        {
            Console.WriteLine( "Plugin disconnected." );
            return base.OnDisconnectedAsync( exception );
        }
    }
}
