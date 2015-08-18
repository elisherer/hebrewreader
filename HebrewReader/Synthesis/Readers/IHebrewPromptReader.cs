using System.Speech.Synthesis;
namespace HebrewReader.Synthesis.Readers
{
    public interface IHebrewPromptReader
    {
        void ReadInto(HebrewPromptBuilder builder, string input);
    }
}
