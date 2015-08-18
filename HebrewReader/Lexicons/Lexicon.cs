using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace HebrewReader.Lexicons
{
    public class Lexicon
    {
        public readonly Dictionary<string, string> Lexemes;

        public Lexicon(string embeddedLexiconFilename)
        {
            Lexemes = new Dictionary<string, string>();

            var assembly = Assembly.GetAssembly(typeof(Lexicon));
            var resourceName = "HebrewReader.Lexicons." + embeddedLexiconFilename;

            var xmlDoc = new XmlDocument();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return;
                xmlDoc.Load(stream);
            }

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("ns", "http://www.w3.org/2005/01/pronunciation-lexicon");

            var lexemeNodes = xmlDoc.GetElementsByTagName("lexeme");

            foreach (XmlNode lexemeNode in lexemeNodes)
            {
                var graphemeNodes = lexemeNode.SelectNodes("ns:grapheme", nsmgr);
                var graphemes = new List<string>();
                if (graphemeNodes != null)
                {
                    graphemes.AddRange(from XmlNode graphemeNode in graphemeNodes select graphemeNode.InnerText);
                }
                var phonemeNode = lexemeNode.SelectSingleNode("ns:phoneme", nsmgr);
                var phoneme = phonemeNode != null ? phonemeNode.InnerText : string.Empty;

                foreach (var grapheme in graphemes)
                {
                    if (!Lexemes.ContainsKey(grapheme)) Lexemes.Add(grapheme, phoneme);
                }
            }

        }
    }
}
