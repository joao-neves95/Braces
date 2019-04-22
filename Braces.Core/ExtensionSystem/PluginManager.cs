using System;
using System.Collections.Generic;
using System.Reflection;
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
            List<Plugin> fetchedPlugins = new List<Plugin> { new TestPlugin() };
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

            await Task.CompletedTask;
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
                await Task.CompletedTask;

            Plugin currentPlugin;

            for (int i = 0; i < this.PluginIdsByFileType[fileTypeName].Count; ++i)
            {
                currentPlugin = this.Plugins[this.PluginIdsByFileType[fileTypeName][i]];
                
                switch (eventName)
                {
                    case EventName.OnFileSave:
                        await currentPlugin.OnFileSave( sender, e, (FileEventArgs)args );
                        break;
                    case EventName.OnFileOpen:
                        await currentPlugin.OnFileOpen( sender, e, (FileEventArgs)args);
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
