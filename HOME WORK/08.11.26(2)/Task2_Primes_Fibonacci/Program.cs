using System;
using System.Windows.Forms;

namespace Task2_Primes_Fibonacci
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
