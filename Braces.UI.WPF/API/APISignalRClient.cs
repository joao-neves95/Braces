using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.AspNetCore.SignalR.Client;
using Braces.Core.Enums;

namespace Braces.UI.WPF.API
{
    /// <summary>
    /// 
    /// (Singleton)
    /// It handles the communication between the server and the UI.
    /// All UIs must implement a similar SignalR API client to in order to cummunicate with the server.
    /// 
    /// </summary>
    public class APISignalRClient
    {
        #region SINGLETON CONTRUCTOR

        static APISignalRClient() { }

        private APISignalRClient() { }

        private static readonly APISignalRClient _instance = new APISignalRClient();

        /// <summary>
        /// 
        /// Get the current instance of this singleton.
        /// 
        /// </summary>
        public static APISignalRClient _
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region PRIVATE FIELDS

        public const string PROTOCOL = "http";
        public const string PORT = "5000";

        #endregion

        #region PRIVATE PROPERTIES

        private HubConnection Connection { get; set; }

        #endregion

        #region PUBLIC METHODS

        public async Task StartClient()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl( $"{PROTOCOL}://localhost:{PORT}/ws/text-editor" )
               .Build();

            SetAPIEventHandlers();

            Console.WriteLine( "Starting the SignalR UI API Client..." );
            await Connection.StartAsync();
            await Connection.InvokeAsync( "BindUI" );
        }

        #endregion

        #region PRIVATE METHODS

        private void SetAPIEventHandlers()
        {
            // THE API.

            Connection.On<string>( APIMethods.AddNewLineToEndOfFile, async content =>
            {
                await App.Current.Dispatcher.BeginInvoke( () => App._MainWindow.CurrentTextEditor.AddNewLineToEndOfFile( content ) );
            } );

            Connection.On<string>( APIMethods.AddNewLineBelowCaretPosition, async content => {
                await App.Current.Dispatcher.BeginInvoke( () => App._MainWindow.CurrentTextEditor.AddNewLineBelowCaretPosition( content ) );
            } );

            Connection.On<string>( APIMethods.AddTextAfterCaretPosition, async content => {
                await App.Current.Dispatcher.BeginInvoke( () => App._MainWindow.CurrentTextEditor.AddTextAfterCaretPosition( content ) );
            } );

            Connection.On<string>( APIMethods.SetAllText, async content => {
                await App.Current.Dispatcher.BeginInvoke( () => App._MainWindow.CurrentTextEditor.SetAllText( content ) );
            } );

            Connection.On<string>( APIMethods.GetCurrentLine, async content => {
                await App.Current.Dispatcher.BeginInvoke( () => Connection.SendAsync( APIMethods.SendCurrentLine, App._MainWindow.CurrentTextEditor.GetCurrentLine() ) );
            } );

            Connection.On<string>( APIMethods.GetAllText, async fileTypeName => {
                await App.Current.Dispatcher.BeginInvoke( () => Connection.SendAsync( APIMethods.SendAllText, fileTypeName, App._MainWindow.CurrentTextEditor.GetAllText() ) );
            } );

            // End of THE API.

            Connection.Closed += async (error) =>
            {
                Console.WriteLine( "Connection lost. Trying to reconnect..." );
                await Task.Delay( new Random().Next( 0, 5 ) * 1000 );
                await Connection.StartAsync();
            };
        }

        #endregion

    }
}
