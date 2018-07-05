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
        #region PROPERTIES
        public IniData UserConfig { get; private set; }

        public MainWindow That { get; private set; }
        #endregion

        public UIConfigurator(IniData userConfig, MainWindow mainWindow)
        {
            this.That = mainWindow;
            this.UserConfig = userConfig;
            this.ConfigureUI(userConfig);
        }

        private void ConfigureUI(IniData userConfig)
        {
            ModifierKeys defaultModifier = Utils.StringToKeyEnum<ModifierKeys>( userConfig["key_bindings"]["default_modifier"] );

            if (userConfig["key_bindings"].ContainsKey("save_modifiers"))
            {
                ModifierKeys saveModifier = Utils.StringToKeyEnum<ModifierKeys>(userConfig["key_bindings"]["save_modifiers"]);
                That.SaveKeyBinding.Modifiers = saveModifier;
                That.SaveBtn.InputGestureText += saveModifier.ToString();
            }
            else
            {
                That.SaveKeyBinding.Modifiers = defaultModifier;
                That.SaveBtn.InputGestureText = defaultModifier.ToString();
            }
            Key saveKey = Utils.StringToKeyEnum<Key>(userConfig["key_bindings"]["save"]);
            That.SaveKeyBinding.Key = saveKey;
            That.SaveBtn.InputGestureText += $"+{ saveKey.ToString() }";

            That.RichTextBox.FontSize = 12;
            That.RichTextBox.FontFamily = new FontFamily("Segoe UI");
        }
    }
}
