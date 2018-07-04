using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using IniParser.Model;
using Braces.Core.Enums;

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
            this.Configurator(userConfig);
        }

        private void Configurator(IniData userConfig)
        {
            // Instead of configurating with code, use property bindings on the XAML.
            That.SaveKeyBinding.Key = Utils.StringToKeyEnum<Key>( userConfig["key_bindings"]["save"] );
            That.SaveKeyBinding.Modifiers = Utils.StringToKeyEnum<ModifierKeys>( userConfig["key_bindings"]["save_modifiers"] );
            That.SaveBtn.InputGestureText = "Ctrl+S";
            That.RichTextBox.FontSize = 12;
            That.RichTextBox.FontFamily = new FontFamily("Segoe UI");
        }
    }
}
