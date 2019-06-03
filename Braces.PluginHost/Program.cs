using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Braces.Core;
using Braces.Core.Enums;
using Braces.Core.ExtensionSystem;
using Braces.UI.WPF.EventArguments;
using System.Diagnostics;

namespace Braces.PluginHost
{
    public class Program
    {
        private static List<Plugin> Plugins = new List<Plugin>();

        public const string PROTOCOL = "http";
        public const string PORT = "5000";

        public static HubConnection Connection { get; private set; }

        static async Task Main( string[] args )
        {
            // TODO: Load plugin assembly and sandbox it in a new domain (Docker?).
            // TODO: Load plugins and order them by language and only load them when necessary.
            Console.WriteLine( "Loading plugin..." );
            // The path is hardcoded for now.
            await LoadPlugin( "../../../Plugins/TesterPlugin/bin/Debug/TestPlugin.dll" );

            try
            {
                Console.WriteLine( "Testing the connection to the ApiServer..." );
                HttpClient httpClient = new HttpClient();
                // For testing the connection.
                // Returns void.
                HttpResponseMessage res = await httpClient.GetAsync( 
                    $"{PROTOCOL}://localhost:{PORT}/api/text-editor/This+is+a+test+message+from+the+Plugin-Host." 
                );

                if ( !res.IsSuccessStatusCode )
                {
                    throw new Exception( res.ReasonPhrase );
                }
                else
                {
                    Console.WriteLine( "Successfull connection with the ApiServer." );
                    httpClient.Dispose();
                }
            } catch (Exception e)
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
                Console.WriteLine( "The connection with the server failed. Exiting..." );
                Process.GetCurrentProcess().Kill( true );
            }

            Console.WriteLine( "Initializing PluginHosts's SignalR Client connection..." );
            await StartClient();

            Console.ReadLine();
            Console.WriteLine( "Exit" );
        }

        public static async Task StartClient()
        {
            Connection = new HubConnectionBuilder()
                //.WithUrl( $"{PROTOCOL}://localhost:{PORT}/ws/text-editor" )
                .WithUrl( $"{PROTOCOL}://127.0.0.1:{PORT}/ws/text-editor" )
                //.WithUrl( $"{PROTOCOL}://host.docker.internal:{PORT}/ws/text-editor" )
                //.WithUrl( $"{PROTOCOL}://172.17.160.1:{PORT}/ws/text-editor" )
                .Build();

            Connection.On<string, object>( EventName.OnFileOpen, async ( fileTypeName, args ) => await Program.FireEventOnPlugins( EventName.OnFileOpen, fileTypeName, args ) );
            Connection.On<string, object>( EventName.OnFileSave, async ( fileTypeName, args ) => await Program.FireEventOnPlugins( EventName.OnFileSave, fileTypeName, args ) );
            Connection.On<string, string>( APIMethods.ReceiveAllText, async ( fileTypeName, content ) => await Program.FireEventOnPlugins( APIMethods.ReceiveAllText, fileTypeName, content ) );

            Connection.Closed += async ( error ) =>
            {
                Console.WriteLine( "Connection lost. Trying to reconnect..." );
                await Task.Delay( new Random().Next( 0, 5 ) * 1000 );
                await Connection.StartAsync();
            };

            Console.WriteLine( "Starting the PluginHost connection..." );
            await Connection.StartAsync();
            await Connection.InvokeAsync( "BindPluginHost" );
            Console.ReadLine();
        }

        #region PRIVATE METHODS

        // TEMPORARY.
        private static async Task LoadPlugin( string path )
        {
            Console.WriteLine( Path.GetFullPath( path ) );

            try
            {
                Assembly thisPluginAsm = Assembly.LoadFile( Path.GetFullPath( "Plugins\\TesterPlugin\\bin\\Debug\\netcoreapp3.0\\TesterPlugin.dll" ) );
                // TODO: (Optimize) Use the name of the Plugin as the type for the assembly.
                Type thisPluginType = thisPluginAsm.GetTypes().First( type => type.BaseType.Name == "Plugin" );
                Plugins.Add( (Plugin)thisPluginAsm.CreateInstance( thisPluginType.ToString() ) );

                thisPluginAsm = Assembly.LoadFile( Path.GetFullPath( "Plugins\\Braces_JSONBeautifier\\bin\\Debug\\netcoreapp3.0\\Braces_JSONBeautifier.dll" ) );
                // TODO: (Optimize) Use the name of the Plugin as the type for the assembly.
                thisPluginType = thisPluginAsm.GetTypes().First( type => type.BaseType.Name == "Plugin" );
                Plugins.Add( (Plugin)thisPluginAsm.CreateInstance( thisPluginType.ToString() ) );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
            }
        }

        private static async Task FireEventOnPlugins( string eventName, string fileTypeName, object args )
        {
            Console.WriteLine( "FireEvent" );
            FileEventArgs eventArgs = new FileEventArgs( new Braces.Core.Models.FileModel( Path.Combine( FileStorage.GetHOMEPATH(), "_braces" ) ) );

            try
            {
                eventArgs = JsonConvert.DeserializeObject<FileEventArgs>( args.ToJSON() );
            }
            catch
            {
                // continue;
            }

            for (int i = 0; i < Plugins.Count; ++i)
            {
                // TODO: Optimize.
                if (!Plugins[i].FileTypes.Contains( fileTypeName ))
                    continue;

                switch (eventName)
                {
                    case EventName.OnFileOpen:
                        await Plugins[i].OnFileOpen( eventArgs );
                        break;
                    case EventName.OnFileSave:
                        await Plugins[i].OnFileSave( eventArgs );
                        break;
                    case APIMethods.ReceiveAllText:
                        await Plugins[i].OnReceiveAllText( (string)args );
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
