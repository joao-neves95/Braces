using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Braces.Core.Enums;

namespace Braces.Core.ExtensionSystem
{
    class TestPlugin : Plugin
    {
        public override List<string> FileTypes { get; } = new List<string>() { FileTypeName.TXT };

        public override string Name { get; } = "Test Plugin";

        public override string Description { get; } = "This is a plugin for testing";

        public override string[] Authors { get; } = new string[] { "shivayl" };

        public override string Version { get; } = "0.0.0.1";

        public override ExtensionType ExtensionType { get; } = ExtensionType.Plugin;

        public override void Execute()
        {
            return;
        }

        #region EVENT HANDLERS

        public override async Task OnFileOpen( object sender, RoutedEventArgs e, FileEventArgs args )
        {
            string message = $" This is from the Plugin { this.Name}.\n The name of the OPENED file is: { args.File.Name}.";
            args.TextEditor.Document.Blocks.Add( new Paragraph( new Run( message ) ) );
            Console.WriteLine( message );
        }

        public override async Task OnFileSave( object sender, RoutedEventArgs e, FileEventArgs args )
        {
            string message = $" This is from the Plugin {this.Name}.\n The name of the SAVED file is: {args.File.Name}.";
            args.TextEditor.Document.Blocks.Add( new Paragraph( new Run( message ) ) );
            Console.WriteLine( message );
        }

        #endregion
    }
}
