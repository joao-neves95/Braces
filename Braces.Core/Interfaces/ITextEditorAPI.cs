using System;
using System.Collections.Generic;
using System.Text;

namespace Braces.Core
{
    public interface ITextEditorAPI
    {
        public void AddNewLineToEndOfFile(string content);

        public void AddTextAfterCaretPosition(string content);

        public void AddNewLineBelowCaretPosition(string content);

        public string GetCurrentLine();

        public void SetCurrentLne(string content);

        public string GetAllText();

        public void SetAllText(string content);
    }
}
