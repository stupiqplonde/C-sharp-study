using System;
using System.Diagnostics;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 3)
        {
            if (double.TryParse(args[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double a) &&
                double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double b))
            {
                double result;
                switch (args[2])
                {
                    case "+": result = a + b; break;
                    case "-": result = a - b; break;
                    case "*": result = a * b; break;
                    case "/":
                        if (b == 0) { Console.WriteLine("нельзя делить на нооль"); return; }
                        result = a / b;
                        break;
                    default: Console.WriteLine("отказано"); return;
                }

                Console.WriteLine("Аргументы дочернего процесса:");
                Console.WriteLine("Первое число: " + args[0]);
                Console.WriteLine("Второе число: " + args[1]);
                Console.WriteLine("Операция: " + args[2]);
                Console.WriteLine("Результат: " + result);
            }
            else Console.WriteLine("Переданы некорректные числа");
            return;
        }

        Console.Write("Введите первое число: ");
        string first = Console.ReadLine() ?? "0";
        Console.Write("Введите второе число: ");
        string second = Console.ReadLine() ?? "0";
        Console.Write("Введите операцию (+, -, *, /): ");
        string operation = Console.ReadLine() ?? "+";

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Environment.ProcessPath!,
            UseShellExecute = false
        };
        startInfo.ArgumentList.Add(first);
        startInfo.ArgumentList.Add(second);
        startInfo.ArgumentList.Add(operation);

        using Process child = Process.Start(startInfo)!;
        child.WaitForExit();
        Console.WriteLine("Код завершения дочернего процесса: " + child.ExitCode);
    }
}
