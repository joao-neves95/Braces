using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using IniParser.Model;

namespace Braces.UI
{
    public partial class UIConfigurator
    {
        public IniData UserConfig { get; private set; }

        public MainWindow That { get; private set; }

        public UIConfigurator(IniData userConfig, MainWindow mainWindow)
        {
            this.That = mainWindow;
            this.UserConfig = userConfig;
            this.Configurator();
        }

        private void Configurator()
        {
            That.SaveKeyBinding.Key = Key.S;
            That.SaveKeyBinding.Modifiers = ModifierKeys.Control;
            That.SaveBtn.InputGestureText = "Ctrl+S";
            That.RichTextBox.FontSize = 12;
            That.RichTextBox.FontFamily = new FontFamily("Segoe UI");
        }
    }
}
