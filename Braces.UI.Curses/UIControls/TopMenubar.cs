using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Braces.UI.Curses.UIControls
{
    // TODO: (Braces.UI.Curses.UIControls.TopMenubar) - Make this reusable (generic and not hardcoded).
    internal class TopMenubar
    {
        public TopMenubar(bool init = true)
        {
            if (init)
            {
                this.Init();
            }
        }

        #region PROPERTIES  

        public MenuBar MenuBar { get; private set; }

        #endregion PROPERTIES  

        #region METHODS

        public void Init()
        {
            Toplevel top = Application.Top;

            this.MenuBar = new MenuBar(
                new MenuBarItem[]
                {
                    new MenuBarItem(
                        "_File",
                        new MenuItem[]
                        {
                            new MenuItem("_New", "Creates a new file", null),
                            new MenuItem("_Close", "", null),
                            new MenuItem ("_Quit", "", null)
                        }
                    ),
                    new MenuBarItem(
                        "_Edit",
                        new MenuItem[]
                        {
                            new MenuItem("_Copy", "", null),
                            new MenuItem("_Cut", "", null),
                            new MenuItem("_Paste", "", null)
                        }
                    )
                }
            );
        }

        #endregion METHODS
    }
}
