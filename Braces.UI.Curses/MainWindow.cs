using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;
using Braces.UI.Curses.UIControls;

namespace Braces.UI.Curses
{
    // TODO: (Braces.UI.Curses.MainWindow) - Turn this into a singleton.
    internal class MainWindow
    {
        public MainWindow(bool init = true)
        {
            if (init)
            {
                this.Init();
            }
        }

        #region PROPERTIES

        public static Window Window { get; private set; }

        public static TopMenubar TopMenubar { get; private set; }

        public static TextEditor TextEditor { get; private set; }

        #endregion PROPERTIES

        #region METHODS

        public void Init()
        {
            Toplevel top = Application.Top;

            MainWindow.Window = new Window(" { BRACES Cursed } ")
            {
                X = 0,
                Y = 1, // Leave one row for the toplevel menu
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            top.Add(MainWindow.Window);
            MainWindow.TopMenubar = new TopMenubar();
            top.Add(MainWindow.TopMenubar.MenuBar);
            MainWindow.TextEditor = new TextEditor();
            MainWindow.Window.Add(MainWindow.TextEditor.TextView);
        }

        #endregion METHODS
    }
}
