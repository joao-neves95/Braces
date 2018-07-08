﻿using System;
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
            this.OverrideDefaultEditorKeyBindings();
            this.AddTextEditorEventHandlers();
            richTextBox.Document.Blocks.Add(new Paragraph());
            this.LineCount = 1;
            richTextBox.Focus();
            richTextBox.Document.Focus();
        }

        #region PROPERTIES
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
                // Set the indentation based on the previuous paragraph.
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
        #endregion

        #region HANDLERS - Specific TextEditor Command Handlers
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
                // Delete the whole line.
                richTextBox.Document.Blocks.Remove(richTextBox.CaretPosition.Paragraph);
            else
                // Execute the default behaviour (delete current selection).
                richTextBox.Cut();
        }
        private void OnTextEditorScroll(object sender, ScrollEventArgs e)
        {
            lineNumbersScroller.ScrollToVerticalOffset( e.NewValue );
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lineNumbersScroller.ScrollToVerticalOffset( richTextBox.VerticalOffset );
        }
        #endregion
        #endregion

        #region METHODS - LINE NUMBERS LIST
        private void AddLineNumber(int num = -1)
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

        private void RemoveLineNumber()
        {
            lineNumersListBlock.Inlines.Remove(lineNumersListBlock.Inlines.LastInline);
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

        private void AddTextEditorEventHandlers()
        {
            richTextBox.Document.Blocks.Count();
            richTextBox.TextChanged += (sender, args) => {
                this.LineCount = richTextBox.Document.Blocks.Count;
            };
        }

    }
}
