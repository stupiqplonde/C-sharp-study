using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2_HorseRace
{
    public class MainForm : Form
    {
        private readonly ProgressBar[] horses = new ProgressBar[5];
        private readonly Button startButton = new() { Text = "Старт гонки", AutoSize = true };
        private readonly ListBox results = new() { Width = 260, Height = 180 };
        private bool running;

        public MainForm()
        {
            Text = "Задание 2 — Конные скачки";
            Width = 720; Height = 440; MinimumSize = new Size(650, 400);
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(15) };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            var left = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            left.Controls.Add(new Label { Text = "Пять лошадей соревнуются на дистанции 100 условных единиц.", AutoSize = true, Margin = new Padding(0, 0, 0, 15) });
            for (int i = 0; i < 5; i++)
            {
                var row = new FlowLayoutPanel { Width = 420, Height = 42, WrapContents = false };
                row.Controls.Add(new Label { Text = $"Лошадь {i + 1}", Width = 85, Margin = new Padding(0, 8, 5, 0) });
                horses[i] = new ProgressBar { Width = 300, Height = 24, Maximum = 100 };
                row.Controls.Add(horses[i]);
                left.Controls.Add(row);
            }
            left.Controls.Add(startButton);
            var right = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            right.Controls.Add(new Label { Text = "Результаты (места):", AutoSize = true });
            right.Controls.Add(results);
            layout.Controls.Add(left, 0, 0);
            layout.Controls.Add(right, 1, 0);
            Controls.Add(layout);
            startButton.Click += async (_, _) => await StartRaceAsync();
        }

        private async Task StartRaceAsync()
        {
            if (running) return;
            running = true;
            startButton.Enabled = false;
            results.Items.Clear();
            foreach (var horse in horses) horse.Value = 0;
            var finishOrder = new List<int>();
            object sync = new();
            try
            {
                var tasks = Enumerable.Range(0, 5).Select(index => Task.Run(async () =>
                {
                    var random = new Random(Guid.NewGuid().GetHashCode());
                    int progress = 0;
                    while (progress < 100)
                    {
                        progress = Math.Min(100, progress + random.Next(1, 9));
                        int current = progress;
                        if (!IsDisposed && IsHandleCreated)
                            BeginInvoke(new Action(() => horses[index].Value = current));
                        await Task.Delay(random.Next(70, 230));
                    }
                    lock (sync) finishOrder.Add(index);
                })).ToArray();

                await Task.WhenAll(tasks);
                results.Items.Add("Победитель: Лошадь " + (finishOrder[0] + 1));
                for (int i = 0; i < finishOrder.Count; i++)
                    results.Items.Add($"{i + 1}-е место — Лошадь {finishOrder[i] + 1}");
            }
            finally
            {
                running = false;
                startButton.Enabled = true;
            }
        }
    }
}
