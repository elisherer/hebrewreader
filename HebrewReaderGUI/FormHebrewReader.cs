using System;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;
using HebrewReader;
using HebrewReader.Synthesis;

namespace HebrewReaderGUI
{
    public partial class FormHebrewReader : Form
    {
        private readonly SpeechSynthesizer _speechSynthesizer = new SpeechSynthesizer();
        private readonly HebrewSpeechSynthesizer _hebrewSpeechSynthesizer = new HebrewSpeechSynthesizer();

        public FormHebrewReader()
        {
            InitializeComponent();

            _speechSynthesizer.SpeakStarted += _speechSynthesizer_SpeakStarted;
            _speechSynthesizer.SpeakCompleted += _speechSynthesizer_SpeakCompleted;

            _hebrewSpeechSynthesizer.SpeakStarted += _hebrewSpeechSynthesizer_SpeakStarted;
            _hebrewSpeechSynthesizer.SpeakCompleted += _hebrewSpeechSynthesizer_SpeakCompleted;
        }

        void _speechSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            buttonStopEnglish.Enabled = false;
        }

        void _speechSynthesizer_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
            buttonStopEnglish.Enabled = true;
        }

        void _hebrewSpeechSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            buttonStopHebrew.Enabled = false;
        }

        void _hebrewSpeechSynthesizer_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
            buttonStopHebrew.Enabled = true;
        }


        private void buttonSpeakHebrew_Click(object sender, EventArgs e)
        {
            _hebrewSpeechSynthesizer.SelectVoice(comboBox1.SelectedItem.ToString());
            _hebrewSpeechSynthesizer.SpeakAsync(textBoxHebrew.Text);
        }

        private void buttonSpeakEnglish_Click(object sender, EventArgs e)
        {
            _speechSynthesizer.SelectVoice(comboBox1.SelectedItem.ToString());
            _speechSynthesizer.SpeakAsync(textBoxEnglish.Text);

        }

        private void FormHebrewReader_Load(object sender, EventArgs e)
        {
            var voices = _hebrewSpeechSynthesizer.GetInstalledVoices();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(voices.Where(voice => voice.Enabled).Select(voice => voice.VoiceInfo.Name).Cast<object>().ToArray());
            if (comboBox1.Items.Count == 0)
            {
                buttonSpeakEnglish.Enabled = false;
                buttonSpeakHebrew.Enabled = false;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void buttonStopEnglish_Click(object sender, EventArgs e)
        {
            _speechSynthesizer.SpeakAsyncCancelAll();
        }

        private void buttonStopHebrew_Click(object sender, EventArgs e)
        {
            _hebrewSpeechSynthesizer.SpeakAsyncCancelAll();
        }

        private static string[] Names = { "Hamir", "Helly", "Or Hud", "Yuh Gale", "Herez", "Udi" };
        private void buttonQueue_Click(object sender, EventArgs e)
        {
            var queueNum = (new Random()).Next(100);
            var text = "Mispar " + queueNum + " Lay " + Names[(new Random()).Next(Names.Length)];

            _hebrewSpeechSynthesizer.SelectVoice(comboBox1.SelectedItem.ToString());
            _hebrewSpeechSynthesizer.SpeakAsync(text);
        }

    }
}
