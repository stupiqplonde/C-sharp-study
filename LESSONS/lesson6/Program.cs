using System;

namespace ConsoleApp1;

internal static class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Введите пароль (5 символов: 3 цифры, 1 буква, 1 спецсимвол): ");
            string password = Console.ReadLine() ?? string.Empty;

            if (IsPasswordValid(password, out string reason))
            {
                Console.WriteLine("Пароль подходит.");
                break;
            }

            Console.WriteLine(reason);
            Console.WriteLine("Попробуйте снова.");
        }
    }

    static bool IsPasswordValid(string password, out string reason)
    {
        if (password.Length != 5)
        {
            reason = "Пароль должен быть из 5 символов.";
            return false;
        }

        int digits = 0;
        int letters = 0;
        int specials = 0;

        foreach (char c in password)
        {
            if (char.IsWhiteSpace(c))
            {
                reason = "Пробелы нельзя использовать.";
                return false;
            }

            if (char.IsDigit(c))
                digits++;
            else if (char.IsLetter(c))
                letters++;
            else
                specials++;
        }

        if (digits != 3)
        {
            reason = "Нужно ровно 3 цифры.";
            return false;
        }

        if (specials != 1)
        {
            reason = "Нужен ровно 1 спецсимвол (не буква и не цифра).";
            return false;
        }

        if (letters != 1)
        {
            reason = "Нужна ровно 1 буква.";
            return false;
        }

        reason = string.Empty;
        return true;
    }
}
