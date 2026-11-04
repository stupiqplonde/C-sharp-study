using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Task3_Stop_Threads
{
    public class MainForm : Form
    {
        private readonly TextBox lowerBox = new() { Width = 110, PlaceholderText = "От (по умолч. 2)" };
        private readonly TextBox upperBox = new() { Width = 110, PlaceholderText = "До (пусто = беск.)" };
        private readonly Button startButton = new() { Text = "Запустить", AutoSize = true };
        private readonly TextBox primesOutput = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill };
        private Worker? primesWorker;
        private readonly TextBox fibOutput = new() { Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical, Dock = DockStyle.Fill };
        private Worker? fibWorker;
        private readonly Button stopPrimesButton = new() { Text = "Остановить простые", AutoSize = true };
        private readonly Button stopFibButton = new() { Text = "Остановить Фибоначчи", AutoSize = true };

        public MainForm()
        {
            Text = "Полная остановка потоков";
            Width = 900; Height = 620;
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(700, 450);

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(10) };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            Controls.Add(root);

            var settings = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = true };
            settings.Controls.Add(new Label { Text = "Нижняя граница:", AutoSize = true, Padding = new Padding(0, 7, 0, 0) });
            settings.Controls.Add(lowerBox);
            settings.Controls.Add(new Label { Text = "Верхняя граница:", AutoSize = true, Padding = new Padding(10, 7, 0, 0) });
            settings.Controls.Add(upperBox);
            settings.Controls.Add(startButton);
            root.Controls.Add(settings, 0, 0);

            var outputs = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            outputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            outputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            outputs.Controls.Add(MakeColumn("Простые числа", primesOutput), 0, 0);
            outputs.Controls.Add(MakeColumn("Числа Фибоначчи", fibOutput), 1, 0);
            root.Controls.Add(outputs, 0, 1);

            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = true, Padding = new Padding(0, 8, 0, 0) };
            actions.Controls.Add(stopPrimesButton); actions.Controls.Add(stopFibButton);
            root.Controls.Add(actions, 0, 2);

            startButton.Click += (_, _) => StartAll();
            stopPrimesButton.Click += (_, _) => primesWorker?.Stop(); stopFibButton.Click += (_, _) => fibWorker?.Stop();
        }

        private void StartAll()
        {
            if (!TryReadBounds(out long lower, out long? upper)) return;
            StopAll();
            primesOutput.Clear();
            primesWorker = new Worker("Простые числа", lower, upper, IsPrime, value => Append(primesOutput, value));
            primesWorker.Start();
            fibOutput.Clear();
            fibWorker = new Worker("Числа Фибоначчи", 0, upper, IsFibonacci, value => Append(fibOutput, value));
            fibWorker.Start();
        }


        private static Control MakeColumn(string title, Control content)
        {{
            var panel = new TableLayoutPanel {{ Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, Padding = new Padding(4) }};
            panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            panel.Controls.Add(new Label {{ Text = title, AutoSize = true, Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold) }}, 0, 0);
            panel.Controls.Add(content, 0, 1);
            return panel;
        }}

        private static bool IsFibonacci(long n)
        {{
            if (n < 0) return false;
            long a = 0, b = 1;
            while (a < n)
            {{
                long next;
                try {{ next = checked(a + b); }}
                catch (OverflowException) {{ return false; }}
                a = b; b = next;
            }}
            return a == n;
        }}

        private bool TryReadBounds(out long lower, out long? upper)
        {
            lower = 2; upper = null;
            if (!string.IsNullOrWhiteSpace(lowerBox.Text) && (!long.TryParse(lowerBox.Text, out lower) || lower < 2))
            {
                MessageBox.Show("Нижняя граница должна быть целым числом не меньше 2.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(upperBox.Text))
            {
                if (!long.TryParse(upperBox.Text, out long value) || value < lower)
                {
                    MessageBox.Show("Верхняя граница должна быть целым числом и не меньше нижней.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                upper = value;
            }
            return true;
        }

        private static bool IsPrime(long n)
        {
            if (n < 2) return false;
            if (n % 2 == 0) return n == 2;
            for (long d = 3; d <= n / d; d += 2)
                if (n % d == 0) return false;
            return true;
        }

        private void Append(TextBox box, long value)
        {
            if (IsDisposed || !IsHandleCreated) return;
            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (!box.IsDisposed) box.AppendText(value + Environment.NewLine);
                }));
            }
            catch (InvalidOperationException) { }
        }

        private void StopAll()
        {
            primesWorker?.Stop();
            fibWorker?.Stop();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopAll();
            base.OnFormClosing(e);
        }

        private sealed class Worker
        {
            private readonly string name;
            private readonly long lower;
            private readonly long? upper;
            private readonly Func<long, bool> predicate;
            private readonly Action<long> publish;
            private readonly CancellationTokenSource cancellation = new();
            private readonly ManualResetEventSlim resumeEvent = new(true);
            private Thread? thread;

            public Worker(string name, long lower, long? upper, Func<long, bool> predicate, Action<long> publish)
            {
                this.name = name; this.lower = lower; this.upper = upper;
                this.predicate = predicate; this.publish = publish;
            }

            public void Start()
            {
                thread = new Thread(Run) { IsBackground = true, Name = name };
                thread.Start();
            }

            private void Run()
            {
                try
                {
                    for (long n = lower; !cancellation.IsCancellationRequested && (!upper.HasValue || n <= upper.Value); n++)
                    {
                        resumeEvent.Wait(cancellation.Token);
                        if (predicate(n)) publish(n);
                        if (n == long.MaxValue) break;
                    }
                }
                catch (OperationCanceledException) { }
                catch (ObjectDisposedException) { }
            }

            public void Stop()
            {
                cancellation.Cancel();
                resumeEvent.Set();
            }

            public void Pause() => resumeEvent.Reset();
            public void Resume() => resumeEvent.Set();
        }
    }
}
