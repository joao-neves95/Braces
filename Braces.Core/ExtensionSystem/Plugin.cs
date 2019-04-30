using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Braces.Core.Enums;
using Microsoft.AspNetCore.SignalR.Client;

namespace Braces.Core.ExtensionSystem
{
    // TODO: Add the SignalR client in order to not make every user repeat this task.
    public abstract class Plugin : IPlugin
    {
        #region INTERFACE CONTENTS

        public abstract List<string> FileTypes { get; }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string[] Authors { get; }

        public abstract string Version { get; }

        public abstract ExtensionType ExtensionType { get; }

        public HubConnection Connection { get; set; }

        public abstract void Execute();

        #endregion

        public Plugin()
        {
            this.Id = Utilities.RandomAlphaNumeric( 15 );
        }

        #region PRIVATE PROPERTIES

        public string Id { get; }

        public string Path { get; } = "";

        #endregion

        #region PUBLIC PROPERTIES

        //public 

        #endregion

        #region EVENTS

        /// <summary>
        /// 
        /// Windows OnFileOpen event.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual async Task OnFileOpen( object sender, RoutedEventArgs e, object args ) { }

        /// <summary>
        /// 
        /// Windows OnFileOpen event.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual async Task OnFileSave( object sender, RoutedEventArgs e, object args ) { }

        public virtual async Task OnTextEditorKeyDown( object sender, RoutedEventArgs e, object args ) { }

        #endregion
    }
}
