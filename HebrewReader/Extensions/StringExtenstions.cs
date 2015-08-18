using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HebrewReader.Extensions
{
    internal static class StringExtenstions
    {
        private static readonly Regex WordDivider = new Regex(@"\b[\s,\.-:;]*", RegexOptions.Compiled);

        public static IEnumerable<string> WordList(this string text)
        {
            return WordDivider.Split(text).Where(w => !string.IsNullOrWhiteSpace(w));
        }

        private static readonly Regex Tokenizer = new Regex(@"[\s,\.-:;]+|[^\s,\.-:;]+", RegexOptions.Compiled);

        public static IEnumerable<string> Tokenize(this string text)
        {
            return from Match m in Tokenizer.Matches(text) select m.Value;
        }
    }
}
