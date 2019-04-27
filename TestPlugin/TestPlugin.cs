using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Braces.Core.ExtensionSystem;
using Braces.Core.Enums;
using Braces.UI.UserControls;

namespace TestPlugin
{
    public static class Program
    {
        public static void Main() { }
    }

    class TestPlugin : Plugin
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

        public override async Task OnFileOpen( object sender, RoutedEventArgs e, object args )
        {
            Braces.UI.ExtensionSystem.FileEventArgs eventArgs = (Braces.UI.ExtensionSystem.FileEventArgs)args;
            string message = $" This is from the Plugin { this.Name}.\n The name of the OPENED file is: { eventArgs.File.Name}.";
            Console.WriteLine( message );
            this.AddMesageToTextEditor( eventArgs.TextEditor, message );
        }

        public override async Task OnFileSave( object sender, RoutedEventArgs e, object args )
        {
            Braces.UI.ExtensionSystem.FileEventArgs eventArgs = (Braces.UI.ExtensionSystem.FileEventArgs)args;
            string message = $" This is from the Plugin {this.Name}.\n The name of the SAVED file is: {eventArgs.File.Name}.";
            Console.WriteLine( message );
            this.AddMesageToTextEditor( eventArgs.TextEditor, message );
        }

        #endregion

        #region PRIVATE METHODS

        private void AddMesageToTextEditor( TextEditorControl textEditor, string message )
        {
            textEditor.RichTextBox.Document.Blocks.Add( new Paragraph( new Run( message ) ) );
            textEditor.AddLineNumber();
            textEditor.AddLineNumber();
        }

        #endregion
    }
}
