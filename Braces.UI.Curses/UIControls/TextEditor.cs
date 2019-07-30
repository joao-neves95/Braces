using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Braces.UI.Curses.UIControls
{
    internal class TextEditor
    {
        public TextEditor(bool init = true)
        {
            if (init)
            {
                this.Init();
            }
        }

        #region PROPERTIES

        public TextView TextView { get; private set; }

        #endregion PROPERTIES

        #region METHODS

        public void Init()
        {
            this.TextView = new TextView()
            {
                X = 0,
                Y = Pos.Bottom(MainWindow.TopMenubar.MenuBar),
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
        }

        #endregion METHODS
    }
}
