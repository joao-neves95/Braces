﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Braces.UI;

namespace Braces.API
{
    public static class TextEditor
    {
        public static void AddNewLineToEndOfFile( string content )
        {
            App._MainWindow.CurrentTextEditor.AddLineToEndOfFile( content );
        }

        public static void AddNewLineAfterCaretPosition( string contents )
        {
            App._MainWindow.CurrentTextEditor.AddNewLineAfterCaretPosition( contents );
        }
    }
}