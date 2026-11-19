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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtFile.Text = dlg.FileName;
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string path = txtFile.Text;
            string word = txtWord.Text;

            if (!File.Exists(path))
            {
                MessageBox.Show("файл не найден");
                return;
            }

            btnSearch.Enabled = false;
            lblResult.Text = "поиск";

            int count = await Task.Run(() => CountWordInFile(path, word));

            lblResult.Text = $"Слово \"{word}\" встречается {count} раз";
            btnSearch.Enabled = true;
        }

        private int CountWordInFile(string path, string word)
        {
            string text = File.ReadAllText(path);
            int count = 0, index = 0;
            while ((index = text.IndexOf(word, index, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                count++;
                index += word.Length;
            }
            return count;
        }
    }
}
