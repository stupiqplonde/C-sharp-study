using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task4_SearchInFile
{
    public class MainForm : Form
    {
        private readonly TextBox pathBox = new() { Width = 390 };
        private readonly TextBox wordBox = new() { Width = 180 };
        private readonly Button browseButton = new() { Text = "Обзор..." };
        private readonly Button searchButton = new() { Text = "Найти" };
        private readonly Label resultLabel = new() { AutoSize = true, Text = "Укажите файл и слово." };

        public MainForm()
        {
            Text = "Задание 4 — Поиск слова в файле";
            Width = 680; Height = 240; MinimumSize = new System.Drawing.Size(600, 200);
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(15), ColumnCount = 3, RowCount = 4 };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.Controls.Add(new Label { Text = "Путь к файлу:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
            panel.Controls.Add(pathBox, 1, 0);
            panel.Controls.Add(browseButton, 2, 0);
            panel.Controls.Add(new Label { Text = "Искомое слово:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
            panel.Controls.Add(wordBox, 1, 1);
            panel.Controls.Add(searchButton, 1, 2);
            panel.Controls.Add(resultLabel, 1, 3);
            Controls.Add(panel);
            browseButton.Click += (_, _) =>
            {
                using var dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK) pathBox.Text = dialog.FileName;
            };
            searchButton.Click += async (_, _) => await SearchAsync();
        }

        private async Task SearchAsync()
        {
            string path = pathBox.Text.Trim();
            string word = wordBox.Text;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                MessageBox.Show("Выберите существующий файл.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(word))
            {
                MessageBox.Show("Введите слово для поиска.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            searchButton.Enabled = false;
            resultLabel.Text = "Идёт поиск...";
            try
            {
                long count = await Task.Run(async () =>
                {
                    string content = await File.ReadAllTextAsync(path);
                    long total = 0;
                    int position = 0;
                    while ((position = content.IndexOf(word, position, StringComparison.OrdinalIgnoreCase)) >= 0)
                    {
                        total++;
                        position += word.Length;
                    }
                    return total;
                });
                resultLabel.Text = $"Количество вхождений «{word}»: {count}";
            }
            catch (Exception ex) { resultLabel.Text = "Ошибка: " + ex.Message; }
            finally { searchButton.Enabled = true; }
        }
    }
}
