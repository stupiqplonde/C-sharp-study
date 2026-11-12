using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace Task1
{
    public class ProcessResult
    {
        public int TotalFiles, Moved, Duplicates;
        public List<string> MovedFiles = new List<string>();
        public List<string> DuplicateFiles = new List<string>();
        public List<string> Errors = new List<string>();
    }

    public static class FileProcessor
    {
        public static ProcessResult Process(string source, string dest, IProgress<string> progress, CancellationToken token)
        {
            var result = new ProcessResult();
            if (!Directory.Exists(source)) throw new DirectoryNotFoundException(source);
            Directory.CreateDirectory(dest);

            var files = Directory.GetFiles(source);
            result.TotalFiles = files.Length;

            var sizeGroups = files.GroupBy(f => new FileInfo(f).Length).Where(g => g.Count() > 1);
            var duplicates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var sg in sizeGroups)
            {
                token.ThrowIfCancellationRequested();
                foreach (var hg in sg.GroupBy(ComputeHash))
                {
                    var list = hg.ToList();
                    if (list.Count < 2) continue;
                    for (int i = 1; i < list.Count; i++) duplicates.Add(list[i]);
                }
            }

            foreach (var file in files)
            {
                token.ThrowIfCancellationRequested();
                try
                {
                    if (duplicates.Contains(file))
                    {
                        result.Duplicates++;
                        result.DuplicateFiles.Add(file);
                        if (progress != null) progress.Report("[дубликат] " + Path.GetFileName(file));
                        continue;
                    }
                    string target = MakeUnique(Path.Combine(dest, Path.GetFileName(file)));
                    File.Move(file, target);
                    result.Moved++;
                    result.MovedFiles.Add(target);
                    if (progress != null) progress.Report("[перенос] " + Path.GetFileName(file) + " -> " + target);
                }
                catch (Exception ex)
                {
                    result.Errors.Add(file + ": " + ex.Message);
                    if (progress != null) progress.Report("[ошибка] " + Path.GetFileName(file) + ": " + ex.Message);
                }
            }
            return result;
        }

        private static string ComputeHash(string path)
        {
            using (var sha = SHA256.Create())
            using (var fs = File.OpenRead(path))
            {
                return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", "");
            }
        }

        private static string MakeUnique(string path)
        {
            if (!File.Exists(path)) return path;
            var dir = Path.GetDirectoryName(path);
            var name = Path.GetFileNameWithoutExtension(path);
            var ext = Path.GetExtension(path);
            for (int i = 1; ; i++)
            {
                var candidate = Path.Combine(dir, name + " (" + i + ")" + ext);
                if (!File.Exists(candidate)) return candidate;
            }
        }
    }
}