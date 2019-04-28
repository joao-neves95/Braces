using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            this.RichTextBox = richTextBox;
            this.OverrideDefaultEditorKeyBindings();
            this.AddTextEditorEventHandlers();
            richTextBox.Document.Blocks.Add(new Paragraph());
            this.LineCount = 1;
            richTextBox.Focus();
            richTextBox.Document.Focus();
        }

        #region PROPERTIES

        public RichTextBox RichTextBox { get; }

        private int _lineCount;

        public int LineCount
        {
            get
            {
                return _lineCount;
            }
            private set {
                this.OnLineCountChange(_lineCount, value);
                _lineCount = value;
            }
        }

        public string CurrentIndentation { get; set; }

        #endregion

        #region PUBLIC METHODS

        public void AddLineToEndOfFile(string content)
        {
            richTextBox.Document.Blocks.Add( new Paragraph( new Run( content ) ) );
            this.AddLineNumber();
        }

        public void AddNewLineAfterCaretPosition( string content)
        {

            richTextBox.Document.Blocks.InsertAfter( richTextBox.CaretPosition.Paragraph, new Paragraph( new Run( content ) ) );
            this.AddLineNumber();
        }

        #endregion

        #region HANDLERS

        #region HANDLERS - Property Changed Handlers
        private void OnLineCountChange(int currLineCount, int newLineCount)
        {
            if (currLineCount == newLineCount)
                return;
            else if (newLineCount < currLineCount)
                this.RemoveLineNumber();
            else
                this.AddLineNumber();
        }
        #endregion

        #region HANDLERS - Global TextEditor Listeners

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Return)
            {
                // Set the current indentation based on the previuous paragraph.
                Paragraph currParagraph = richTextBox.CaretPosition.Paragraph;

                char[] currLineArr = new TextRange(currParagraph.ContentStart, currParagraph.ContentEnd).Text.ToCharArray();
                if (currLineArr == null)
                    return;

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
                caret.GetLineStartPosition(0).InsertTextInRun(this.CurrentIndentation);
                TextPointer moveTo = caret.Paragraph.ContentEnd;
                if (moveTo != null)
                    richTextBox.CaretPosition = moveTo;
            }
        }

        private void AddTextEditorEventHandlers()
        {
            richTextBox.TextChanged += (sender, args) => {
                this.LineCount = richTextBox.Document.Blocks.Count;
                if (lineNumersListBlock.Inlines.Count != this.LineCount)
                    this.InjectAllLineNumbers();
            };
        }

        #endregion

        #region HANDLERS - Specific TextEditor Command Handlers

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void Cut_Handler(object sender, KeyEventArgs e)
        {
            string currentSelection = richTextBox.Selection.Text;

            if (currentSelection == "" || currentSelection == null)
                // Delete the whole line.
                richTextBox.Document.Blocks.Remove(richTextBox.CaretPosition.Paragraph);
            else
                // Execute the default behaviour (delete current selection).
                richTextBox.Cut();
        }

        // TODO: Make a separate method for the caret positioning.
        private void MoveUp_Handler(object sender, ExecutedRoutedEventArgs e)
        {
            Paragraph currentParagraph = richTextBox.CaretPosition.Paragraph;
            Paragraph previousParagraph = currentParagraph.PreviousBlock as Paragraph;
            int currentPointerOffset = richTextBox.CaretPosition.GetOffsetToPosition( currentParagraph.ContentStart );
            richTextBox.Document.Blocks.Remove(currentParagraph);
            richTextBox.Document.Blocks.InsertBefore(previousParagraph, currentParagraph);
            richTextBox.CaretPosition = previousParagraph.PreviousBlock.ContentStart;
            richTextBox.CaretPosition = richTextBox.CaretPosition.GetPositionAtOffset( -currentPointerOffset - 1 );
        }

        private void MoveDown_Handler(object sender, ExecutedRoutedEventArgs e)
        {
            Paragraph currentParagraph = richTextBox.CaretPosition.Paragraph;
            Paragraph nextParagraph = currentParagraph.NextBlock as Paragraph;
            int currentPointerOffset = richTextBox.CaretPosition.GetOffsetToPosition( currentParagraph.ContentStart );
            richTextBox.Document.Blocks.Remove(currentParagraph);
            richTextBox.Document.Blocks.InsertAfter(nextParagraph, currentParagraph);
            richTextBox.CaretPosition = nextParagraph.NextBlock.ContentStart;
            richTextBox.CaretPosition = richTextBox.CaretPosition.GetPositionAtOffset( -currentPointerOffset - 1 );
        }

        private void Duplicate_Handler(object sender, ExecutedRoutedEventArgs e)
        {
            Paragraph currentParagraph = richTextBox.CaretPosition.Paragraph;
            TextRange currentParagraphContents = new TextRange(currentParagraph.ContentStart, currentParagraph.ContentEnd);
            int currentPointerOffset = richTextBox.CaretPosition.GetOffsetToPosition(currentParagraph.ContentStart);
            Paragraph newParagraph = new Paragraph(new Run(currentParagraphContents.Text));
            richTextBox.Document.Blocks.InsertAfter(currentParagraph, newParagraph);
            richTextBox.CaretPosition = currentParagraph.NextBlock.ContentStart;
            richTextBox.CaretPosition = richTextBox.CaretPosition.GetPositionAtOffset( -currentPointerOffset - 1 );
        }
        #endregion
        #endregion

        #region METHODS - LINE NUMBERS LIST

        public void AddLineNumber(int num = -1)
        {
            Label newLine = new Label();
            newLine.Padding = new Thickness(0);
            newLine.Margin = new Thickness(5, 1, 5, 0);

            if (num == -1)
                newLine.Content = (lineNumersListBlock.Inlines.Count + 1).ToString();
            else
                newLine.Content = (num).ToString();

            lineNumersListBlock.Inlines.Add( newLine );
        }

        public void RemoveLineNumber()
        {
            lineNumersListBlock.Inlines.Remove( lineNumersListBlock.Inlines.LastInline );
        }

        /// <summary>
        /// This clears and updates all line numbers.
        /// </summary>
        private void InjectAllLineNumbers()
        {
            lineNumersListBlock.Inlines.Clear();
            this.LineCount = richTextBox.Document.Blocks.Count;

            for (int i = 0; i < this.LineCount; ++i)
            {
                this.AddLineNumber(i);
            }
        }

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
