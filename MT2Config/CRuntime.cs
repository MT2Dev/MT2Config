using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MT2Config
{
    /// <summary>The C runtime functions the client parses metin2.cfg with ("C" locale).</summary>
    internal static class CRuntime
    {
        // isspace()
        public static readonly char[] Whitespace = { ' ', '\t', '\n', '\v', '\f', '\r' };

        static readonly Regex FloatPrefix = new Regex(
            @"^[ \t\n\v\f\r]*[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)([eE][+-]?[0-9]+)?", RegexOptions.CultureInvariant);

        // atoi(): optional whitespace and sign, then digits up to the first other character; 0 if there are none.
        public static int Atoi(string s)
        {
            int i = 0;
            while (i < s.Length && Array.IndexOf(Whitespace, s[i]) >= 0)
                i++;

            bool negative = false;
            if (i < s.Length && (s[i] == '+' || s[i] == '-'))
                negative = s[i++] == '-';

            long result = 0;
            for (; i < s.Length && s[i] >= '0' && s[i] <= '9'; i++)
                result = Math.Min(result * 10 + (s[i] - '0'), (long)int.MaxValue + 1);

            return (int)Math.Max(int.MinValue, Math.Min(int.MaxValue, negative ? -result : result));
        }

        // atof(): the longest number at the start of the string, always with '.' as decimal point; 0 if there is none.
        // (The old tool parsed with the Windows culture, which turned "0.5" into 5 on English systems.)
        public static double Atof(string s)
        {
            Match match = FloatPrefix.Match(s);
            double result;
            return match.Success && double.TryParse(match.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out result)
                ? result
                : 0.0;
        }
    }
}
