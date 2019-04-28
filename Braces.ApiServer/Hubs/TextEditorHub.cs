using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Braces.API;

namespace Braces.ApiServer.Hubs
{
    public class TextEditorHub : Hub
    {
        public void AddNewLineToEnd( string content )
        {
            TextEditor.AddNewLineToEndOfFile( content );
        }

        public void AddNewLineAfterCaretPosition( string contents )
        {
            TextEditor.AddNewLineAfterCaretPosition( contents );
        }
    }
}
