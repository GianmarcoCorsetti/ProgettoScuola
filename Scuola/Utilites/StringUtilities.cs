using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scuola.Utilites {
    internal static class StringUtilities {
        public static string Reverse(this string stringInput){
            char[] chars = stringInput.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
}
