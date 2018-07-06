using System;
using System.IO;
using System.Collections;
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
using Braces.Core;
using Braces.Core.Models;
using Braces.UI.UserControls;
using IniParser.Model;

namespace Braces.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IniData userConfig)
        {
            InitializeComponent();
            this.DataContext = this;
            this.ExplorerIsVisible = false;
            this.SearchIsVisible = false;
            this.CurrentFile = null;

            // TODO: Implement the editor configurator.
            this.UIConfiguraton = new UIConfigurator(userConfig, this);

            // Change after tab system.
            this.CurrentTextEditor = textEditor1;
        }

        #region PROPERTIES

        // TODO: Use the FileModel instead.
        public UIConfigurator UIConfiguraton { get; set; }

        public FileModel CurrentFile { get; set; }

        private TextEditorControl currentTextEditor;
        public TextEditorControl CurrentTextEditor
        {
            get
            {
                return this.currentTextEditor;
            }
            set
            {
                this.currentTextEditor = value;
                UIConfiguraton.UpdateTextEditorConfig();
            }
        }

        #endregion

        #region PROPERTY BINDINGS
        #endregion

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
            // TODO: Break the path to test and improve exception handling.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = this.UIConfiguraton.OpenPath;
            bool? userSelectedFile = openFileDialog.ShowDialog();

            // Fired when the user clicks on "open".
            if (userSelectedFile == true)
            {
                string filePath = openFileDialog.FileName;
                string fileContent = await FileStorage.ReadFileAsStringAsync(filePath);
                this.CurrentTextEditor.richTextBox.Document.Blocks.Clear();
                this.CurrentTextEditor.richTextBox.Document.Blocks.Add(new Paragraph(new Run(fileContent)));

                this.CurrentFile = new FileModel(filePath);
            }
        }

        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        // TODO: Prompt the user where to save. Store as property.
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // About the TextRange Class:
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.documents.textrange?view=netframework-4.7.2

            TextRange userInput = new TextRange(
                this.CurrentTextEditor.richTextBox.Document.ContentStart,
                this.CurrentTextEditor.richTextBox.Document.ContentEnd
            );

            byte[] encodedInput = new UnicodeEncoding().GetBytes(userInput.Text);

            if (this.CurrentFile == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All Files|*.* |C|*.c |C#|*.cs |CSS|*.css |gitignore|.gitignore |HTML|*.html |JavaScript|*.js |Markdown|*.md |No Extension|*. |PHP|.php |SQL|*.sql |Text Files|*.txt";
                // Fired when the user clicks save.
                if (saveFileDialog.ShowDialog() == true)
                {
                    this.CurrentFile = new FileModel(saveFileDialog.FileName);
                }
            }

            await FileStorage.SaveFileAsync(this.CurrentFile.CompletePath, encodedInput);
        }
        #endregion
    }
}
