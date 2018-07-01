using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using System.IO;
using Braces.Core.FileSystem;
using Braces.Core.Models;

namespace Braces.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: Refactor the configuration.
        ConfigurationModel configuration = new ConfigurationModel { Modifier = "Control", SaveKey = "S" };

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.ExplorerIsVisible = false;
            this.SearchIsVisible = false;
            SaveKeyBinding.Key = Key.S;
            SaveKeyBinding.Modifiers = ModifierKeys.Control;
        }


        #region SIDENAV

        #region SIDENAV EXPLORER
        public bool ExplorerIsVisible { get; private set; }

        private void BtnSideNavExplorer_Click(object sender, RoutedEventArgs e)
        {
            this.ShowHideSideNav(GetStoryboard(ExplorerIsVisible), explorerPanel);
            this.ExplorerIsVisible = !ExplorerIsVisible;
        }
        #endregion

        #region SIDENAV SHOW
        public bool SearchIsVisible { get; private set; }

        private void BtnSideNavSearch_Click(object sender, RoutedEventArgs e)
        {
            this.ShowHideSideNav(GetStoryboard(SearchIsVisible), searchPanel);
            this.SearchIsVisible = !SearchIsVisible;
        }
        #endregion

        private void ShowHideSideNav(string Storyboard, Grid target)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(target,HandoffBehavior.SnapshotAndReplace, true);
        }

        private string GetStoryboard(bool isVisible)
        {
            string storyboard;
            if (isVisible)
                storyboard = "sbHideSideNav";
            else
                storyboard = "sbShowSideNav";

            return storyboard;
        }

        #endregion

        #region TOPBAR MENU

        private async void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            // TEST PATH.
            string filePath = "C:\\Users\\Utilizador\\odrive\\ISTEC\\DEV\\test\\Braces\\Braces.Core\\FileSystem\\TestSaves\\testfile.txt";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Fired when the use clicks on "open".
            if (openFileDialog.ShowDialog() == true)
            {
                byte[] fileContent = await FileStorage.ReadFileAsync(filePath);
                RichTextBox.Document.Blocks.Clear();
                RichTextBox.Document.Blocks.Add(new Paragraph(new Run(new UnicodeEncoding().GetString(fileContent))));
            }
        }


        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // See TextRange Class
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.documents.textrange?view=netframework-4.7.2

            TextRange userInput = new TextRange(
                RichTextBox.Document.ContentStart,
                RichTextBox.Document.ContentEnd
            );

            byte[] encodedInput = new UnicodeEncoding().GetBytes(userInput.Text);

            // TEST PATH.
            string filePath = "C:\\Users\\Utilizador\\odrive\\ISTEC\\DEV\\test\\Braces\\Braces.Core\\FileSystem\\TestSaves\\testfile.txt";

            await FileStorage.SaveFileAsync(filePath, encodedInput);
        }
        #endregion
    }
}
