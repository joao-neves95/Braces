using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Braces.Core.ExtensionSystem;
using System.Reflection;
using System.IO;

namespace Braces.PluginHost
{
    public class Program
    {
        private static List<Plugin> Plugins = new List<Plugin>();

        public const string FRIENDLY_NAME= "Braces_Sandbox";
        public const string PROTOCOL = "http";
        public const string PORT = "5000";

        public static HubConnection Connection { get; private set; }

        static async Task Main( string[] args )
        {
            // TODO: Load plugin assembly and sandbox it in a new domain (Docker?).
            // TODO: Load plugins and order them by language and only load them when necessary.
            Console.WriteLine( "Loading plugin..." );
            // The path is hardcoded for now.
            await LoadPlugin( "../../../TestPlugin/bin/Debug/TestPlugin.dll" );

            Console.WriteLine( "Initializing SignalR Client connection for all plugins..." );
            for (int i = 0; i < Plugins.Count; ++i)
            {
                await InitClient( Plugins[i] );
            }
        }

        public static async Task InitClient( IPlugin plugin )
        {
            plugin.Connection = new HubConnectionBuilder()
                .WithUrl( $"{PROTOCOL}://localhost:{PORT}/ws/text-editor" )
                .Build();

            plugin.Connection.Closed += async ( error ) =>
            {
                await Task.Delay( new Random().Next( 0, 5 ) * 1000 );
                await Connection.StartAsync();
            };

            await plugin.Connection.StartAsync();
        }

        public static async Task LoadPlugin( string path )
        {
            Console.WriteLine( Path.GetFullPath( path ) );

            try
            {
                Assembly thisPluginAsm = Assembly.LoadFile( Path.GetFullPath( "C:\\Users\\jpedrone\\DEV\\Braces\\TestPlugin\\bin\\Debug\\TestPlugin.dll" ) );
                // TODO: Use the name of the Plugin as the type for the assembly.
                Type thisPluginType = thisPluginAsm.GetTypes().First( type => type.BaseType.Name == "Plugin" );
                Plugins.Add( (Plugin)thisPluginAsm.CreateInstance( thisPluginType.ToString() ) );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
            }
        }
    }
}
