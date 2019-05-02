using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Braces.Core;
using Braces.Core.Enums;
using Braces.Core.ExtensionSystem;
using Braces.UI.ExtensionSystem;

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
            await LoadPlugin( "../../../TestPlugin/bin/Debug/TestPlugin.dll" );

            Console.WriteLine( "Initializing PluginHosts's SignalR Client connection..." );
            await InitClient( null );

            Console.ReadLine();
            Console.WriteLine( "Exit" );
        }

        public static async Task InitClient( Plugin plugin )
        {
            Connection = new HubConnectionBuilder()
                .WithUrl( $"{PROTOCOL}://localhost:{PORT}/ws/text-editor" )
                .Build();

            Connection.On<string, object>( EventName.OnFileOpen, async ( fileTypeName, args ) => await Program.FireEventOnPlugins( EventName.OnFileOpen, fileTypeName, args ) );
            Connection.On<string, object>( EventName.OnFileSave, async ( fileTypeName, args ) => await Program.FireEventOnPlugins( EventName.OnFileSave, fileTypeName, args ) );

            Connection.Closed += async ( error ) =>
            {
                Console.WriteLine( "Connection lost. Trying to reconnect..." );
                await Task.Delay( new Random().Next( 0, 5 ) * 1000 );
                await Connection.StartAsync();
            };

            Console.WriteLine( "Starting the connection..." );
            await Connection.StartAsync();
            Console.ReadLine();
        }

        #region PRIVATE METHODS

        private static async Task LoadPlugin( string path )
        {
            Console.WriteLine( Path.GetFullPath( path ) );

            try
            {
                Assembly thisPluginAsm = Assembly.LoadFile( Path.GetFullPath( "D:\\joao9\\odrive\\ISTEC\\DEV\\Braces\\TestPlugin\\bin\\Debug\\TestPlugin.dll" ) );
                // TODO: (Optimize) Use the name of the Plugin as the type for the assembly.
                Type thisPluginType = thisPluginAsm.GetTypes().First( type => type.BaseType.Name == "Plugin" );
                Plugins.Add( (Plugin)thisPluginAsm.CreateInstance( thisPluginType.ToString() ) );
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex );
            }
        }

        private static async Task FireEventOnPlugins( string eventName, string fileTypeName, object args )
        {
            FileEventArgs eventArgs = JsonConvert.DeserializeObject<FileEventArgs>( args.ToJSON() );

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
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
