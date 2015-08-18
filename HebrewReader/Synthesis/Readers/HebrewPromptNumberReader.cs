using System.Collections.Generic;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using HebrewReader.Lexicons;

namespace HebrewReader.Synthesis.Readers
{
    internal class HebrewPromptNumberReader : IHebrewPromptReader
    {
        private static readonly Lexicon HebrewLexicon;

        private const int MaxLengthToReadNumber = 9;
        private const string WordSpace = " ";

        private const string WordHeMinus = "מינוס";
        private const string WordHeMillion = "מיליון";
        private const string WordHeThousands = "אלפים";
        private const string WordHeThousand = "אלף";
        private const string WordHeTensOf = "עשרת";
        private const string WordHeAnd = "ו";

        private static readonly string[] WordsHeFemaleUnits = { "אפס", "אחת", "שתיים", "שלוש", "ארבע", "חמש", "שש", "שבע", "שמונה", "תשע", "עשר", "אחת עשרה", "שתים עשרה", "שלוש עשרה", "ארבע עשרה", "חמש עשרה", "שש עשרה", "שבע עשרה", "שמונה עשרה", "תשע עשרה" };
        private static readonly string[] WordsHeMaleUnits = { null, "אחד", "שניים", "שלושה", "ארבעה", "חמישה", "שישה", "שבעה", "שמונה", "תשעה", "עשרה", "אחד עשר", "שנים עשר", "שלושה עשר", "ארבעה עשר", "חמישה עשר", "שישה עשר", "שבעה עשר", "שמונה עשר", "תשעה עשר" };
        private static readonly string[] WordsHeTens = { null, "עשר", "עשרים", "שלושים", "ארבעים", "חמישים", "ששים", "שבעים", "שמונים", "תשעים" };
        private static readonly string[] WordsHeHundreds = { null, "מאה", "מאתיים", "שלוש מאות", "ארבע מאות", "חמש מאות", "שש מאות", "שבע מאות", "שמונה מאות", "תשע מאות" };
        private static readonly string[] WordsHeThousands = { null, "אלף", "אלפיים", "שלושת אלפים", "ארבעת אלפים", "חמשת אלפים", "ששת אלפים", "שבעת אלפים", "שמונת אלפים", "תשעת אלפים" };
        private static readonly string[] WordsHeMaleCount = { null, null, "שני", "שלושה", "ארבעה", "חמישה", "ששה", "שבעה", "שמונָה", "תשעה" };

        static HebrewPromptNumberReader()
        {
             HebrewLexicon = new Lexicon("hebrew-numbers.pls");
        }

        private void ReadDigitsInto(HebrewPromptBuilder builder, string numberString)
        {
            foreach (var c in numberString.ToCharArray())
            {
                builder.AppendTextWithPronunciation(WordsHeFemaleUnits[c - '0'], HebrewLexicon);
            }
        }

        private void ReadNumberInto(HebrewPromptBuilder builder, int number)
        {
            if (number < 0)
            {
                builder.AppendTextWithPronunciation(WordHeMinus, HebrewLexicon);
                number = -number;
            }

            var remaining = number;
            var digits = new List<int>();

            while (remaining >= 1)
            {
                digits.Add(remaining % 10); //i.e for 1234: digits = [4,3,2,1]
                remaining = remaining/10;
            }

            if (number < 20)
            {
                builder.AppendTextWithPronunciation(WordsHeFemaleUnits[number], HebrewLexicon);
            }
            else
            {
                var i = digits.Count;
                while (i > 0)
                {
                    var digitsHandled = 1;
                    switch (i)
                    {
                        case 4: //1,xxx-9,xxx
                            builder.AppendTextWithPronunciation(WordsHeThousands[digits[i - 1]], HebrewLexicon);
                            break;
                        case 7: //1,xxx,xxx-9,xxx,xxx
                            if (digits[i-1] > 1)
                                builder.AppendTextWithPronunciation(WordsHeMaleCount[digits[i - 1]], HebrewLexicon);
                            builder.AppendTextWithPronunciation(WordHeMillion, HebrewLexicon);
                            break;
                        default: //handle triplets of hundreds/tens/units
                            digitsHandled = (i % 3 == 2 ? 2 : 3);
                            
                            var f100 = digitsHandled == 3 ? digits[i - 1] : 0;
                            var f10 = digitsHandled == 3 ? digits[i - 2] : digits[i - 1];
                            var f1 = digitsHandled == 3 ? digits[i - 3] : digits[i - 2];
                            var f11 = f10 * 10 + f1;

                            if (f100 > 0) //1xx-9xx
                            {
                                builder.AppendTextWithPronunciation(WordsHeHundreds[f100], HebrewLexicon);
                            }

                            if (f11 > 0) //not 00
                            {
                                if (f10 < 2) //0x-1x
                                {
                                    if ((i < digits.Count && i < 4) || f100 > 0)
                                        builder.AppendTextWithPronunciation(WordHeAnd, HebrewLexicon); // And

                                    if (i < 4) //the right most digits are female
                                    {
                                        builder.AppendTextWithPronunciation(WordsHeFemaleUnits[f11], HebrewLexicon);
                                    }
                                    else if (i < 7 && f100 == 0 && f11 == 10) //010,xxx
                                    {
                                        builder.AppendTextWithPronunciation(WordHeTensOf, HebrewLexicon);
                                    }
                                    else //anywhere else is male
                                    {
                                        builder.AppendTextWithPronunciation(WordsHeMaleUnits[f11], HebrewLexicon);
                                    }
                                }
                                else //2x-9x
                                {
                                    builder.AppendTextWithPronunciation(WordsHeTens[f10], HebrewLexicon); // X0

                                    if (f1 > 0) //1-9
                                    {
                                        builder.AppendTextWithPronunciation(WordHeAnd, HebrewLexicon); // And

                                        if (i < 4) //the right most digits are female
                                        {
                                            builder.AppendTextWithPronunciation(WordsHeFemaleUnits[f1], HebrewLexicon);
                                        }
                                        else //anywhere else is male
                                        {
                                            builder.AppendTextWithPronunciation(WordsHeMaleUnits[f1], HebrewLexicon);
                                        }
                                    }
                                }
                            }

                            if (i > 3 && f1 + f10 + f100 > 0)
                            {
                                if (i < 7) //001,000-999,000
                                {
                                    if (f1 == 0 && f10 == 1 && f100 == 0) // 10,000
                                    {
                                        builder.AppendTextWithPronunciation(WordHeThousands, HebrewLexicon);
                                    }
                                    else
                                    {
                                        builder.AppendTextWithPronunciation(WordHeThousand, HebrewLexicon);
                                    }
                                }
                                else if (i <= MaxLengthToReadNumber) //001,000,000-999,000,000
                                {
                                    builder.AppendTextWithPronunciation(WordHeMillion, HebrewLexicon);
                                }
                            }
                            break;
                    }
                    i -= digitsHandled;
                }
            }
        }

        public void ReadInto(HebrewPromptBuilder builder, string input)
        {
            if (input.Length > MaxLengthToReadNumber || input.StartsWith("0"))
            {
                ReadDigitsInto(builder, input);
            }
            else
            {
                ReadNumberInto(builder, int.Parse(input));
            }
        }
    }
}
