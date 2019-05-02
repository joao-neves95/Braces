using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Braces.Core.ExtensionSystem;
using Braces.API;
using Braces.ApiServer.Interfaces;

namespace Braces.ApiServer.Hubs
{
    public class TextEditorHub : Hub<IPluginClient>
    {
        public async Task Join( string fileTypeName )
        {
            Console.WriteLine( $"Adding plugin for {fileTypeName} file types to hub..." );
            await Groups.AddToGroupAsync( Context.ConnectionId, fileTypeName );
        }

        public void AddNewLineToEnd( string content )
        {
            TextEditor.AddNewLineToEndOfFile( content );
        }

        public void AddNewLineAfterCaretPosition( string contents )
        {
            TextEditor.AddNewLineAfterCaretPosition( contents );
        }

        public override Task OnDisconnectedAsync( Exception exception )
        {
            Console.WriteLine( "Plugin disconnected." );
            return base.OnDisconnectedAsync( exception );
        }
    }
}
