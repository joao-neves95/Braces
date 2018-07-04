using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Braces.UI
{
    public class Utils
    {
        public static void HandleException(Exception e)
        {
            // TODO: Implement HandleException().
        }

        public static void HandleExceptionWithDialog()
        {
            // TODO: HandleExceptionWithDialog() with a MessageBox. Pass to Braces.UI.
        }

        public static Key StringToKeyEnum(string key)
        {
            try
            {
                Key keyEnum = (Key)Enum.Parse(typeof(Key), key);
                return keyEnum;
            }
            // Invalid key.
            catch (ArgumentException e)
            {
                // Handle exception.
                Console.WriteLine(e);
                return Key.None;
            }
            catch (Exception e)
            {
                // Handle unknown exception.
                Console.WriteLine(e);
                return Key.None;
            }
        }
    }
}
