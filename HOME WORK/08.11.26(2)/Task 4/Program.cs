using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

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
            Console.Write("путь к файлу: ");
            string file = Console.ReadLine();
            Console.Write("Слово для поиска: ");
            string word = Console.ReadLine();

            string exePath = Process.GetCurrentProcess().MainModule.FileName;

            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = $"\"{file}\" \"{word}\"",
                UseShellExecute = false
            };

            Console.WriteLine("\nзапуск дочернего процесса");
            using (Process p = Process.Start(info))
            {
                p.WaitForExit();
                Console.WriteLine("\nдочерний процесс завершён c кодом " + p.ExitCode);
            }

            Console.WriteLine("\nнажмите любую кнопку");
            Console.ReadKey();
        }

        static void RunAsChild(string[] args)
        {
            Console.WriteLine("дочерний процесс");

            if (args.Length < 2)
            {
                Console.WriteLine("нужны путь к файлу и слово");
                Console.ReadKey();
                return;
            }

            string path = args[0];
            string word = args[1];

            Console.WriteLine($"файл: {path}");
            Console.WriteLine($"слово: {word}");

            if (!File.Exists(path))
            {
                Console.WriteLine("файл не найден");
                Console.ReadKey();
                return;
            }

            string text = File.ReadAllText(path);

            int count = 0;
            int index = 0;
            while ((index = text.IndexOf(word, index, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                count++;
                index += word.Length;
            }

            Console.WriteLine($"\nслово \"{word}\" встречается {count} раз(а).");
            Console.ReadKey();
        }
    }
}
