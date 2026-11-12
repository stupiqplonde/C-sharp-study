using System;
using System.Collections.Generic;
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
                    List<int> numbers = lines
                        .SelectMany(l => l.Split(new[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                        .Select(int.Parse)
                        .ToList();

                    if (numbers.Count == 0) return;

                    var result = numbers.AsParallel()
                        .Select((x, i) =>
                        {
                            int length = 1;
                            for (int j = i + 1; j < numbers.Count; j++)
                            {
                                if (numbers[j] > numbers[j - 1]) length++;
                                else break;
                            }
                            return new { StartIndex = i, Length = length };
                        })
                        .OrderByDescending(x => x.Length)
                        .First();

                    var seq = numbers.Skip(result.StartIndex).Take(result.Length);
                    string seqString = string.Join(", ", seq);

                    txtResult.Text = $"Максимальная длина: {result.Length}\r\n" +
                                     $"Последовательность: {seqString}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}