using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1_EventsAndPairs
{
    public class MainForm : Form
    {
        private readonly NumericUpDown pairCount = new() { Minimum = 1, Maximum = 10000, Value = 20, Width = 100 };
        private readonly TextBox folderBox = new() { Width = 340 };
        private readonly Button browseButton = new() { Text = "Папка..." };
        private readonly Button startButton = new() { Text = "Выполнить" };
        private readonly TextBox output = new() { Multiline = true, ReadOnly = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical };

        public MainForm()
        {
            Text = "Задание 1 — События и пары чисел";
            Width = 760; Height = 500; MinimumSize = new System.Drawing.Size(600, 350);
            folderBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 90, Padding = new Padding(10), WrapContents = true };
            top.Controls.Add(new Label { Text = "Количество пар:", AutoSize = true, Margin = new Padding(0, 8, 4, 0) });
            top.Controls.Add(pairCount);
            top.Controls.Add(new Label { Text = "Папка для файлов:", AutoSize = true, Margin = new Padding(12, 8, 4, 0) });
            top.Controls.Add(folderBox);
            top.Controls.Add(browseButton);
            top.Controls.Add(startButton);
            Controls.Add(output);
            Controls.Add(top);

            browseButton.Click += (_, _) =>
            {
                using var dialog = new FolderBrowserDialog();
                if (Directory.Exists(folderBox.Text)) dialog.SelectedPath = folderBox.Text;
                if (dialog.ShowDialog() == DialogResult.OK) folderBox.Text = dialog.SelectedPath;
            };
            startButton.Click += async (_, _) => await RunThreadsAsync();
        }

        private async Task RunThreadsAsync()
        {
            if (!Directory.Exists(folderBox.Text))
            {
                MessageBox.Show("Выберите существующую папку.");
                return;
            }

            startButton.Enabled = false;
            output.Clear();
            string folder = folderBox.Text;
            int amount = (int)pairCount.Value;
            try
            {
                string report = await Task.Run(() =>
                {
                    string pairsPath = Path.Combine(folder, "pairs.txt");
                    string sumsPath = Path.Combine(folder, "sums.txt");
                    string productsPath = Path.Combine(folder, "products.txt");

                    using var generated = new ManualResetEventSlim(false);
                    Exception? workerError = null;

                    Thread generator = new(() =>
                    {
                        try
                        {
                            var random = new Random();
                            using var writer = new StreamWriter(pairsPath, false);
                            for (int i = 0; i < amount; i++)
                            {
                                int a = random.Next(1, 101);
                                int b = random.Next(1, 101);
                                writer.WriteLine($"{a};{b}");
                            }
                        }
                        finally { generated.Set(); }
                    });

                    Thread sumWorker = new(() =>
                    {
                        generated.Wait();
                        try
                        {
                            using var reader = new StreamReader(pairsPath);
                            using var writer = new StreamWriter(sumsPath, false);
                            string? line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] parts = line.Split(';');
                                writer.WriteLine(int.Parse(parts[0]) + int.Parse(parts[1]));
                            }
                        }
                        catch (Exception ex) { workerError = ex; }
                    });

                    Thread productWorker = new(() =>
                    {
                        generated.Wait();
                        try
                        {
                            using var reader = new StreamReader(pairsPath);
                            using var writer = new StreamWriter(productsPath, false);
                            string? line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] parts = line.Split(';');
                                writer.WriteLine(int.Parse(parts[0]) * int.Parse(parts[1]));
                            }
                        }
                        catch (Exception ex) { workerError ??= ex; }
                    });

                    generator.Start();
                    sumWorker.Start();
                    productWorker.Start();
                    generator.Join();
                    sumWorker.Join();
                    productWorker.Join();

                    if (workerError != null) throw workerError;
                    return $"Работа завершена.{Environment.NewLine}" +
                           $"Создан файл пар: {pairsPath}{Environment.NewLine}" +
                           $"Файл сумм: {sumsPath}{Environment.NewLine}" +
                           $"Файл произведений: {productsPath}{Environment.NewLine}" +
                           $"Количество обработанных пар: {amount}";
                });

                output.Text = report;
            }
            catch (Exception ex) { output.Text = "Ошибка: " + ex.Message; }
            finally { startButton.Enabled = true; }
        }
    }
}
