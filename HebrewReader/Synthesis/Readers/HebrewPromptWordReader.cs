using HebrewReader.Lexicons;
namespace HebrewReader.Synthesis.Readers
{
    internal class HebrewPromptWordReader : IHebrewPromptReader
    {
        //private static readonly Lexicon WordLexicon;

        static HebrewPromptWordReader()
        {
             //WordLexicon = new Lexicon("hebrew-words.pls");
        }

        public void ReadInto(HebrewPromptBuilder builder, string input)
        {
            //TODO: HebrewPromptWordReader
            builder.BaseAppendText(input);
        }
    }
}
