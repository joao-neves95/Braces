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
using System.Windows.Controls;

namespace Braces.UI
{
    public partial class UIConfigurator
    {
        #region PROPERTIES
        public MainWindow That { get; private set; }

        public IniData UserConfig { get; private set; }

        public ModifierKeys DefaultModifier { get; set; }
        #endregion

        public UIConfigurator(IniData userConfig, MainWindow mainWindow)
        {
            this.That = mainWindow;
            this.UserConfig = userConfig;
            this.ConfigureUI();
        }

        private void ConfigureUI()
        {
            this.DefaultModifier = Utils.StringToKeyEnum<ModifierKeys>( this.UserConfig["key_bindings"]["default_modifier"] );
            ConfigureKey("save", That.SaveKeyBinding, That.SaveBtn);
            ConfigureKey("open", That.OpenKeyBinding, That.OpenBtn);

            That.richTextBox.FontSize = 12;
            That.richTextBox.FontFamily = new FontFamily("Segoe UI");
        }

        private void ConfigureKey(string inputKey, KeyBinding keyBinding, MenuItem menuItem)
        {
            if (this.UserConfig["key_bindings"].ContainsKey(inputKey + "_modifiers"))
            {
                ModifierKeys modifierKeys = Utils.StringToKeyEnum<ModifierKeys>(this.UserConfig["key_bindings"][inputKey + "_modifiers"]);
                keyBinding.Modifiers = modifierKeys;
                That.SaveBtn.InputGestureText += modifierKeys.ToString();
            }
            else
            {
                keyBinding.Modifiers = this.DefaultModifier;
                menuItem.InputGestureText = this.DefaultModifier.ToString();
            }

            Key key = Utils.StringToKeyEnum<Key>( this.UserConfig["key_bindings"][inputKey] );
            keyBinding.Key = key;
            menuItem.InputGestureText += $"+{ key.ToString() }";
        }
    }
}
