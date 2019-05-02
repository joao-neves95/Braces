using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Newtonsoft.Json;
using Braces.Core;
using Braces.Core.Models;
using Braces.Core.Enums;
using Braces.Core.ExtensionSystem;
using Braces.UI.ExtensionSystem;

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

        public override string Name { get; } = "Test Plugin";

        public override string Description { get; } = "This is a plugin for testing";

        public override string[] Authors { get; } = new string[] { "shivayl" };

        public override string Version { get; } = "0.0.0.1";

        public override ExtensionType ExtensionType { get; } = ExtensionType.Plugin;

        #endregion

        public override void Execute()
        {
            return;
        }

        #region EVENT HANDLERS

        public override async Task OnFileOpen( object args )
        {
            Console.WriteLine( "Here" );
            Console.WriteLine( args );
            Console.WriteLine( args.ToJSON() );
            FileEventArgs fileEvent = (FileEventArgs)args;
            string message = $" This is from the Plugin { this.Name }.\n The name of the OPENED file is: { fileEvent.File.Name }.";
            Console.WriteLine( message );
            await Braces.PluginHost.Program.Connection.InvokeCoreAsync( "AddNewLineAfterCaretPosition", typeof(void), new object[] { message } );
        }

        public override async Task OnFileSave( object args )
        {
            FileEventArgs fileEvent = (FileEventArgs)args;
            string message = $" This is from the Plugin { this.Name }.\n The name of the SAVED file is: { fileEvent.File.Name }.";
            Console.WriteLine( message );
            await Braces.PluginHost.Program.Connection.InvokeCoreAsync( "AddNewLineAfterCaretPosition", typeof(void), new object[] { message } );
        }

        #endregion
    }
}
