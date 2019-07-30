using System;
using Terminal.Gui;

namespace Braces.UI.Curses
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            new MainWindow();
            Application.Run();
        }
    }
}
