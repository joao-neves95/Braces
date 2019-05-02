using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Braces.UI.WPF.UserControls
{
    public static class CustomCommands
    {
        public static RoutedUICommand Duplicate = new RoutedUICommand( "Duplicate Line", "Duplicate", typeof( TextEditorControl ) );
    }
}
