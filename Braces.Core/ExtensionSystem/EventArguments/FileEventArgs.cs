using System;
using System.Collections.Generic;
using System.Text;
using Braces.Core.Models;
using System.Windows;
using System.Windows.Controls;

namespace Braces.Core.ExtensionSystem
{
    public class FileEventArgs
    {
        public FileEventArgs( RichTextBox textEditor, FileModel fileModel )
        {
            this.TextEditor = textEditor;
            this.File = fileModel;
        }

        public RichTextBox TextEditor { get; }

        public FileModel File { get; }
    }
}
