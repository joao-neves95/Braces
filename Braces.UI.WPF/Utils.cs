using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Braces.Core.Enums;

namespace Braces.UI.WPF
{
    public class Utils
    {
        public static void HandleException(Exception e)
        {
            Console.WriteLine("AN ERROR OCCURED:");
            Console.WriteLine(e);
        }

        public static void HandleExceptionWithDialog(Exception e, string errorMessage, string title = "ERROR:", MessageBoxImage icon = MessageBoxImage.Warning)
        {
            HandleException(e);
            MessageBox.Show(errorMessage, title, MessageBoxButton.OK, icon);
        }

        /// <summary>
        /// Converts a string to its enum type. To use it you must specify the enum type T. Can, potentialy, be use for multiple enum types.
        /// <para />
        /// Example:
        /// StringToKeyEnum<Key>("S", KeyType.Save)
        /// <para />
        /// Generic method type. Use with caution.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="keyType"></param>
        /// <returns></returns>
        public static T StringToKeyEnum<T>(string key) where T : Enum
        {
            try
            {
                T keyEnum = (T)Enum.Parse(typeof(T), key);
                return (T)Convert.ChangeType(keyEnum, typeof(T));
            }
            // Invalid key.
            catch (ArgumentException e)
            {
                HandleExceptionWithDialog(e, $"Invalid key configuration \"{ key }\"");
                return default(T);
            }
            catch (Exception e)
            {
                // Handle unknown exception.
                HandleExceptionWithDialog(e, $"Invalid key configuration \"{ key }\"");
                return default(T);
            }
        }
    }
}
