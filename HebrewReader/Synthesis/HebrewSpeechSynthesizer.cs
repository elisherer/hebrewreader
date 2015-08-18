using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Speech.Synthesis;

namespace HebrewReader.Synthesis
{
    public sealed class HebrewSpeechSynthesizer : IDisposable
    {
        private readonly SpeechSynthesizer _speechSynthesizer;

        public HebrewSpeechSynthesizer(string voiceName = null)
        {
            _speechSynthesizer = new SpeechSynthesizer();
            //_speechSynthesizer.AddLexicon(new Uri("", UriKind.Relative), "application/pls+xml"); //This doesn't work!
            _speechSynthesizer.SpeakStarted += _speechSynthesizer_SpeakStarted;
            _speechSynthesizer.SpeakCompleted += _speechSynthesizer_SpeakCompleted;
            if (voiceName != null)
            {
                _speechSynthesizer.SelectVoice(voiceName);
            }
        }

        public void Dispose()
        {
            _speechSynthesizer.Dispose();
        }

        ~HebrewSpeechSynthesizer()
        {
            _speechSynthesizer.Dispose();
        }


        #region Events

        private void RaiseEventOnUIThread(Delegate theEvent, params object[] args)
        {
            foreach (var @delegate in theEvent.GetInvocationList())
            {
                var syncer = @delegate.Target as ISynchronizeInvoke;
                if (syncer == null)
                {
                    @delegate.DynamicInvoke(args);
                }
                else
                {
                    syncer.BeginInvoke(@delegate, args);  // cleanup omitted
                }
            }
        }

        public event EventHandler<SpeakStartedEventArgs> SpeakStarted;
        public event EventHandler<SpeakCompletedEventArgs> SpeakCompleted;

        void _speechSynthesizer_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
            if (SpeakStarted != null)
            {
                RaiseEventOnUIThread(SpeakStarted, sender, e);
            }
        }

        void _speechSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if (SpeakCompleted != null)
            {
                RaiseEventOnUIThread(SpeakCompleted, sender, e);
            }
        }

        #endregion

        public ReadOnlyCollection<InstalledVoice> GetInstalledVoices()
        {
            return _speechSynthesizer.GetInstalledVoices();
        }

        public void SelectVoice(string name)
        {
            _speechSynthesizer.SelectVoice(name);
        }

        #region Sync

        public void Speak(HebrewPrompt prompt)
        {
            _speechSynthesizer.Speak(prompt);
        }
        public void Speak(HebrewPromptBuilder promptBuilder)
        {
            _speechSynthesizer.Speak(promptBuilder);
        }
        public void Speak(string textToSpeak)
        {
            Speak(new HebrewPromptBuilder(textToSpeak));
        }
        public void SpeakSsml(string ssmlString)
        {
            _speechSynthesizer.SpeakSsml(ssmlString);
        }

        #endregion


        #region Async

        public void SpeakAsync(HebrewPrompt prompt)
        {
            _speechSynthesizer.SpeakAsync(prompt);
        }
        public Prompt SpeakAsync(HebrewPromptBuilder promptBuilder)
        {
            return _speechSynthesizer.SpeakAsync(promptBuilder);
        }
        public Prompt SpeakAsync(string textToSpeak)
        {
            return SpeakAsync(new HebrewPromptBuilder(textToSpeak));
        }
        public Prompt SpeakSsmlAsync(string ssmlString)
        {
            return _speechSynthesizer.SpeakSsmlAsync(ssmlString);
        }
        
        public void SpeakAsyncCancel(Prompt prompt)
        {
            _speechSynthesizer.SpeakAsyncCancel(prompt);
        }
        public void SpeakAsyncCancelAll()
        {
            _speechSynthesizer.SpeakAsyncCancelAll();
        }

        #endregion

    }
}
