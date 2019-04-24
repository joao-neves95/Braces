using System;
using System.Collections.Generic;
using System.Text;
using Braces.Core.Models;
using System.Windows;
using System.Windows.Controls;
using Braces.UI.UserControls;

namespace Braces.UI.ExtensionSystem
{
    public class FileEventArgs
    {
        public FileEventArgs( TextEditorControl textEditor, FileModel fileModel )
        {
            this.TextEditor = textEditor;
            this.File = fileModel;
        }

        public TextEditorControl TextEditor { get; }

        public FileModel File { get; }
    }
}
