using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1_DancingProgressBars
{
    public class MainForm : Form
    {
        private readonly NumericUpDown countInput = new() { Minimum = 1, Maximum = 30, Value = 8, Width = 70 };
        private readonly Button startButton = new() { Text = "Старт", AutoSize = true };
        private readonly FlowLayoutPanel barsPanel = new() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, Padding = new Padding(12) };
        private readonly List<ProgressBar> bars = new();
        private bool running;

        public MainForm()
        {
            Text = "Задание 1 - Танцующие прогресс-бары";
            Width = 650; Height = 550; MinimumSize = new Size(500, 350);
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 55, Padding = new Padding(12), WrapContents = false };
            top.Controls.Add(new Label { Text = "Количество:", AutoSize = true, Margin = new Padding(0, 8, 8, 0) });
            top.Controls.Add(countInput);
            top.Controls.Add(startButton);
            Controls.Add(barsPanel);
            Controls.Add(top);
            countInput.ValueChanged += (_, _) => CreateBars();
            startButton.Click += async (_, _) => await StartBarsAsync();
            CreateBars();
        }

        private void CreateBars()
        {
            if (running) return;
            barsPanel.Controls.Clear();
            bars.Clear();
            for (int i = 0; i < (int)countInput.Value; i++)
            {
                var row = new FlowLayoutPanel { Width = 570, Height = 38, WrapContents = false };
                row.Controls.Add(new Label { Text = $"Полоса {i + 1}", Width = 75, Margin = new Padding(0, 8, 5, 0) });
                var bar = new ProgressBar { Width = 440, Height = 24, Maximum = 100 };
                bars.Add(bar);
                row.Controls.Add(bar);
                barsPanel.Controls.Add(row);
            }
        }

        private async Task StartBarsAsync()
        {
            if (running) return;
            running = true;
            countInput.Enabled = false;
            startButton.Enabled = false;
            foreach (var b in bars) { b.Value = 0; b.ForeColor = Color.DodgerBlue; }
            try
            {
                var tasks = new List<Task>();
                for (int i = 0; i < bars.Count; i++)
                {
                    int index = i;
                    tasks.Add(Task.Run(async () =>
                    {
                        var random = new Random(Guid.NewGuid().GetHashCode());
                        int value = 0;
                        Color[] colors = { Color.Crimson, Color.SeaGreen, Color.RoyalBlue, Color.DarkOrange, Color.MediumPurple, Color.DeepPink };
                        Color color = colors[random.Next(colors.Length)];
                        while (value < 100)
                        {
                            value = Math.Min(100, value + random.Next(1, 12));
                            int current = value;
                            if (!IsDisposed && IsHandleCreated)
                                BeginInvoke(new Action(() => { bars[index].Value = current; bars[index].ForeColor = color; }));
                            await Task.Delay(random.Next(60, 220));
                        }
                    }));
                }
                await Task.WhenAll(tasks);
            }
            finally
            {
                running = false;
                countInput.Enabled = true;
                startButton.Enabled = true;
            }
        }
    }
}
