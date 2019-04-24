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
            // Simulate finding plugins.
            List<Plugin> fetchedPlugins = new List<Plugin> { /* new TestPlugin() */ };
            // LOAD PLUGIN:
            // TODO: Optimize this.
            // TODO: Add a convention for the typename, or add the type name to a configuration file.
            string assemblyPath = Path.GetFullPath( "../../../TestPlugin/bin/Debug/TestPlugin.dll" );
            Assembly ptrAssembly = Assembly.LoadFile( assemblyPath );
            Type thisPluginType = ptrAssembly.GetTypes().First( type => type.BaseType.Name == "Plugin" ); // .GetType();
            fetchedPlugins.Add( (Plugin)Activator.CreateInstance( thisPluginType ) );

            // List<Plugin> fetchedPlugins = 

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

        public async Task LoadPluginsAsync()
        {
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
        public async Task FireEventAsync(string eventName, string fileTypeName, object sender, RoutedEventArgs e, object args)
        {
            if (!this.PluginIdsByFileType.ContainsKey( fileTypeName ))
                return;

            Plugin currentPlugin;

            for (int i = 0; i < this.PluginIdsByFileType[fileTypeName].Count; ++i)
            {
                currentPlugin = this.Plugins[this.PluginIdsByFileType[fileTypeName][i]];
                
                if ( !currentPlugin.ImplementsMethod(eventName) )
                    continue;

                switch (eventName)
                {
                    case EventName.OnFileSave:
                        await currentPlugin.OnFileSave( sender, e, args );
                        break;
                    case EventName.OnFileOpen:
                        await currentPlugin.OnFileOpen( sender, e, args);
                        break;
                    default:
                        break;
                }
            }

            // SOLUTION WITH REFLEXION.
            //MethodInfo thisMethodInfo;
            //if (thisMethodInfo.IsNull())
            //    // TODO: Error handling.
            //    await Task.CompletedTask;
            //thisMethodInfo = thisFileTypePlugins[i].GetMethod( EventName.OnFileSave );
            //IPlugin thisNewPluginInstance = (IPlugin)Activator.CreateInstance( thisFileTypePlugins.GetType(), null );
            //Task thisTask = (Task)thisMethodInfo.Invoke( thisNewPluginInstance, new object[] { sender, e } );
            //await thisTask;
        }

        #endregion

        #region PRIVATE METHODS

        #endregion
    }
}

//This code loads a new dll to the current AppDomain.
//Assembly.LoadFrom(@"<DLL Path>");
