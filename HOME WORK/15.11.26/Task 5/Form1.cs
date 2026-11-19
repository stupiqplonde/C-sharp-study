using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtDir.Text = dlg.SelectedPath;
            }
        }

        private async void btnSearchDir_Click(object sender, EventArgs e)
        {
            string dir = txtDir.Text;
            string word = txtWord.Text;

            if (!Directory.Exists(dir))
            {
                MessageBox.Show("Папки нет");
                return;
            }

            btnSearchDir.Enabled = false;
            rtbReport.Clear();
            rtbReport.AppendText("поиск\n");

            var results = await Task.Run(() => SearchInDirectory(dir, word));

            rtbReport.Clear();
            foreach (var r in results)
            {
                rtbReport.AppendText($"Название файла: {r.fileName}\n");
                rtbReport.AppendText($"Путь к файлу: {r.fullPath}\n");
                rtbReport.AppendText($"кол-во вхождений слова: {r.count}\n");
                rtbReport.AppendText(new string('-', 40) + "\n");
            }

            btnSearchDir.Enabled = true;
        }

        private List<(string fileName, string fullPath, int count)> SearchInDirectory(string dir, string word)
        {
            var results = new List<(string, string, int)>();

            string[] files;
            try
            {
                files = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
            }
            catch
            {
                return results;
            }

            foreach (string file in files)
            {
                try
                {
                    string text = File.ReadAllText(file);
                    int count = 0, index = 0;
                    while ((index = text.IndexOf(word, index, StringComparison.OrdinalIgnoreCase)) >= 0)
                    {
                        count++;
                        index += word.Length;
                    }

                    if (count > 0)
                    {
                        results.Add((
                            Path.GetFileName(file),
                            file,
                            count
                        ));
                    }
                }
                catch { }
            }

            return results;
        }
    }
}
