using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Braces.Core;
using Braces.Core.ApiClientManager;
using Braces.UI;

namespace Braces.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // TODO: Limit external assembly's access (on UI and Core).
        public static MainWindow _MainWindow { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            UserConfig userConfig = new UserConfig();
            await userConfig.InitAsync();
            MainWindow mainWindow = new MainWindow( userConfig.Config );
            MainWindow = mainWindow;
            _MainWindow = mainWindow;
            MainWindow.Show();
            ApiClientManager.Instance.StartApiServer();
            // await PluginManager.Instance.FindPluginsAsync();
        }
    }
}
