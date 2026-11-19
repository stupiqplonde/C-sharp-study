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

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        private ProgressBar[] bars;
        private volatile bool running = false;

        public Form2()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            panelBars.Controls.Clear();
            int n = (int)numCount.Value;
            bars = new ProgressBar[n];

            for (int i = 0; i < n; i++)
            {
                var pb = new ProgressBar
                {
                    Width = 300,
                    Height = 20,
                    Maximum = 100,
                    Value = 0,
                    Margin = new Padding(5)
                };
                bars[i] = pb;
                panelBars.Controls.Add(pb);
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (bars == null || bars.Length == 0)
            {
                MessageBox.Show("Сначала создайте бары");
                return;
            }

            running = true;
            Task[] tasks = new Task[bars.Length];
            for (int i = 0; i < bars.Length; i++)
            {
                int index = i;
                tasks[i] = Task.Run(() => DanceBar(index));
            }
            await Task.WhenAll(tasks);
            MessageBox.Show("все бары заполнены");
        }

        private void DanceBar(int index)
        {
            ProgressBar pb = bars[index];
            Random rnd = new Random(index * 31 + Environment.TickCount);

            Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            Invoke(new Action(() => pb.ForeColor = color));

            int speed = rnd.Next(1, 11);

            while (pb.Value < pb.Maximum && running)
            {
                int step = rnd.Next(1, speed + 1);
                Invoke(new Action(() =>
                {
                    pb.Value = Math.Min(pb.Value + step, pb.Maximum);
                }));
                Thread.Sleep(50);
            }
        }


    }
}
