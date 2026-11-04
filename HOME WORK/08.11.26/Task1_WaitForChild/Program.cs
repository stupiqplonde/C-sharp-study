using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--child")
        {
            Console.WriteLine("Дочерний процесс работает...");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Дочерний процесс завершён");
            return;
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Environment.ProcessPath!,
            UseShellExecute = false
        };
        startInfo.ArgumentList.Add("--child");

        using Process child = Process.Start(startInfo)!;
        Console.WriteLine("Родитель ожидает завершения дочернего процесса...");
        child.WaitForExit();

        Console.WriteLine("Код завершения: " + child.ExitCode);
    }
}
