using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HebrewReader.Lexicons;

namespace HebrewReader.Synthesis.Hebrewtize
{
    internal class NumberHebrewtizer : IHebrewtizer
    {
        private static readonly Lexicon HebrewLexicon;

        private const string WordSpace = " ";

        private const string WordHeMinus = "מינוס";
        private const string WordHeMillion = "מיליון";
        private const string WordHeThousands = "אלפים";
        private const string WordHeThousand = "אלף";
        private const string WordHeTensOf = "עשרת";
        private const string WordHeAnd = "ו";

        private const string WordEnMinus = "Minooss";
        private const string WordEnMillion = "Mill yon,";
        private const string WordEnThousands = "Halafim,";
        private const string WordEnThousand = "Hellef,";
        private const string WordEnTensOf = "Hasseret";
        private const string WordEnAnd = "Ve ";


        private static readonly string[] WordsHeFemaleUnits = { "אפס", "אחת", "שתיים", "שלוש", "ארבע", "חמש", "שש", "שבע", "שמונה", "תשע", "עשר", "אחת עשרה", "שתים עשרה", "שלוש עשרה", "ארבע עשרה", "חמש עשרה", "שש עשרה", "שבע עשרה", "שמונה עשרה", "תשע עשרה" };
        private static readonly string[] WordsHeMaleUnits = { "", "אחד", "שניים", "שלושה", "ארבעה", "חמישה", "שישה", "שבעה", "שמונה", "תשעה", "עשרה", "אחד עשר", "שנים עשר", "שלושה עשר", "ארבעה עשר", "חמישה עשר", "שישה עשר", "שבעה עשר", "שמונה עשר", "תשעה עשר" };
        private static readonly string[] WordsHeTens = { "", "עשר", "עשרים", "שלושים", "ארבעים", "חמישים", "ששים", "שבעים", "שמונים", "תשעים" };
        private static readonly string[] WordsHeHundreds = { "", "מאה", "מאתיים", "שלוש מאות", "ארבע מאות", "חמש מאות", "שש מאות", "שבע מאות", "שמונה מאות", "תשע מאות" };
        private static readonly string[] WordsHeThousands = { "", "אלף", "אלפיים", "שלושת אלפים", "ארבעת אלפים", "חמשת אלפים", "ששת אלפים", "שבעת אלפים", "שמונת אלפים", "תשעת אלפים" };
        private static readonly string[] WordsHeMillions = { "", "", "שני", "שלושה", "ארבעה", "חמישה", "ששה", "שבעה", "שמונה", "תשעה" };

        private static readonly string[] WordsEnFemaleUnits = { "Heffhaes", "Ahat", "Shtuh yim", "Shalosh", "Arbuh", "Ha mesh", "Shesh", "Sheaeva", "Shmonee", "Tesha", "Hesser", "Ahat Esrae", "Shteh misray", "Shlosheasrae", "Arbaa Easrae", "Ha mesh Esrae", "Shesh Esrae", "Shvaa Esrae", "Shmona Esrae", "Tsha Esrae" };
        private static readonly string[] WordsEnMaleUnits = { "", "Aehad", "Shnaim", "Shlosha", "Arbaha", "Hamisha", "Shisha", "Shivha", "Shmona", "Tisha", "Asara", "Ahad Asar", "Shneim Asar", "Shlosha Asar", "Arbaha Asar", "Hamisha Asar", "Shisha Asar", "Shivaa Asar", "Shmona Asar", "Tishaa Asar" };
        private static readonly string[] WordsEnTens = { "", "Hesser", "Aesrim", "Shloshim", "Har bahim", "Hamishim", "Shishim", "Shiv yim", "Shmo nim", "Tish yim" };
        private static readonly string[] WordsEnHundreds = { "", "Mehuh", "Maatahim", "Shalosh mehot", "Arbuh mehot", "Ha mesh mehot", "Shesh mehot", "Shva mehot", "Shmonee mehot", "Tsha mehot" };
        private static readonly string[] WordsEnThousands = { "", "Heleff", "Alpaa yim", "Shloshet Halafim", "Arbaat Halafim", "Ha Meshet Halafim", "Sheshet Halafim", "Shvaat Halafim", "Shmonat Halafim", "Tishat Halafim" };
        private static readonly string[] WordsEnMillions = { "", "", "Shne", "Shlosha", "Arbuh Ha", "Hami shah", "Shisha", "Shiv ha", "Shmona", "Tisha" };

        static NumberHebrewtizer()
        {
             HebrewLexicon = new Lexicon("hebrew-numbers.pls");
        }

        public static readonly Regex NumberRegex = new Regex(@"-?\d+", RegexOptions.Compiled);
        private string HebrewtizeNumbersReplacer(Match match)
        {
            return match.Value.Length > 9 ? HebrewtizeDigits(match.Value) : HebrewtizeNumber(int.Parse(match.Value));
        }

        private string HebrewtizeDigits(string numberString, bool hebrew = false)
        {
            var output = new StringBuilder();

            foreach (var c in numberString.ToCharArray())
            {
                output.Append(hebrew ? WordsHeFemaleUnits[c - '0'] : WordsEnFemaleUnits[c - '0']);
            }

            return output.ToString().Trim();
        }

        private string HebrewtizeNumber(int number, bool hebrew = false)
        {
            var output = new StringBuilder();

            if (number < 0)
            {
                output.Append(hebrew ? WordHeMinus : WordEnMinus).Append(WordSpace);
                number = -number;
            }

            var remaining = number;
            var digits = new List<int>();

            while (remaining >= 1)
            {
                digits.Add(remaining%10);
                remaining = remaining/10;
            }

            if (number < 20)
            {
                output.Append(hebrew ? WordsHeFemaleUnits[number] : WordsEnFemaleUnits[number]);
            }
            else
            {
                var i = digits.Count;
                while (i > 0)
                {
                    var digitsHandled = 1;
                    switch (i)
                    {
                        case 4: //Special care for 4 digits number (few thousands)
                            output.Append(hebrew ? WordsHeThousands[digits[i - 1]] : WordsEnThousands[digits[i - 1]]).Append(WordSpace);
                            break;
                        case 7: //Special care for 7 digits number (few millions)
                            output.Append(hebrew ? WordsHeMillions[digits[i - 1]] : WordsEnMillions[digits[i - 1]]).Append(WordSpace)
                                .Append(hebrew ? WordHeMillion : WordEnMillion).Append(WordSpace);
                            break;
                        default: //handle triplets of hundreds/tens/units
                            digitsHandled = (i%3 == 2 ? 2 : 3);
                            
                            //START ConvertTriplet

                            var f100 = digitsHandled == 3 ? digits[i - 1] : 0;
                            var f10 = digitsHandled == 3 ? digits[i - 2] : digits[i - 1];
                            var f1 = digitsHandled == 3 ? digits[i - 3] : digits[i - 2];
                            output.Append(hebrew ? WordsHeHundreds[f100] : WordsEnHundreds[f100]);

                            if (f10 < 2) //Esre'e
                            {
                                if (f10 > 0 || f1 > 0) //atleast one digit
                                {
                                    var soFar = output.ToString();
                                    if (soFar != string.Empty && soFar != (hebrew ? WordHeMinus : WordEnMinus)) output.Append(WordSpace).Append(hebrew ? WordHeAnd : WordEnAnd);

                                    if (i <= 3) //dealing with the most right 2 digits-->female
                                    {
                                        output.Append(hebrew ? WordsHeFemaleUnits[f10*10 + f1] : WordsEnFemaleUnits[f10*10 + f1]);
                                    }
                                    else if (i >= 4 && i <= 6 && f100 == 0 && f10 * 10 + f1 == 10) //dealing with the middle of the number-->male
                                    {
                                        output.Append(hebrew ? WordHeTensOf : WordEnTensOf);
                                    }
                                    else
                                    {
                                        output.Append(hebrew ? WordsHeMaleUnits[f10*10 + f1] : WordsEnMaleUnits[f10*10 + f1]);
                                    }
                                }
                            }
                            else
                            {
                                output.Append(WordSpace).Append(hebrew ? WordsHeTens[f10] : WordsEnTens[f10]);
                                if (f1 > 0)
                                {
                                    if (i <= 3) //dealing with the most right 2 digits-->female
                                    {
                                        output.Append(hebrew ? WordHeAnd : WordEnAnd).Append(hebrew ? WordsHeFemaleUnits[f1] : WordsEnFemaleUnits[f1]);
                                    }
                                    else //dealing with the middle of the number-->male
                                    {
                                        output.Append(hebrew ? WordHeAnd : WordEnAnd).Append(hebrew ? WordsHeMaleUnits[f1] : WordsEnMaleUnits[f1]);
                                    }
                                }
                            }

                            //END ConvertTriplet

                            if (i > 6)
                            {
                                output.Append(WordSpace).Append(hebrew ? WordHeMillion : WordEnMillion).Append(WordSpace);
                            }
                            else if (i > 3 && (digits.Count > 3 ? digits[3] : 0) + (digits.Count > 4 ? digits[4] : 0) + (digits.Count > 5 ? digits[5] : 0) > 0) // Only if there are digits in the thousand section
                            {
                                if ((digits.Count > 4 ? digits[4] : 0) == 1 && (digits.Count > 3 ? digits[3] : 0) + (digits.Count > 5 ? digits[5] : 0) == 0)
                                {
                                    output.Append(WordSpace).Append(hebrew ? WordHeThousands : WordEnThousands).Append(WordSpace);
                                }
                                else
                                {
                                    output.Append(WordSpace).Append(hebrew ? WordHeThousand : WordEnThousand).Append(WordSpace);
                                }
                            }

                            break;
                    }
                    i -= digitsHandled;
                }
            }

            return output.ToString().Trim();
        }

        public string Hebrewtize(string input)
        {
            return NumberRegex.Replace(input, HebrewtizeNumbersReplacer);
        }
    }
}
