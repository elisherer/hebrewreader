using System.Globalization;
using System.Speech.Synthesis;
using HebrewReader.Synthesis.Readers;

namespace HebrewReader.Synthesis
{
    public class HebrewPromptBuilder : PromptBuilder
    {
        #region Constructors

        public HebrewPromptBuilder() 
            : this(new CultureInfo("he-IL"))
        {
        }

        public HebrewPromptBuilder(CultureInfo culture) 
            : base(culture)
        {
        }

        public HebrewPromptBuilder(string textToSpeak)
            : this()
        {
            AppendText(textToSpeak);
        }

        #endregion

        internal void BaseAppendText(string textToSpeak)
        {
            base.AppendText(textToSpeak);
        }

        #region Methods

        //public new void ClearContent()

        public new void AppendText(string textToSpeak)
        {
            HebrewPromptReader.ReadInto(this, textToSpeak);
        }

        public new void AppendText(string textToSpeak, PromptRate rate)
        {
            HebrewPromptReader.ReadInto(this, textToSpeak);
        }

        public new void AppendText(string textToSpeak, PromptVolume volume)
        {
            StartStyle(new PromptStyle(volume));
            HebrewPromptReader.ReadInto(this, textToSpeak);
            EndStyle();
        }

        public new void AppendText(string textToSpeak, PromptEmphasis emphasis) 
        {
            StartStyle(new PromptStyle(emphasis));
            HebrewPromptReader.ReadInto(this, textToSpeak);
            EndStyle();
        }

        //public new void StartStyle (PromptStyle style)

        //public new void EndStyle ()

        //public new void StartVoice (VoiceInfo voice) 

        //public new void StartVoice (string name)

        //public new void StartVoice (VoiceGender gender)

        //public new void StartVoice (VoiceGender gender, VoiceAge age) 

        //public new void StartVoice (VoiceGender gender, VoiceAge age, int voiceAlternate) 

        //public new void StartVoice (CultureInfo culture) 

        //public new void EndVoice ()

        //public new void StartParagraph ()

        //public new void StartParagraph (CultureInfo culture)

        //public new void EndParagraph ()

        //public new void StartSentence ()

        //public new void StartSentence (CultureInfo culture) 

        //public new void EndSentence () 

        //public new void AppendTextWithHint (string textToSpeak, SayAs sayAs)

        //public new void AppendTextWithHint (string textToSpeak, string sayAs) 

        //public new void AppendTextWithPronunciation (string textToSpeak, string pronunciation)
        
        public void AppendTextWithPronunciation (string grapheme, Lexicons.Lexicon lexicon)
        {
            AppendTextWithPronunciation(grapheme, lexicon.Lexemes[grapheme]);
        }

        //public new void AppendTextWithAlias (string textToSpeak, string substitute) 

        //public new void AppendBreak () 

        //public new void AppendBreak (PromptBreak strength) 

        //public new void AppendBreak (TimeSpan duration)

        //public new void AppendAudio (string path) 

        //public new void AppendAudio (Uri audioFile)

        //public new void AppendAudio (Uri audioFile, string alternateText) 

        //public new void AppendBookmark (string bookmarkName) 

        //public new void AppendPromptBuilder (PromptBuilder promptBuilder) 

        //public new void AppendSsml (string path) 

        //public new void AppendSsml (Uri ssmlFile)

        //public new void AppendSsml (XmlReader ssmlFile)

        //public new void AppendSsmlMarkup (string ssmlMarkup)

        //public new string ToXml ()

        //public new void AddPromptDatabase (Uri promptDatabase) 

        //public new void AddPromptDatabase (Uri primaryDatabase, Uri deltaDatabase) 

        //public new void RemovePromptDatabase (Uri primaryDatabase, Uri deltaDatabase)

        #endregion

        #region Properties

        //public new bool IsEmpty

        //public new CultureInfo Culture 

        #endregion
    }
}
