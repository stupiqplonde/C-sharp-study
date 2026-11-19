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
        private ProgressBar[] horses;
        private Label[] labels;
        private volatile bool raceRunning = false;

        public Form1()
        {
            InitializeComponent();
            horses = new[] { pb1, pb2, pb3, pb4, pb5 };
            labels = new[] { lbl1, lbl2, lbl3, lbl4, lbl5 };
        }

        private async void btnRace_Click(object sender, EventArgs e)
        {
            foreach (var h in horses) h.Value = 0;
            lstResults.Items.Clear();
            raceRunning = true;

            Task[] tasks = new Task[horses.Length];
            for (int i = 0; i < horses.Length; i++)
            {
                int index = i;
                tasks[i] = Task.Run(() => RunHorse(index));
            }
            await Task.WhenAll(tasks);
            MessageBox.Show("Гонка завершена");
        }

        private void RunHorse(int index)
        {
            Random rnd = new Random(index * 997 + Environment.TickCount);
            ProgressBar pb = horses[index];

            while (pb.Value < pb.Maximum && raceRunning)
            {
                int step = rnd.Next(1, 6);
                Invoke(new Action(() =>
                {
                    pb.Value = Math.Min(pb.Value + step, pb.Maximum);
                }));
                Thread.Sleep(100);
            }

            Invoke(new Action(() =>
            {
                lstResults.Items.Add($"{labels[index].Text} - финиш ({pb.Value})");
            }));
        }
    }
}
