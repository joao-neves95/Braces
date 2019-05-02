using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Braces.Core.ExtensionSystem;
using System.Windows;
using Braces.ApiServer.Interfaces;
using Braces.API;

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
            try
            {
                TextEditor.AddNewLineAfterCaretPosition( contents );

            } catch (Exception e)
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
            }
        }

        public override Task OnDisconnectedAsync( Exception exception )
        {
            Console.WriteLine( "Plugin disconnected." );
            return base.OnDisconnectedAsync( exception );
        }
    }
}
