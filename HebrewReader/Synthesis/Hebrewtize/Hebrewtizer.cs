namespace HebrewReader.Synthesis.Hebrewtize
{
    public static class Hebrewtizer
    {
        public static string Hebrewtize(string word)
        {
            var hebrewtizer = NumberHebrewtizer.NumberRegex.IsMatch(word)
                ? (IHebrewtizer)new NumberHebrewtizer()
                : new WordHebrewtizer();

            return hebrewtizer.Hebrewtize(word);
        }
    }
}
