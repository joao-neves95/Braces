using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Braces.UI.UserControls
{
    /// <summary>
    /// Interaction logic for TextEditorControl.xaml
    /// </summary>
    public partial class TextEditorControl : UserControl
    {
        public TextEditorControl()
        {
            InitializeComponent();
            DataContext = this;
            this.OverrideDefaultEditorKeyBindings();
            richTextBox.Focus();
        }

        public int LineCount { get; set; }

        public string CurrentIndentation { get; set; }

        #region HANDLERS
        #region HANDLERS - Global Listeners
        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Return)
            {
                Paragraph currParagraph = richTextBox.CaretPosition.Paragraph;

                char[] currLineArr = new TextRange(currParagraph.ContentStart, currParagraph.ContentEnd).Text.ToCharArray();
                StringBuilder indent = new StringBuilder();
                for (uint i = 0; i < currLineArr.Length; ++i)
                {
                    if (currLineArr[i] != ' ')
                        break;

                    indent.Append(" ");
                }

                this.CurrentIndentation = indent.ToString();
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Indentation.
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                TextPointer caret = richTextBox.CaretPosition;
                caret.GetLineStartPosition(0).InsertTextInRun( this.CurrentIndentation );
                TextPointer moveTo = caret.Paragraph.ContentEnd;
                if (moveTo != null)
                    richTextBox.CaretPosition = moveTo;
            }
        }
        #endregion

        #region HANDLERS - Specific Command Handlers
        private void MoveLine_Handler(object sender, KeyEventArgs e)
        {
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Cut_Handler(object sender, KeyEventArgs e)
        {
            string currentSelection = richTextBox.Selection.Text;

            if (currentSelection == "" || currentSelection == null)
                // Delete whole line.
                richTextBox.Document.Blocks.Remove(richTextBox.CaretPosition.Paragraph);
            else
                // Execute the default behaviour.
                richTextBox.Cut();
        }
        #endregion
        #endregion

        private void OverrideDefaultEditorKeyBindings()
        {
            richTextBox.PreviewKeyDown += (sender, args) => {
                // Cut()
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && args.Key == Key.X)
                {
                    args.Handled = true;
                    this.Cut_Handler(sender, args);
                }
            };
        }
    }
}
