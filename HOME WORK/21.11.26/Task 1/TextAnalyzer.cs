using System.Text;
using System.Text.RegularExpressions;

namespace Task1
{
    public class AnalysisResult
    {
        public int Sentences, Characters, Words, Questions, Exclamations;
        public string ToReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Количество предложений: " + Sentences);
            sb.AppendLine("Количество символов: " + Characters);
            sb.AppendLine("Количество слов: " + Words);
            sb.AppendLine("Количество вопросительных предложений: " + Questions);
            sb.AppendLine("Количество восклицательных предложений: " + Exclamations);
            return sb.ToString();
        }
    }

    public static class TextAnalyzer
    {
        private static readonly Regex SentenceRegex = new Regex(@"[^.!?\r\n]+[.!?]+|[^.!?\r\n]+$", RegexOptions.Compiled);
        private static readonly Regex WordRegex = new Regex(@"[\p{L}\p{N}]+(?:[-'][\p{L}\p{N}]+)*", RegexOptions.Compiled);

        public static AnalysisResult Analyze(string text)
        {
            var r = new AnalysisResult();
            if (string.IsNullOrEmpty(text)) return r;
            r.Characters = text.Length;
            r.Words = WordRegex.Matches(text).Count;
            foreach (Match m in SentenceRegex.Matches(text))
            {
                var s = m.Value.Trim();
                if (s.Length == 0) continue;
                r.Sentences++;
                char last = s[s.Length - 1];
                if (last == '?') r.Questions++;
                else if (last == '!') r.Exclamations++;
            }
            return r;
        }
    }
}