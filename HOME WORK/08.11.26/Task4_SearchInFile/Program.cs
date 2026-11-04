using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 2)
        {
            string path = args[0];
            string word = args[1];

            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден: " + path);
                Environment.ExitCode = 1;
                return;
            }

            if (word.Length == 0)
            {
                Console.WriteLine("Слово для поиска не должно быть пустым.");
                Environment.ExitCode = 1;
                return;
            }

            string content = File.ReadAllText(path);
            int count = 0;
            int position = 0;

            while ((position = content.IndexOf(word, position, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                count++;
                position += word.Length;
            }

            Console.WriteLine("Файл: " + path);
            Console.WriteLine("Слово для поиска: " + word);
            Console.WriteLine("Количество вхождений: " + count);
            return;
        }

        Console.Write("Введите полный путь к файлу: ");
        string filePath = Console.ReadLine() ?? "";
        Console.Write("Введите слово для поиска: ");
        string searchWord = Console.ReadLine() ?? "";

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Environment.ProcessPath!,
            UseShellExecute = false
        };
        startInfo.ArgumentList.Add(filePath);
        startInfo.ArgumentList.Add(searchWord);

        using Process child = Process.Start(startInfo)!;
        child.WaitForExit();
        Console.WriteLine("Код завершения дочернего процесса: " + child.ExitCode);
    }
}
