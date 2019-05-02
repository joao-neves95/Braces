using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Braces.Core.Enums;
using Newtonsoft.Json;

namespace Braces.Core
{
    public static class Utilities
    {
        #region METHODS

        public const string alphaNum = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public static int RandomInt( int min = 0, int max = 100 )
        {
            return new Random().Next( min, max );
        }

        public static string RandomAlphaNumeric( uint size = 5 )
        {
            StringBuilder result = new StringBuilder( (int)size );

            for (uint i = 0; i <= size; ++i)
            {
                result.Append( Utilities.alphaNum[Utilities.RandomInt( 0, 62 )] );
            }

            return result.ToString();
        }

        #endregion

        #region EXTENSIONS

        public static bool IsNull( this Object value )
        {
            return value == null;
        }

        public static string ToJSON( this object content )
        {
            return JsonConvert.SerializeObject( content, Formatting.Indented );
        }

        #endregion
    }
}
