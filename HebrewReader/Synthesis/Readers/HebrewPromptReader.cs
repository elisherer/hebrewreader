using System.Linq;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace HebrewReader.Synthesis.Readers
{
    internal static class HebrewPromptReader
    {
        private static readonly Regex Token = new Regex(@"[\s,\.:;]+", RegexOptions.Compiled);
        private static readonly Regex Tokenizer = new Regex(@"[\s,\.:;]+|[^\s,\.:;]+", RegexOptions.Compiled);
        private static readonly Regex NumberRegex = new Regex(@"^-?\d+$", RegexOptions.Compiled);
        private static readonly HebrewPromptNumberReader NumberReader = new HebrewPromptNumberReader();
        private static readonly HebrewPromptWordReader WordReader = new HebrewPromptWordReader();

        public static void ReadInto(HebrewPromptBuilder builder, string textToSpeak)
        {
            foreach (var word in from Match m in Tokenizer.Matches(textToSpeak) select m.Value)
            {
                if (Token.IsMatch(word))
                {
                    builder.BaseAppendText(word);
                }
                else
                {
                    var reader = NumberRegex.IsMatch(word)
                        ? (IHebrewPromptReader)NumberReader
                        : (IHebrewPromptReader)WordReader;
                    reader.ReadInto(builder, word);
                }
            }
        }
    }
}
