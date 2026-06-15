using System;

namespace ConsoleApp1;

internal static class Program
{
    static void Main(string[] args)
    {
        var savings = new SavingsAccount(5000);
        var checking = new CheckingAccount(1000);

        Console.WriteLine($"Накопительный счёт: {savings.Balance} руб.");
        Console.WriteLine($"Расчётный счёт: {checking.Balance} руб.");
        Console.WriteLine();

        Console.Write("Сумма перевода с накопительного на расчётный: ");
        string? line = Console.ReadLine();
        if (!int.TryParse(line, out int sum) || sum < 0)
        {
            Console.WriteLine("Нужно целое число не меньше нуля.");
            return;
        }

        bool done = savings.TransferTo(checking, sum);
        if (done)
            Console.WriteLine("Перевод выполнен.");
        else
            Console.WriteLine("Перевод не выполнен: не хватает средств или неверная сумма.");

        Console.WriteLine();
        Console.WriteLine($"Накопительный счёт: {savings.Balance} руб.");
        Console.WriteLine($"Расчётный счёт: {checking.Balance} руб.");
    }
}
