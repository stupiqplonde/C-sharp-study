using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task5_SearchInDirectory
{
    public class MainForm : Form
    {
        private readonly TextBox pathBox = new() { Width = 390 };
        private readonly TextBox wordBox = new() { Width = 180 };
        private readonly Button browseButton = new() { Text = "Обзор..." };
        private readonly Button searchButton = new() { Text = "Искать во всех файлах" };
        private readonly TextBox reportBox = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, WordWrap = false, Dock = DockStyle.Fill };

        public MainForm()
        {
            Text = "Задание 5 — Поиск в директории";
            Width = 850; Height = 560; MinimumSize = new System.Drawing.Size(650, 400);
            var top = new TableLayoutPanel { Dock = DockStyle.Top, Height = 105, Padding = new Padding(12), ColumnCount = 3, RowCount = 3 };
            top.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            top.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            top.Controls.Add(new Label { Text = "Папка:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
            top.Controls.Add(pathBox, 1, 0);
            top.Controls.Add(browseButton, 2, 0);
            top.Controls.Add(new Label { Text = "Слово:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
            top.Controls.Add(wordBox, 1, 1);
            top.Controls.Add(searchButton, 1, 2);
            Controls.Add(reportBox);
            Controls.Add(top);
            browseButton.Click += (_, _) =>
            {
                using var dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK) pathBox.Text = dialog.SelectedPath;
            };
            searchButton.Click += async (_, _) => await SearchAsync();
        }

        private async Task SearchAsync()
        {
            string directory = pathBox.Text.Trim();
            string word = wordBox.Text;
            if (!Directory.Exists(directory))
            {
                MessageBox.Show("Выберите существующую папку.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(word))
            {
                MessageBox.Show("Введите слово для поиска.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            searchButton.Enabled = false;
            reportBox.Clear();
            reportBox.Text = "Выполняется поиск во всех файлах и поддиректориях...";
            try
            {
                string report = await Task.Run(() =>
                {
                    var sb = new StringBuilder();
                    int filesWithMatches = 0;
                    long totalMatches = 0;
                    IEnumerable<string> files = EnumerateFilesSafely(directory);

                    foreach (string file in files)
                    {
                        try
                        {
                            string content = File.ReadAllText(file);
                            long count = CountOccurrences(content, word);
                            if (count > 0)
                            {
                                filesWithMatches++;
                                totalMatches += count;
                                sb.AppendLine($"Название файла: {Path.GetFileName(file)}");
                                sb.AppendLine($"Путь к файлу: {file}");
                                sb.AppendLine($"Количество вхождений слова: {count}");
                                sb.AppendLine(new string('-', 65));
                            }
                        }
                        catch (IOException) { }
                        catch (UnauthorizedAccessException) { }
                    }

                    if (filesWithMatches == 0)
                        sb.AppendLine("Совпадений не найдено.");
                    sb.AppendLine();
                    sb.AppendLine($"Файлов с совпадениями: {filesWithMatches}");
                    sb.AppendLine($"Всего вхождений: {totalMatches}");
                    return sb.ToString();
                });
                reportBox.Text = report;
            }
            catch (Exception ex) { reportBox.Text = "Ошибка: " + ex.Message; }
            finally { searchButton.Enabled = true; }
        }

        private static IEnumerable<string> EnumerateFilesSafely(string root)
        {
            var pending = new Stack<string>();
            pending.Push(root);
            while (pending.Count > 0)
            {
                string current = pending.Pop();
                string[] files = Array.Empty<string>();
                string[] dirs = Array.Empty<string>();
                try { files = Directory.GetFiles(current); } catch { }
                try { dirs = Directory.GetDirectories(current); } catch { }
                foreach (string file in files) yield return file;
                foreach (string dir in dirs) pending.Push(dir);
            }
        }

        private static long CountOccurrences(string content, string word)
        {
            long count = 0;
            int position = 0;
            while ((position = content.IndexOf(word, position, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                count++;
                position += word.Length;
            }
            return count;
        }
    }
}
