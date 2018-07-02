using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Braces.Core;

namespace Braces.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            UserConfig userConfig = new UserConfig();
            await userConfig.InitAsync();
            MainWindow = new MainWindow(userConfig.Config);
            MainWindow.Show();
        }
    }
}
