using System;

namespace ConsoleApp1;

internal static class Program
{
    static void Main(string[] args)
    {
        var catalog = new Products();
        SeedCatalog(catalog);

        var cart = new Cart();
        var balance = new Balance(ReadInitialBalance());

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Магазин");
            Console.WriteLine($"Баланс: {balance.Amount} руб.");
            Console.WriteLine("1 — Показать каталог");
            Console.WriteLine("2 — Добавить товар в корзину (по номеру из каталога)");
            Console.WriteLine("3 — Показать корзину");
            Console.WriteLine("4 — Оформить покупку (списать с баланса и очистить корзину)");
            Console.WriteLine("5 — Пополнить баланс");
            Console.WriteLine("6 — Добавить товар в каталог (для администратора)");
            Console.WriteLine("0 — Выход");
            Console.Write("Выбор: ");

            string menu = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            try
            {
                switch (menu)
                {
                    case "0":
                        Console.WriteLine("До свидания.");
                        return;
                    case "1":
                        catalog.PrintCatalog();
                        break;
                    case "2":
                        AddToCartFromCatalog(catalog, cart);
                        break;
                    case "3":
                        cart.Print();
                        break;
                    case "4":
                        Checkout(cart, balance);
                        break;
                    case "5":
                        TopUpBalance(balance);
                        break;
                    case "6":
                        AddProductToCatalog(catalog);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    private static int ReadInitialBalance()
    {
        while (true)
        {
            Console.Write("Начальный баланс (целое число >= 0): ");
            string? text = Console.ReadLine();
            if (int.TryParse(text, out int value) && value >= 0)
                return value;
            Console.WriteLine("Введите неотрицательное целое число.");
        }
    }

    private static void SeedCatalog(Products catalog)
    {
        catalog.Add("Хлеб", 45);
        catalog.Add("Молоко", 89);
        catalog.Add("Сыр", 320);
        catalog.Add("Яблоки", 120);
        catalog.Add("Кофе", 450);
    }

    private static void AddToCartFromCatalog(Products catalog, Cart cart)
    {
        if (catalog.Count == 0)
        {
            Console.WriteLine("Каталог пуст — нечего добавлять.");
            return;
        }

        catalog.PrintCatalog();
        Console.Write("Номер товара из каталога: ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1)
        {
            Console.WriteLine("Нужен номер из списка (начиная с 1).");
            return;
        }

        if (!catalog.TryGetByDisplayIndex(index, out Product? product) || product is null)
        {
            Console.WriteLine("Товара с таким номером нет.");
            return;
        }

        cart.Add(product);
        Console.WriteLine($"Добавлено: {product.Name} ({product.Price} руб.)");
    }

    private static void Checkout(Cart cart, Balance balance)
    {
        if (cart.ItemCount == 0)
        {
            Console.WriteLine("Корзина пуста.");
            return;
        }

        int total = cart.Total;
        if (!balance.TryPay(total))
        {
            Console.WriteLine($"Недостаточно средств. Нужно {total} руб., на балансе {balance.Amount} руб. (до оплаты).");
            return;
        }

        Console.WriteLine($"Оплачено {total} руб. Остаток на балансе: {balance.Amount} руб.");
        cart.Clear();
    }

    private static void TopUpBalance(Balance balance)
    {
        Console.Write("Сумма пополнения: ");
        if (!int.TryParse(Console.ReadLine(), out int sum) || sum < 0)
        {
            Console.WriteLine("Введите неотрицательное целое число.");
            return;
        }

        balance.TopUp(sum);
        Console.WriteLine($"Баланс пополнен. Текущий баланс: {balance.Amount} руб.");
    }

    private static void AddProductToCatalog(Products catalog)
    {
        Console.Write("Название товара: ");
        string name = (Console.ReadLine() ?? string.Empty).Trim();
        if (name.Length == 0)
        {
            Console.WriteLine("Название не может быть пустым.");
            return;
        }

        Console.Write("Цена (целое >= 0): ");
        if (!int.TryParse(Console.ReadLine(), out int price) || price < 0)
        {
            Console.WriteLine("Некорректная цена.");
            return;
        }

        catalog.Add(name, price);
        Console.WriteLine("Товар добавлен в каталог.");
    }
}
