using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace eShop.web.Helpers
{
    public static class StringExtensions
    {
        public static string ToOnlyAlphaNumericInput(this string input)
        {
            if (input == null)
            {
                return null;
            }
            return Regex.Replace(input, @"[^\w]", string.Empty);
        }
        public static string ToOnlyNormalTextInput(this string input)
        {
            if (input == null)
            {
                return null;
            }
            return Regex.Replace(input, @"[^\w\.@!? ,/:+()'´-]", string.Empty);
        }
    }
}