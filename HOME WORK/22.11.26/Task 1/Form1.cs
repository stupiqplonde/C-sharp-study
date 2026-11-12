using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var lines = File.ReadAllLines(openFileDialog1.FileName);
                    var numbers = lines
                        .SelectMany(l => l.Split(new[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                        .Select(int.Parse)
                        .ToList();

                    int uniqueCount = numbers.AsParallel().Distinct().Count();

                    txtResult.Text = $"Файл: {Path.GetFileName(openFileDialog1.FileName)}\r\n" +
                                     $"Всего чисел: {numbers.Count}\r\n" +
                                     $"Уникальных чисел (PLINQ): {uniqueCount}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при чтении файла: " + ex.Message);
                }
            }
        }
    }
}