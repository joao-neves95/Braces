using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Braces.Core.Enums;
using Braces.ApiServer.Interfaces;
using Braces.Core.Interfaces;

namespace Braces.ApiServer.Hubs
{
    public class TextEditorHub : Hub
    {
        public async Task Join( string fileTypeName )
        {
            Console.WriteLine( $"Adding plugin for {fileTypeName} file types to hub..." );
            await Groups.AddToGroupAsync( Context.ConnectionId, fileTypeName );
        }

        public async Task BindPluginHost()
        {
            Console.WriteLine( "Binding PluginHost..." );
            await Groups.AddToGroupAsync( Context.ConnectionId, SignalRGroupNames.PLUGIN_HOST );
            Console.WriteLine( "PluginHost binding complete." );
        }

        public async Task BindUI()
        {
            Console.WriteLine( "Binding UI..." );
            await Groups.AddToGroupAsync( Context.ConnectionId, SignalRGroupNames.UI );
            Console.WriteLine( "UI binding complete." );
        }
        
        public async Task AddNewLineToEndOfFile( string content )
        {
            await Clients.Group( SignalRGroupNames.UI ).SendAsync( APIMethods.AddNewLineToEndOfFile, content );
        }

        public async Task AddNewLineBelowCaretPosition( string content )
        {
            await Clients.Group( SignalRGroupNames.UI ).SendAsync( APIMethods.AddNewLineBelowCaretPosition, content );
        }

        public async Task AddTextAfterCaretPosition(string content)
        {
            await Clients.Group( SignalRGroupNames.UI ).SendAsync( APIMethods.AddTextAfterCaretPosition, content );
        }

        public override Task OnDisconnectedAsync( Exception exception )
        {
            Console.WriteLine( "Plugin disconnected." );
            return base.OnDisconnectedAsync( exception );
        }

        public async Task GetCurrentLine()
        {
            await Clients.Group( SignalRGroupNames.UI ).SendAsync( APIMethods.GetCurrentLine );
        }

        /// <summary>
        /// 
        /// Used when the text editor sends the current line to the plugin.
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SendCurrentLine( string currentLine )
        {
            await Clients.Group( SignalRGroupNames.PLUGIN_HOST ).SendAsync( APIMethods.ReceiveCurrentLine, currentLine );
        }

        public Task SetCurrentLine(string content)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllText( string fileTypeName )
        {
            Console.WriteLine( "Plugin requested GetAllText()" );
            await Clients.Group( SignalRGroupNames.UI ).SendAsync( APIMethods.GetAllText, fileTypeName );
        }

        public async Task SendAllText( string fileTypeName, string content )
        {
            Console.WriteLine( "The UI is sending SendAllText()" );
            await Clients.Group( SignalRGroupNames.PLUGIN_HOST ).SendAsync( APIMethods.ReceiveAllText, fileTypeName, content );
        }

        public async Task SetAllText(string content)
        {
            Console.WriteLine( "The Plugin requested SetAllText()" );
            await Clients.Group( SignalRGroupNames.UI ).SendAsync( APIMethods.SetAllText, content );
        }
    }
}
