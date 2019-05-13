using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Braces.Core;
using Braces.Core.Models;
using Braces.Core.Enums;
using Braces.Core.ExtensionSystem;
using Braces.UI.WPF.EventArguments;

namespace TestPlugin
{
    public static class Program
    {
        public static void Main() { }
    }

    public class TestPlugin : Plugin
    {
        #region PROPERTIES

        public override List<string> FileTypes { get; } = new List<string>() { FileTypeName.TXT };

        public override string Name { get; } = "Tester Plugin";

        public override string Description { get; } = "This is a plugin for testing";

        public override string[] Authors { get; } = new string[] { "shivayl" };

        public override string Version { get; } = "0.0.0.2";

        public override ExtensionType ExtensionType { get; } = ExtensionType.Plugin;

        #endregion

        public override void Execute()
        {
            return;
        }

        #region EVENT HANDLERS

        public override async Task OnFileOpen( object args )
        {
            FileEventArgs fileEvent = (FileEventArgs)args;
            string message = $" This is from the Plugin { this.Name }.\n The name of the OPENED file is: { fileEvent.File.Name }.";
            Console.WriteLine( message );
            await Braces.PluginHost.Program.Connection.InvokeCoreAsync( APIMethods.AddNewLineBelowCaretPosition , typeof(Task), new object[] { message } );
        }

        public override async Task OnFileSave( object args )
        {
            FileEventArgs fileEvent = (FileEventArgs)args;
            string message = $" This is from the Plugin { this.Name }.\n The name of the SAVED file is: { fileEvent.File.Name }.";
            Console.WriteLine( message );
            return;
            // await Braces.PluginHost.Program.Connection.InvokeCoreAsync( APIMethods.AddTextAfterCaretPosition, typeof(Task), new object[] { "SAVE" } );
        }

        #endregion
    }
}
