using System.Text;
using System.Text.RegularExpressions;

namespace Task1
{
    public class ReportOptions
    {
        public bool ShowSentences, ShowCharacters, ShowWords, ShowQuestions, ShowExclamations;
        public bool Any { get { return ShowSentences || ShowCharacters || ShowWords || ShowQuestions || ShowExclamations; } }
    }

    public class AnalysisResult
    {
        public int Sentences, Characters, Words, Questions, Exclamations;
        public string ToReport(ReportOptions o)
        {
            var sb = new StringBuilder();
            if (o.ShowSentences) sb.AppendLine("Количество предложений: " + Sentences);
            if (o.ShowCharacters) sb.AppendLine("Количество символов: " + Characters);
            if (o.ShowWords) sb.AppendLine("Количество слов: " + Words);
            if (o.ShowQuestions) sb.AppendLine("Количество вопросительных предложений: " + Questions);
            if (o.ShowExclamations) sb.AppendLine("Количество восклицательных предложений: " + Exclamations);
            return sb.ToString();
        }
    }

    public static class TextAnalyzer
    {
        private static readonly Regex SentenceRegex = new Regex(@"[^.!?\r\n]+[.!?]+|[^.!?\r\n]+$", RegexOptions.Compiled);
        private static readonly Regex WordRegex = new Regex(@"[\p{L}\p{N}]+(?:[-'][\p{L}\p{N}]+)*", RegexOptions.Compiled);

        public static AnalysisResult Analyze(string text, ReportOptions o)
        {
            var r = new AnalysisResult();
            if (string.IsNullOrEmpty(text)) return r;
            if (o.ShowCharacters) r.Characters = text.Length;
            if (o.ShowWords) r.Words = WordRegex.Matches(text).Count;
            if (o.ShowSentences || o.ShowQuestions || o.ShowExclamations)
            {
                foreach (Match m in SentenceRegex.Matches(text))
                {
                    var s = m.Value.Trim();
                    if (s.Length == 0) continue;
                    if (o.ShowSentences) r.Sentences++;
                    char last = s[s.Length - 1];
                    if (last == '?' && o.ShowQuestions) r.Questions++;
                    else if (last == '!' && o.ShowExclamations) r.Exclamations++;
                }
            }
            return r;
        }
    }
}