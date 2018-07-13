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
        #region CTOR

        public UIConfigurator(IniData userConfig, MainWindow mainWindow)
        {
            this.MainWnd = mainWindow;
            this.UserConfig = userConfig;
            this.ConfigureUI();
        }

        #endregion

        #region PROPERTIES

        public MainWindow MainWnd { get; private set; }

        public IniData UserConfig { get; private set; }

        public ModifierKeys DefaultModifier { get; set; }

        public string OpenPath { get; set; }

        #endregion

        public void ConfigureUI()
        {
            this.DefaultModifier = Utils.StringToKeyEnum<ModifierKeys>( this.UserConfig["key_bindings"]["default_modifier"] );
            ConfigureKey("save", MainWnd.SaveKeyBinding, MainWnd.SaveBtn);
            ConfigureKey("open", MainWnd.OpenKeyBinding, MainWnd.OpenBtn);

            this.OpenPath = this.UserConfig["env"]["open_path"];

        }

        public void UpdateTextEditorConfig()
        {
            MainWnd.CurrentTextEditor.richTextBox.FontSize = 12;
            MainWnd.CurrentTextEditor.richTextBox.FontFamily = new FontFamily("Segoe UI");
        }

        private void ConfigureKey(string inputKey, KeyBinding keyBinding, MenuItem menuItem)
        {
            if (this.UserConfig["key_bindings"].ContainsKey(inputKey + "_modifiers"))
            {
                ModifierKeys modifierKeys = Utils.StringToKeyEnum<ModifierKeys>( this.UserConfig["key_bindings"][inputKey + "_modifiers"] );
                keyBinding.Modifiers = modifierKeys;
                MainWnd.SaveBtn.InputGestureText += modifierKeys.ToString();
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
