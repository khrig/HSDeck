using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS
{
    public static class StringExtensions {
        public static string Between(this string input, string start, string end) {
            int startIndex = input.IndexOf(start, StringComparison.InvariantCulture) + 1;
            return input.Substring(startIndex, input.IndexOf(end, StringComparison.InvariantCulture) - startIndex);
        }
    }
}
