using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player hero = new Player { name = "hero" };
            hero.Died += p => Console.WriteLine($"{p.name} умер");
            hero.LevelUp += p => Console.WriteLine($"{p.name} поднял уровень! Теперь lvl {p.level}");

            Console.WriteLine("Нажмите любую клавишу для повышения уровня, Esc — выход");

            while (true)
            {
                Console.WriteLine($"Уровень: {hero.level}");
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                    break;

                hero.RaiseLevel();
            }

            // RunCalculator();

            // int[] numbers = { -2, -1, 0, 1, 2, 3, 4, 11, 15 };
            //
            // int[] evens = FilterArray(numbers, IsEven);
            // int[] positive = FilterArray(numbers, IsPositive);
            // int[] big = FilterArray(numbers, IsBig);
            // int[] odds = FilterArray(numbers, IsOdd);
            // int[] negative = FilterArray(numbers, IsNegative);
            // int[] small = FilterArray(numbers, IsSmall);
            //
            // Console.WriteLine("Исходный массив: " + string.Join(", ", numbers));
            // Console.WriteLine("Чётные (n % 2 == 0): " + string.Join(", ", evens));
            // Console.WriteLine("Положительные (n > 0): " + string.Join(", ", positive));
            // Console.WriteLine("Большие (n > 10): " + string.Join(", ", big));
            // Console.WriteLine("Нечётные (n % 2 != 0): " + string.Join(", ", odds));
            // Console.WriteLine("Отрицательные (n < 0): " + string.Join(", ", negative));
            // Console.WriteLine("Маленькие (n < 5): " + string.Join(", ", small));
        }

        // static bool IsEven(int n) => n % 2 == 0;
        // static bool IsPositive(int n) => n > 0;
        // static bool IsBig(int n) => n > 10;
        // static bool IsOdd(int n) => n % 2 != 0;
        // static bool IsNegative(int n) => n < 0;
        // static bool IsSmall(int n) => n < 5;
        //
        // static int[] FilterArray(int[] arr, Predicate<int> filter)
        // {
        //     List<int> result = new List<int>();
        //     foreach (int i in arr)
        //     {
        //         if (filter(i))
        //             result.Add(i);
        //     }
        //     return result.ToArray();
        // }

        // static void RunCalculator()
        // {
        //     Func<int, int, int> add = (a, b) => a + b;
        //     Func<int, int, int> subtract = (a, b) => a - b;
        //     Func<int, int, int> multiply = (a, b) => a * b;
        //     Func<int, int, int> divide = (a, b) => b != 0 ? a / b : throw new DivideByZeroException();
        //     
        //     Action<int, int, int> printResult = (a, b, result) =>
        //     {
        //         Console.WriteLine($"{a} и {b} => {result}");
        //     };
        //
        //     Console.Write("Первое число: ");
        //     int a = int.Parse(Console.ReadLine()!);
        //
        //     Console.Write("Второе число: ");
        //     int b = int.Parse(Console.ReadLine()!);
        //
        //     Console.WriteLine("Операция: 1-сложение, 2-вычитание, 3-умножение, 4-деление");
        //     Console.Write("Код операции: ");
        //     int operationCode = int.Parse(Console.ReadLine()!);
        //
        //     Func<int, int, int> calculate = operationCode switch
        //     {
        //         1 => add,
        //         2 => subtract,
        //         3 => multiply,
        //         4 => divide,
        //         _ => throw new ArgumentException("Неизвестная операция")
        //     };
        //
        //     int result = calculate(a, b);
        //     printResult(a, b, result);
        // }
    } 

    class Player
    {
        public string name;
        public int helth;
        public int level = 1;

        public event Action<Player> Died;
        public event Action<Player> LevelUp;

        public void TakeDamage(int damage)
        {
            helth -= damage;
            if (helth <= 0)
                Died?.Invoke(this);
        }

        public void RaiseLevel()
        {
            level++;
            LevelUp?.Invoke(this);
        }
    }
}
