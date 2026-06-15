using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    internal class Program
    {

        // 1

        //static void Main(string[] args)
        //{
        //    Console.Write("Введите размер квадратной матрицы n: ");
        //    int n = int.Parse(Console.ReadLine());

        //    int[,] matrix = new int[n, n];

        //    Console.WriteLine("Введите элементы матрицы построчно:");
        //    for (int i = 0; i < n; i++)
        //    {
        //        string[] row = Console.ReadLine().Split(' ');

        //        for (int j = 0; j < n; j++)
        //        {
        //            matrix[i, j] = int.Parse(row[j]);
        //        }
        //    }

        //    int sum = 0;
        //    for (int i = 0; i < n; i++)
        //    {
        //        sum += matrix[i, n - 1 - i];
        //    }

        //    Console.WriteLine("Сумма элементов побочной диагонали: " + sum);
        //}

        // 2

        //static void BubbleSort(int[,] arr)
        //{
        //    int rows = arr.GetLength(0);
        //    int cols = arr.GetLength(1);
        //    int total = rows * cols;
        //    int[] flat = new int[total];

        //    int index = 0;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < cols; j++)
        //        {
        //            flat[index] = arr[i, j];
        //            index++;
        //        }
        //    }

        //    for (int i = 0; i < total - 1; i++)
        //    {
        //        for (int j = 0; j < total - i - 1; j++)
        //        {
        //            if (flat[j] > flat[j + 1])
        //            {
        //                int temp = flat[j];
        //                flat[j] = flat[j + 1];
        //                flat[j + 1] = temp;
        //            }
        //        }
        //    }

        //    index = 0;
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < cols; j++)
        //        {
        //            arr[i, j] = flat[index];
        //            index++;
        //        }
        //    }

        //    Console.WriteLine("Отсортированный двумерный массив:");
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < cols; j++)
        //        {
        //            Console.Write(arr[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        //static void Main()
        //{
        //    int[,] numbers = { { 1, 67, 3 }, { 52, 5, 69 } };

        //    BubbleSort(numbers);
        //}

        // 3

        //static void Main(string[] args)
        //{
        //    Console.Write("Введите количество строк: ");
        //    int rows = int.Parse(Console.ReadLine());

        //    Console.Write("Введите количество столбцов: ");
        //    int cols = int.Parse(Console.ReadLine());

        //    int[,] matrix = new int[rows, cols];

        //    Console.WriteLine("Введите элементы массива построчно:");
        //    for (int i = 0; i < rows; i++)
        //    {
        //        string[] row = Console.ReadLine().Split(' ');

        //        for (int j = 0; j < cols; j++)
        //        {
        //            matrix[i, j] = int.Parse(row[j]);
        //        }
        //    }

        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < cols / 2; j++)
        //        {
        //            int temp = matrix[i, j];
        //            matrix[i, j] = matrix[i, cols - 1 - j];
        //            matrix[i, cols - 1 - j] = temp;
        //        }
        //    }

        //    Console.WriteLine("Массив после реверса каждой строки:");
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < cols; j++)
        //        {
        //            Console.Write(matrix[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        // 4

        //static void Main(string[] args)
        //{
        //    Console.Write("Введите количество строк: ");
        //    int rows = int.Parse(Console.ReadLine());

        //    Console.Write("Введите количество столбцов: ");
        //    int cols = int.Parse(Console.ReadLine());

        //    int[,] matrix = new int[rows, cols];

        //    Console.WriteLine("Введите элементы массива построчно:");
        //    for (int i = 0; i < rows; i++)
        //    {
        //        string[] row = Console.ReadLine().Split(' ');

        //        for (int j = 0; j < cols; j++)
        //        {
        //            matrix[i, j] = int.Parse(row[j]);
        //        }
        //    }

        //    for (int i = 0; i < rows; i++)
        //    {
        //        int min = matrix[i, 0];
        //        int max = matrix[i, 0];

        //        for (int j = 1; j < cols; j++)
        //        {
        //            if (max < matrix[i, j])
        //            {
        //                max = matrix[i, j];
        //            }

        //            if (min > matrix[i, j])
        //            {
        //                min = matrix[i, j];
        //            }

        //        }

        //        Console.WriteLine($"макс : {max}");
        //        Console.WriteLine($"мин : {min}");
        //    }

        //Console.WriteLine("Массив :");
        //for (int i = 0; i < rows; i++)
        //{
        //    for (int j = 0; j < cols; j++)
        //    {
        //        Console.Write(matrix[i, j] + " ");
        //    }
        //    Console.WriteLine();
        //}
        //}

        // 5

        //static void Main(string[] args)
        //{
        //    Console.Write("Введите количество строк: ");
        //    int rows = int.Parse(Console.ReadLine());

        //    Console.Write("Введите количество столбцов: ");
        //    int cols = int.Parse(Console.ReadLine());

        //    int[,] matrix = new int[rows, cols];

        //    Console.WriteLine("Введите элементы массива построчно:");
        //    for (int i = 0; i < rows; i++)
        //    {
        //        string[] row = Console.ReadLine().Split(' ');

        //        for (int j = 0; j < cols; j++)
        //        {
        //            matrix[i, j] = int.Parse(row[j]);
        //        }
        //    }

        //    int[,] trans = new int[cols, rows];

        //    for (int i = 0;i < cols; i++)
        //    {
        //        for (int j = 0;j < rows; j++)
        //        {
        //            trans[i, j] = matrix[j, i];
        //        }
        //    }

        //    Console.WriteLine("Массив :");
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < cols; j++)
        //        {
        //            Console.Write(trans[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        // 6

        //static void Main(string[] args)
        //{
        //    Console.Write("Введите количество строк: ");
        //    int rows = int.Parse(Console.ReadLine());

        //    Console.Write("Введите количество столбцов: ");
        //    int cols = int.Parse(Console.ReadLine());

        //    int[,] matrix = new int[rows, cols];

        //    Console.WriteLine("Введите значения первой колонки через пробел:");
        //    string[] firstColumn = Console.ReadLine().Split(' ');
        //    for (int i = 0; i < rows; i++)
        //    {
        //        matrix[i, 0] = int.Parse(firstColumn[i]);
        //    }

        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < cols; j++)
        //        {
        //            matrix[i, j] = (int)Math.Pow(matrix[i, 0], j + 1);
        //        }
        //    }

        //    Console.WriteLine("Массив :");
        //    for (int i = 0; i < rows; i++)
        //    {
        //        for (int j = 0; j < cols; j++)
        //        {
        //            Console.Write(matrix[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        class cars
        {
            public string name;
            public int speed;
            public string date;

            public void info()
            {

            }
        }

        class student
        {
            public string name;
            public int age;
            public string range;
            public string date;

            public void info()
            {
                Console.WriteLine($"Имя ученика: {name}, возраст: {age}, класс: {range}, дата рождения: {date}");
            }
        }

        class BankAcc
        {
            public string name;
            public string pass;
            public int account_number;
            public double bill;

            public void info()
            {   
                Console.WriteLine("Введите имя и пароль");
                string indent_name = Console.ReadLine();
                string indent_pass = Console.ReadLine();
                if (indent_name == name && indent_pass == pass)
                {
                    Console.WriteLine($"Имя владельца: {name}, номер счета: {account_number}, кол-во: {bill}");
                }
                else
                {
                    return;
                }
                
            }

            public void DepositMoney()
            {
                Console.WriteLine("Введите имя и пароль");
                string indent_name = Console.ReadLine();
                string indent_pass = Console.ReadLine();
                if (indent_name == name && indent_pass == pass)
                {
                    Console.WriteLine("Введите сумму:");
                    double sum = double.Parse(Console.ReadLine());
                    bill += sum;
                    Console.WriteLine($"Имя владельца: {name}, номер счета: {account_number}, кол-во: {bill}");
                }
                else
                {
                    return;
                }

            }
        }


        static void Main(string[] args)
        {
            BankAcc Vasiliy = new BankAcc();
            Console.WriteLine("Введите имя:");
            Vasiliy.name = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            Vasiliy.pass = Console.ReadLine();
            Console.WriteLine("Введите номер счета:");
            Vasiliy.account_number = int.Parse(Console.ReadLine());

            Vasiliy.DepositMoney();
        }
    }
}
