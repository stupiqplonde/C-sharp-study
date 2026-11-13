using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private async void btnCalc_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(txtLimit.Text, out long limit))
            {
                MessageBox.Show("Введите число");
                return;
            }

            btnCalc.Enabled = false;
            lblResult.Text = "Подсчет";
            lstFib.Items.Clear();

            var result = await Task.Run(() => CalculateFibonacci(limit));

            foreach (var n in result.numbers)
                lstFib.Items.Add(n);

            lblResult.Text = $"Чисел: {result.numbers.Count}, сумма: {result.sum}";
            btnCalc.Enabled = true;
        }

        private (List<long> numbers, long sum) CalculateFibonacci(long limit)
        {
            var list = new List<long>();
            long a = 0, b = 1;
            long sum = 0;

            while (a <= limit)
            {
                list.Add(a);
                sum += a;
                long next = a + b;
                a = b;
                b = next;
            }

            return (list, sum);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
