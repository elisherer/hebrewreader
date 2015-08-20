using System.Text.RegularExpressions;
using HebrewReader.Lexicons;
namespace HebrewReader.Synthesis.Readers
{
    internal class HebrewPromptWordReader : IHebrewPromptReader
    {
        private static readonly Lexicon WordLexicon;
        private static Regex HebrewWord = new Regex(@"[א-ת]", RegexOptions.Compiled);

        static HebrewPromptWordReader()
        {
             WordLexicon = new Lexicon("hebrew-words.pls");
        }

        public void ReadInto(HebrewPromptBuilder builder, string input)
        {
            //TODO: HebrewPromptWordReader
            if (HebrewWord.IsMatch(input))
                builder.AppendTextWithPronunciation(input, WordLexicon);
            else
                builder.BaseAppendText(input);
        }
    }
}
