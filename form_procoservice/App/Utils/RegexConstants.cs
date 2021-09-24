using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace form_procoservice
{
    internal static class RegexConstants
    {
        public static readonly Regex Numeros = new("[^0-9]+$");
    }
}
