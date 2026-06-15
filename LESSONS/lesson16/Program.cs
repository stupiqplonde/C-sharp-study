using System;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Введите числа массива: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Массив пуст. Повторите ввод.");
                return;
            }

            var arr = input
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToArray();

            if (arr.Length == 0)
            {
                Console.WriteLine("Массив пуст. Повторите ввод.");
                return;
            }

            int sumEven = arr
                .Where(n => n % 2 == 0)
                .DefaultIfEmpty(0)
                .Sum();

            int min = arr.Min();
            int max = arr.Max();

            Console.WriteLine($"Сумма чётных: {sumEven}");
            Console.WriteLine($"Минимальное значение: {min}");
            Console.WriteLine($"Максимальное значение: {max}");
            */
            
            Console.WriteLine("Введите строки: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Массив пуст. Повторите ввод.");
                return;
            }

            string[] arr = input
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (arr.Length == 0)
            {
                Console.WriteLine("Массив пуст. Повторите ввод.");
                return;
            }

            var filtered = arr
                .Where(s => s.Length > 3)
                .OrderBy(s => s)
                .ToArray();

            int countA = arr
                .SelectMany(s => s)
                .Count(c => c == 'а' || c == 'А');

            Console.WriteLine("Строки длиной > 3 (по алфавиту):");
            foreach (string s in filtered)
                Console.WriteLine($"  {s}");

            if (filtered.Length == 0)
                Console.WriteLine("  (нет подходящих строк)");

            Console.WriteLine($"Общее количество букв 'а': {countA}");
        }
    }
}
