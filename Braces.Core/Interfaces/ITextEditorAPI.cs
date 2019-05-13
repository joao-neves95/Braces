using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Braces.Core.Interfaces
{
    public interface ITextEditorAPI
    {
        public Task AddNewLineToEndOfFile(string content);

        public Task AddTextAfterCaretPosition(string content);

        public Task AddNewLineBelowCaretPosition(string content);

        public Task<string> GetCurrentLine();

        public Task SetCurrentLne(string content);

        public Task<string> GetAllText();

        public Task SetAllText(string content);
    }
}
