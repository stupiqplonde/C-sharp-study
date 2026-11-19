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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // поток для простых чисел
        private Thread primeThread;
        private volatile bool primeRunning = false;  
        private volatile bool primePaused = false;

        // поток для фибоначчи
        // Я, Ковалев Михаил, самостоятельбно прочитал о volatile вот тут: 
        // https://learn.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/volatile
        private Thread fibThread;
        private volatile bool fibRunning = false;
        private volatile bool fibPaused = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrimeStart_Click(object sender, EventArgs e)
        {
            if (primeThread != null && primeThread.IsAlive)
                return;

            long from = 2;
            if (long.TryParse(txtPrimeFrom.Text, out long f))
                from = f;

            long to = long.MaxValue;
            if (long.TryParse(txtPrimeTo.Text, out long t))
                to = t;

            primeRunning = true;
            primePaused = false;

            primeThread = new Thread(() => GeneratePrimes(from, to));
            primeThread.IsBackground = true; 
            primeThread.Start();
        }

        private void GeneratePrimes(long from, long to)
        {
            for (long n = from; n <= to; n++)
            {
                if (!primeRunning) return;

                while (primePaused)
                {
                    if (!primeRunning) return; 
                    Thread.Sleep(50);          
                }

                if (IsPrime(n))
                {
                    Invoke(new Action(() => lstPrimes.Items.Add(n)));
                }
            }
        }

        private static bool IsPrime(long n)
        {
            if (n < 2) return false;
            for (long i = 2; i * i <= n; i++)
                if (n % i == 0) return false;
            return true;
        }

        private void btnPrimeStop_Click(object sender, EventArgs e)
        {
            primeRunning = false; 
            primePaused = false;  
        }

        private void btnPrimePause_Click(object sender, EventArgs e)
        {
            primePaused = true;
        }

        private void btnPrimeResume_Click(object sender, EventArgs e)
        {
            primePaused = false;
        }

        private void btnPrimeRestart_Click(object sender, EventArgs e)
        {
            primeRunning = false;
            primePaused = false;

            if (primeThread != null && primeThread.IsAlive)
                primeThread.Join(1000);

            lstPrimes.Items.Clear();

            btnPrimeStart_Click(sender, e);
        }

        private void btnFibStart_Click(object sender, EventArgs e)
        {
            if (fibThread != null && fibThread.IsAlive)
                return;

            fibRunning = true;
            fibPaused = false;

            fibThread = new Thread(GenerateFibonacci);
            fibThread.IsBackground = true;
            fibThread.Start();
        }

        private void GenerateFibonacci()
        {
            long a = 0;
            long b = 1;

            while (fibRunning)
            {
                while (fibPaused)
                {
                    if (!fibRunning) return;
                    Thread.Sleep(50);
                }

                long current = a;
                Invoke(new Action(() => lstFib.Items.Add(current)));

                long next = a + b;
                a = b;
                b = next;

                Thread.Sleep(100); 
            }
        }

        private void btnFibStop_Click(object sender, EventArgs e)
        {
            fibRunning = false;
            fibPaused = false;
        }

        private void btnFibPause_Click(object sender, EventArgs e)
        {
            fibPaused = true;
        }

        private void btnFibResume_Click(object sender, EventArgs e)
        {
            fibPaused = false;
        }

        private void btnFibRestart_Click(object sender, EventArgs e)
        {
            fibRunning = false;
            fibPaused = false;

            if (fibThread != null && fibThread.IsAlive)
                fibThread.Join(1000);

            lstFib.Items.Clear();

            btnFibStart_Click(sender, e);
        }
    }
}
