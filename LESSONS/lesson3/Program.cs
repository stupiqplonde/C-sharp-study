using System;

namespace ConsoleApp1
{
    internal class Program
    {
        class Product
        {
            public string Name;
            public int Price;

            public Product(string name, int price)
            {
                Name = name;
                Price = price;
            }
        }

        class Products
        {
            private readonly List<Product> items = new List<Product>();

            public void Add(string name, int price)
            {
                items.Add(new Product(name, price));
            }

            public void PrintAll()
            {
                if (items.Count == 0)
                {
                    Console.WriteLine("Список товаров пуст.");
                    return;
                }

                Console.WriteLine("Товары:");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {items[i].Name} - {items[i].Price} руб за кг");
                }
            }
        }

        static void Main(string[] args)
        {
            Products db = new Products();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Добавить товар");
                Console.WriteLine("2 - Показать все товары");
                Console.WriteLine("0 - Выход");
                Console.Write("Выбор: ");

                string menu = Console.ReadLine() ?? string.Empty;
                Console.WriteLine();

                if (menu == "0")
                    break;

                if (menu == "2")
                {
                    db.PrintAll();
                    continue;
                }

                if (menu != "1")
                {
                    Console.WriteLine("Неверный выбор.");
                    continue;
                }

                Console.Write("Введите название товара: ");
                string name = (Console.ReadLine() ?? string.Empty).Trim();
                if (name.Length == 0)
                {
                    Console.WriteLine("Название не может быть пустым.");
                    continue;
                }

                while (true)
                {
                    try
                    {
                        Console.Write("Введите цену (только целое число >= 0): ");
                        string priceText = Console.ReadLine() ?? string.Empty;
                        int price = int.Parse(priceText);

                        if (price < 0)
                            throw new ArgumentException("Цена не может быть отрицательной.");

                        db.Add(name, price);
                        Console.WriteLine("Товар сохранён.");
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: цена должна быть целым числом (без дробей).");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Ошибка: слишком большое число.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
            }

            Console.WriteLine("Программа завершена.");
        }
    }
}
