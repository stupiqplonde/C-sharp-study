using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("kernel32.dll")]
    static extern bool Beep(uint frequency, uint duration);

    [DllImport("user32.dll")]
    static extern bool MessageBeep(uint type);

    static void Main()
    {
        Console.WriteLine("Начало воспроизведения сигналов");

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("Сигнал Beep");

            Beep(800, 300);

            Thread.Sleep(1000);

            Console.WriteLine("Сигнал MessageBeep");

            MessageBeep(0x00000000);

            Thread.Sleep(1500);
        }

        Console.WriteLine("Воспроизведение завершено");
    }
}