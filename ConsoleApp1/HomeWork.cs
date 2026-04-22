using System;

// 1
Console.Write("Введите строку: ");
string str = Console.ReadLine();

if (str.ToLower().Contains("a"))
    Console.WriteLine("Буква 'a' есть.");
else
    Console.WriteLine("Буквы 'a' нет.");

// 2
Console.Write("Введите первую строку: ");
string s1 = Console.ReadLine();

Console.Write("Введите вторую строку: ");
string s2 = Console.ReadLine();

if (s1.ToLower() == s2.ToLower())
    Console.WriteLine("Строки одинаковые.");
else
    Console.WriteLine("Строки разные.");

// 3
Console.Write("\nВведите строку (вывод по буквам): ");
string str2 = Console.ReadLine() ?? "";

Console.WriteLine("Буквы по одной с новой строки:");
foreach (char ch in str2)
{
    Console.WriteLine(ch);
}

// 4
Console.Write("\nВведите строку (четные символы): ");
string str3 = Console.ReadLine() ?? "";

Console.WriteLine("Четные символы:");
for (int i = 1; i < str3.Length; i += 2) 
{
    Console.Write(str3[i]);
}

Console.WriteLine();
