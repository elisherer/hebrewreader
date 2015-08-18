using System;
using System.Speech.Synthesis;

namespace HebrewReader.Synthesis
{
    public class HebrewPrompt : Prompt
    {
        public HebrewPrompt(string textToSpeak) 
            : base((new HebrewPromptBuilder(textToSpeak)))
        {
        }

        public HebrewPrompt(PromptBuilder promptBuilder) 
            : base(promptBuilder)
        {
            throw new ArgumentException("HebrewPrompt accepts only HebrewPromptBuilder", "promptBuilder");
        }

        public HebrewPrompt(HebrewPromptBuilder promptBuilder) 
            : base(promptBuilder)
        {
        }

        public HebrewPrompt(string textToSpeak, SynthesisTextFormat media)
            : base(textToSpeak, media)
        {
            if (media == SynthesisTextFormat.Text)
                throw new ArgumentException("Please use constructor with one textToSpeak parameter instead (This is for Text media format)", "media");
        }

    }
}
