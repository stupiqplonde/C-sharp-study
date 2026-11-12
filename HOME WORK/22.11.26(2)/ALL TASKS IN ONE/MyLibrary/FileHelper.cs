using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyLibrary
{
    public static class FileHelper
    {
        public static void CopyFile(string source, string dest) { File.Copy(source, dest, true); }

        public static void CopyDirectory(string source, string dest)
        {
            Directory.CreateDirectory(dest);
            foreach (var file in Directory.GetFiles(source))
            {
                string destFile = Path.Combine(dest, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }
            foreach (var dir in Directory.GetDirectories(source))
            {
                string destDir = Path.Combine(dest, Path.GetFileName(dir));
                CopyDirectory(dir, destDir);
            }
        }

        public static void DeleteFile(string path) { if (File.Exists(path)) File.Delete(path); }

        public static void DeleteFiles(List<string> paths)
        {
            foreach (var path in paths) DeleteFile(path);
        }

        public static void DeleteFilesByMask(string directory, string mask)
        {
            if (!Directory.Exists(directory)) return;
            foreach (var file in Directory.GetFiles(directory, mask))
            {
                File.Delete(file);
            }
        }

        public static void MoveFile(string source, string dest) { File.Move(source, dest); }

        public static int SearchWordInFile(string filePath, string word, string reportPath)
        {
            if (!File.Exists(filePath)) return 0;
            string text = File.ReadAllText(filePath);
            int count = CountOccurrences(text, word);

            File.WriteAllText(reportPath, $"Поиск слова \"{word}\" в файле: {filePath}\r\nНайдено совпадений: {count}");
            return count;
        }

        public static int SearchWordInFolder(string folderPath, string word, string reportPath)
        {
            if (!Directory.Exists(folderPath)) return 0;

            int totalCount = 0;
            StringBuilder report = new StringBuilder();
            report.AppendLine($"Поиск слова \"{word}\" в папке: {folderPath}\r\n");

            foreach (var file in Directory.GetFiles(folderPath, "*.txt"))
            {
                string text = File.ReadAllText(file);
                int count = CountOccurrences(text, word);
                if (count > 0)
                {
                    report.AppendLine($"Файл {Path.GetFileName(file)}: {count} совпадений");
                    totalCount += count;
                }
            }

            report.AppendLine($"\r\nВсего найдено: {totalCount}");
            File.WriteAllText(reportPath, report.ToString());
            return totalCount;
        }

        private static int CountOccurrences(string text, string word)
        {
            int count = 0, index = 0;
            while ((index = text.IndexOf(word, index, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                index += word.Length;
                count++;
            }
            return count;
        }
    }
}