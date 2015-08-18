using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace HebrewReader.Lexicons
{
    public class Lexicon
    {
        public readonly Dictionary<string, List<string>> Lexemes;

        public Lexicon(string embeddedLexiconFilename)
        {
            Lexemes = new Dictionary<string, List<string>>();

            var assembly = Assembly.GetAssembly(typeof(Lexicon));
            var resourceName = "HebrewReader.Lexicons." + embeddedLexiconFilename;

            var xmlDoc = new XmlDocument();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return;
                xmlDoc.Load(stream);
            }
            var lexemeNodes = xmlDoc.GetElementsByTagName("lexeme");

            foreach (XmlNode lexemeNode in lexemeNodes)
            {
                var graphemeNodes = lexemeNode.SelectNodes("./grapheme");
                var graphemes = new List<string>();
                if (graphemeNodes != null)
                {
                    graphemes.AddRange(from XmlNode graphemeNode in graphemeNodes select graphemeNode.InnerText);
                }
                var phonemeNode = lexemeNode.SelectSingleNode("./phoneme");
                var phoneme = phonemeNode != null ? phonemeNode.InnerText : string.Empty;

                Lexemes.Add(phoneme, graphemes);
            }

        }
    }
}
