using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--child")
        {
            Console.WriteLine("Дочерний процесс запущен. Он завершится через 30 секунд");
            Thread.Sleep(30000);
            Console.WriteLine("Дочерний процесс завершён самостоятельно");
            return;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Environment.ProcessPath!,
            UseShellExecute = false
        };
        startInfo.ArgumentList.Add("--child");

        using Process child = Process.Start(startInfo)!;

        Console.WriteLine("Выберите действие:");
        Console.WriteLine("1 — ожидать завершения дочернего процесса");
        Console.WriteLine("2 — принудительно завершить дочерний процесс");
        string? choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.WriteLine("Ожидание завершения...");
            child.WaitForExit();
        }
        else if (choice == "2")
        {
            child.Kill();
            child.WaitForExit();
            Console.WriteLine("Дочерний процесс принудительно завершён");
        }
        else
        {
            Console.WriteLine("Неверный выбор. Ожидаем завершения процесса...");
            child.WaitForExit();
        }

        Console.WriteLine("Код завершения: " + child.ExitCode);
    }
}
