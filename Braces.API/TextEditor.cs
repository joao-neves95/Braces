using System;
using System.Collections.Generic;
using System.Text;
using Braces.UI.WPF;

namespace Braces.API
{
    public static class TextEditor
    {
        public static void AddNewLineToEndOfFile(string content)
        {
            App._MainWindow.CurrentTextEditor.AddLineToEndOfFile( content );
        }

        public static void AddNewLineAfterCaretPosition(string contents)
        {
            // TODO: Fix (workaround) sharing data (MainWindow instance) between the two processes. Or move the server to WPF.
            App._MainWindow.CurrentTextEditor.AddNewLineAfterCaretPosition( contents );
        }
    }
}
