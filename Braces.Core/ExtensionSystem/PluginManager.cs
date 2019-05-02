using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Braces.Core.Enums;
using Braces.Core.ApiClientManager;
using Braces.Core.DTOs;

namespace Braces.Core.ExtensionSystem
{
    /// <summary>
    /// 
    /// (Singleton)
    /// 
    /// </summary>
    public sealed class PluginManager
    {
        #region SINGLETON CONTRUCTOR

        static PluginManager() { }

        private PluginManager() { }

        private static readonly PluginManager _instance = new PluginManager();

        public static PluginManager Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        public string PluginsPath { get; set; }

        /// <summary>
        /// 
        /// Key: \<string\> (Plugin.Id)
        /// Value: \<Plugin\>
        /// 
        /// </summary>
        public Dictionary<string, Plugin> Plugins { get; } = new Dictionary<string, Plugin>();

        /// <summary>
        /// 
        /// Key: <string> (FileTypeName.[extension])
        /// Value: List<string> (Plugin.Id)
        /// 
        /// </summary>
        public Dictionary<string, List<string>> PluginIdsByFileType { get; } = new Dictionary<string, List<string>>();

        #region METHODS

        // TODO: Pass to the ExtensionManager and call this FindExtensionsAsync
        public async Task FindPluginsAsync()
        {
            // LOAD PLUGIN:
            // This method of loading is temporary. In the future I will probably create a server
            // to handle the comunication between the plugin and the UI.
            List<Plugin> fetchedPlugins = new List<Plugin> { /* new TestPlugin() */ };
            fetchedPlugins.Add( this.LoadPlugin( "../../../TestPlugin/bin/Debug/TestPlugin.dll" ) );

            string currentFileType;

            for (int i = 0; i < fetchedPlugins.Count; ++i)
            {
                this.Plugins.Add( fetchedPlugins[i].Id, fetchedPlugins[i] );

                for (int j = 0; j < fetchedPlugins[i].FileTypes.Count; ++j)
                {
                    currentFileType = fetchedPlugins[i].FileTypes[j];

                    if (this.PluginIdsByFileType.ContainsKey( currentFileType ))
                        this.PluginIdsByFileType[currentFileType].Add( fetchedPlugins[i].Id );
                    else
                        this.PluginIdsByFileType.Add( currentFileType, new List<string>() { fetchedPlugins[i].Id } );
                }
            }

            return;
        }

        public Plugin LoadPlugin( string path )
        {
            string assemblyPath = Path.GetFullPath( path );
            Assembly ptrAssembly = Assembly.LoadFile( assemblyPath );
            Type thisPluginType = ptrAssembly.GetTypes().First( type => type.BaseType.Name == "Plugin" );
            return (Plugin)Activator.CreateInstance( thisPluginType );
        }

        public async Task ExecutePluginsAsync()
        {
        }

        /// <summary>
        /// 
        /// Fire the provided event type for all plugins from the provided file type.
        /// 
        /// </summary>
        /// <param name="eventName"> Event name from the EventName class of constansts. </param>
        /// <param name="fileType"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task FireEventAsync(string eventName, string fileTypeName, object args)
        {
            await ApiClientManager.ApiClientManager.Instance.PostAsJSON( "text-editor/fire-event", new FireEventDTO()
            {
                eventName = eventName,
                fileTypeName = fileTypeName,
                args = args
            } );

            //if (!this.PluginIdsByFileType.ContainsKey( fileTypeName ))
            //    return;

            //Plugin currentPlugin;

            //for (int i = 0; i < this.PluginIdsByFileType[fileTypeName].Count; ++i)
            //{
            //    currentPlugin = this.Plugins[this.PluginIdsByFileType[fileTypeName][i]];

            //    if ( !currentPlugin.ImplementsMethod(eventName) )
            //        continue;

            //    // TODO: Instead of calling the plugin directly, call the clients subscribed to the correct Hub instead.
            //    switch (eventName)
            //    {
            //        case EventName.OnFileSave:
            //            await currentPlugin.OnFileSave( sender, e, args );
            //            break;
            //        case EventName.OnFileOpen:
            //            await currentPlugin.OnFileOpen( sender, e, args);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

        public async Task FireEventAsync2( string eventName, string fileTypeName, object sender, RoutedEventArgs e, object args )
        {
        }

        #endregion

        #region PRIVATE METHODS

        #endregion
    }
}
