using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyLibrary
{
    public static class TextHelper
    {
        public static bool IsPalindrome(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            var clean = new string(text.ToLower().Where(char.IsLetterOrDigit).ToArray());
            return clean.SequenceEqual(clean.Reverse());
        }

        public static int CountSentences(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            return Regex.Matches(text, @"[.!?]+").Count;
        }

        public static string ReverseString(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return new string(text.Reverse().ToArray());
        }
    }
}