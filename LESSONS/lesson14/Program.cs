using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\admin\C#\text.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл не найден: {filePath}");
                return;
            }

            string sourceText = File.ReadAllText(filePath);

            Console.Write("Введите сдвиг для шифра Цезаря: ");
            if (!int.TryParse(Console.ReadLine(), out int shift))
            {
                Console.WriteLine("Неверный сдвиг. Введите целое число.");
                return;
            }

            string encryptedText = EncryptCaesar(sourceText, shift);

            Console.WriteLine();
            Console.WriteLine("Исходный текст:");
            Console.WriteLine(sourceText);
            Console.WriteLine();
            Console.WriteLine("Зашифрованный текст:");
            Console.WriteLine(encryptedText);

            string encryptedFilePath = @"C:\Users\admin\C#\text.encrypted.txt";
            File.WriteAllText(encryptedFilePath, encryptedText);
            Console.WriteLine();
            Console.WriteLine($"Результат сохранен в файл: {encryptedFilePath}");
        }

        static string EncryptCaesar(string text, int shift)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                result[i] = ShiftChar(text[i], shift);
            }

            return new string(result);
        }

        static char ShiftChar(char c, int shift)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return ShiftInRange(c, shift, 'A', 26);
            }

            if (c >= 'a' && c <= 'z')
            {
                return ShiftInRange(c, shift, 'a', 26);
            }

            if (c >= 'А' && c <= 'Я')
            {
                return ShiftInRange(c, shift, 'А', 32);
            }

            if (c >= 'а' && c <= 'я')
            {
                return ShiftInRange(c, shift, 'а', 32);
            }

            if (c == 'Ё')
            {
                return ShiftSpecialYo(shift, true);
            }

            if (c == 'ё')
            {
                return ShiftSpecialYo(shift, false);
            }

            return c;
        }

        static char ShiftInRange(char c, int shift, char rangeStart, int alphabetSize)
        {
            int normalized = ((c - rangeStart) + shift) % alphabetSize;
            if (normalized < 0)
            {
                normalized += alphabetSize;
            }

            return (char)(rangeStart + normalized);
        }

        static char ShiftSpecialYo(int shift, bool upper)
        {
            const string upperAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            const string lowerAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

            string alphabet = upper ? upperAlphabet : lowerAlphabet;
            int index = alphabet.IndexOf(upper ? 'Ё' : 'ё');
            int normalized = (index + shift) % alphabet.Length;
            if (normalized < 0)
            {
                normalized += alphabet.Length;
            }

            return alphabet[normalized];
        }
    }
}
