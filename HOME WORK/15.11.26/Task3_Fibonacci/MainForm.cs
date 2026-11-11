using System;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3_Fibonacci
{
    public class MainForm : Form
    {
        private readonly NumericUpDown limitInput = new() { Minimum = 0, Maximum = 1000000000, Value = 1000, Width = 150, ThousandsSeparator = true };
        private readonly Button calculateButton = new() { Text = "Рассчитать", AutoSize = true };
        private readonly TextBox output = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill, Font = new System.Drawing.Font("Consolas", 10) };
        private readonly Label status = new() { AutoSize = true, Text = "Укажите верхнюю границу." };

        public MainForm()
        {
            Text = "Задание 3 — Числа Фибоначчи";
            Width = 650; Height = 500; MinimumSize = new System.Drawing.Size(450, 350);
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(10), WrapContents = false };
            top.Controls.Add(new Label { Text = "Граница:", AutoSize = true, Margin = new Padding(0, 8, 5, 0) });
            top.Controls.Add(limitInput);
            top.Controls.Add(calculateButton);
            top.Controls.Add(status);
            Controls.Add(output);
            Controls.Add(top);
            calculateButton.Click += async (_, _) => await CalculateAsync();
        }

        private async Task CalculateAsync()
        {
            calculateButton.Enabled = false;
            output.Clear();
            status.Text = "Вычисление...";
            try
            {
                BigInteger limit = new BigInteger(limitInput.Value);
                string result = await Task.Run(() =>
                {
                    var sb = new StringBuilder();
                    BigInteger a = 0, b = 1;
                    int count = 0;
                    while (a <= limit)
                    {
                        sb.AppendLine(a.ToString());
                        count++;
                        BigInteger next = a + b;
                        a = b;
                        b = next;
                    }
                    return $"Найдено чисел: {count}{Environment.NewLine}{Environment.NewLine}{sb}";
                });
                output.Text = result;
                status.Text = "Готово.";
            }
            catch (Exception ex) { status.Text = "Ошибка: " + ex.Message; }
            finally { calculateButton.Enabled = true; }
        }
    }
}
