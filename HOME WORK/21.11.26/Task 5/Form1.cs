using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task1;

namespace Task1
{
    public partial class Form1 : Form
    {
        private TextBox sourceBox, destBox, outputBox;
        private Button srcBtn, dstBtn, startBtn;

        public Form1() { InitializeComponent(); }

        private void InitializeComponent()
        {
            Text = "Дубликаты + отчёт";
            ClientSize = new Size(760, 540);

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(10) };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var r1 = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill };
            r1.Controls.Add(new Label { Text = "Источник:", Width = 80, TextAlign = ContentAlignment.MiddleLeft });
            sourceBox = new TextBox { Width = 520, Margin = new Padding(0, 4, 8, 0) };
            srcBtn = new Button { Text = "...", Width = 40, Height = 26 };
            srcBtn.Click += delegate { PickFolder(sourceBox); };
            r1.Controls.Add(sourceBox); r1.Controls.Add(srcBtn);

            var r2 = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill };
            r2.Controls.Add(new Label { Text = "Приёмник:", Width = 80, TextAlign = ContentAlignment.MiddleLeft });
            destBox = new TextBox { Width = 520, Margin = new Padding(0, 4, 8, 0) };
            dstBtn = new Button { Text = "...", Width = 40, Height = 26 };
            dstBtn.Click += delegate { PickFolder(destBox); };
            r2.Controls.Add(destBox); r2.Controls.Add(dstBtn);

            var r3 = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill };
            startBtn = new Button { Text = "Начать", Width = 140, Height = 32 };
            startBtn.Click += StartBtn_Click;
            r3.Controls.Add(startBtn);

            outputBox = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill, ReadOnly = true, Font = new Font("Consolas", 10) };

            layout.Controls.Add(r1, 0, 0);
            layout.Controls.Add(r2, 0, 1);
            layout.Controls.Add(r3, 0, 2);
            layout.Controls.Add(outputBox, 0, 3);
            Controls.Add(layout);
        }

        private static void PickFolder(TextBox t)
        {
            using (var dlg = new FolderBrowserDialog())
                if (dlg.ShowDialog() == DialogResult.OK) t.Text = dlg.SelectedPath;
        }

        private async void StartBtn_Click(object sender, EventArgs e)
        {
            string src = sourceBox.Text, dst = destBox.Text;
            if (!Directory.Exists(src) || string.IsNullOrWhiteSpace(dst))
            { MessageBox.Show("Укажите существующий источник и приёмник"); return; }

            startBtn.Enabled = false;
            outputBox.Clear();
            var progress = new Progress<string>(delegate (string s) { outputBox.AppendText(s + "\r\n"); });
            try
            {
                var res = await Task.Run(() => FileProcessor.Process(src, dst, progress, CancellationToken.None));
                string report = BuildReport(res, src, dst);
                string reportPath = Path.Combine(dst, "report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt");
                await Task.Run(() => File.WriteAllText(reportPath, report, Encoding.UTF8));
                outputBox.AppendText("\r\nГотово. Всего: " + res.TotalFiles + ", перенесено: " + res.Moved +
                    ", дубликатов: " + res.Duplicates + ", ошибок: " + res.Errors.Count + ".\r\nОтчёт: " + reportPath + "\r\n");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
            finally { startBtn.Enabled = true; }
        }

        private static string BuildReport(ProcessResult r, string src, string dst)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Отчёт о переносе файлов");
            sb.AppendLine("Дата:         " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("Источник:     " + src);
            sb.AppendLine("Приёмник:     " + dst);
            sb.AppendLine();
            sb.AppendLine("Всего файлов: " + r.TotalFiles);
            sb.AppendLine("Перенесено:   " + r.Moved);
            sb.AppendLine("Дубликатов:   " + r.Duplicates);
            sb.AppendLine("Ошибок:       " + r.Errors.Count);
            sb.AppendLine();
            sb.AppendLine("Перенесённые оригиналы");
            foreach (var f in r.MovedFiles) sb.AppendLine("  " + f);
            sb.AppendLine();
            sb.AppendLine("Дубликаты (оставлены в источнике)");
            foreach (var f in r.DuplicateFiles) sb.AppendLine("  " + f);
            if (r.Errors.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Ошибки");
                foreach (var e in r.Errors) sb.AppendLine("  " + e);
            }
            return sb.ToString();
        }
    }
}