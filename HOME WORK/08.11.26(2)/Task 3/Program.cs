using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                RunAsChild(args);
                return;
            }

            RunAsParent();
        }

        static void RunAsParent()
        {
            Console.Write("Число 1: ");
            string a = Console.ReadLine();
            Console.Write("Число 2: ");
            string b = Console.ReadLine();
            Console.Write("Операция (+ - * /): ");
            string op = Console.ReadLine();

            string exePath = Process.GetCurrentProcess().MainModule.FileName;

            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"{a} {b} {op}",
                UseShellExecute = false
            };

            Console.WriteLine("\nзапуск дочернего процесса");
            using (Process p = Process.Start(info))
            {
                p.WaitForExit();
                Console.WriteLine("\nдочерний процесс завершён с кодом: " + p.ExitCode);
            }

            Console.WriteLine("\nнажмите любую кнопку");
            Console.ReadKey();
        }

        static void RunAsChild(string[] args)
        {
            Console.WriteLine("доченрий проуесс");
            Console.WriteLine("получено аргументов: " + args.Length);

            if (args.Length < 3)
            {
                Console.WriteLine("нужно: число1 + число2 + операция");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"арг 1: {args[0]}");
            Console.WriteLine($"арг 2: {args[1]}");
            Console.WriteLine($"арг 3: {args[2]}");

            double x, y;
            if (!double.TryParse(args[0], out x) || !double.TryParse(args[1], out y))
            {
                Console.WriteLine("не все - числа");
                Console.ReadKey();
                return;
            }

            string op = args[2];
            double result;
            switch (op)
            {
                case "+": result = x + y; break;
                case "-": result = x - y; break;
                case "*": result = x * y; break;
                case "/":
                    if (y == 0) { Console.WriteLine("не делить на 0"); Console.ReadKey(); return; }
                    result = x / y; break;
                default:
                    Console.WriteLine("неизвестная операция: " + op);
                    Console.ReadKey();
                    return;
            }

            Console.WriteLine($"\nрезультат: {result}");
            Console.ReadKey();
        }
    }
}
