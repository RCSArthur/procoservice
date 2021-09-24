using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace form_procoservice
{
    internal static class StringExt
    {
        public static string Strip(this string str)
        {
            var sb = new StringBuilder(str.Length);
            foreach (var chr in str) if (char.IsDigit(chr)) sb.Append(chr);
            return sb.ToString();
        }
    }
}
