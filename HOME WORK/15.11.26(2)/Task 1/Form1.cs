using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
        private ManualResetEvent generationDone = new ManualResetEvent(false);
        private string folder;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int n = (int)numPairs.Value;
            folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "results");
            Directory.CreateDirectory(folder);

            rtbLog.Clear();
            generationDone.Reset();

            Thread t2 = new Thread(() => SumWorker());
            Thread t3 = new Thread(() => ProductWorker());
            t2.IsBackground = true;
            t3.IsBackground = true;
            t2.Start();
            t3.Start();

            Thread t1 = new Thread(() => GenerateWorker(n));
            t1.IsBackground = true;
            t1.Start();
        }

        private void GenerateWorker(int count)
        {
            Random rnd = new Random();
            string path = Path.Combine(folder, "pairs.txt");

            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < count; i++)
                {
                    int a = rnd.Next(1, 100);
                    int b = rnd.Next(1, 100);
                    sw.WriteLine($"{a} {b}");
                    Log($"сгенерирована пара: {a} {b}");
                    Thread.Sleep(200); 
                }
            }

            Log("Генерация завершена");
            generationDone.Set();
        }

        private void SumWorker()
        {
            Log("Поток сумм ждёт генерацию");
            generationDone.WaitOne();
            Log("Поток сумм начал работу");

            string inPath = Path.Combine(folder, "pairs.txt");
            string outPath = Path.Combine(folder, "sums.txt");

            using (StreamReader sr = new StreamReader(inPath))
            using (StreamWriter sw = new StreamWriter(outPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    int a = int.Parse(parts[0]);
                    int b = int.Parse(parts[1]);
                    int sum = a + b;
                    sw.WriteLine($"{a} + {b} = {sum}");
                }
            }

            Log("Суммы записаны в sums.txt");
        }

        private void ProductWorker()
        {
            Log("поток произведений ждёт генерацию");
            generationDone.WaitOne();
            Log("поток произведений начал работу");

            string inPath = Path.Combine(folder, "pairs.txt");
            string outPath = Path.Combine(folder, "products.txt");

            using (StreamReader sr = new StreamReader(inPath))
            using (StreamWriter sw = new StreamWriter(outPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    int a = int.Parse(parts[0]);
                    int b = int.Parse(parts[1]);
                    int prod = a * b;
                    sw.WriteLine($"{a} * {b} = {prod}");
                }
            }

            Log("произведения записаны в products.txt");
        }

        private void Log(string msg)
        {
            if (InvokeRequired)
                Invoke(new Action(() => rtbLog.AppendText(msg + "\n")));
            else
                rtbLog.AppendText(msg + "\n");
        }
    }
}
