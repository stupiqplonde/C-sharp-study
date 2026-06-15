using System;
using System.Linq;

namespace ConsoleApp1
{
    record Point(int x, int y);

    record OrderItem(string Name, decimal Price);

    record Order(List<OrderItem> Products, decimal Sum, DateTime Date)
    {
        public decimal GetTotal() => Products.Sum(p => p.Price);
    }

    record Book(string Title, string Author, int Year);

    class Pt(int X, int Y);
    
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Point p1 = new Point(1, 2);
            Point p2 = new Point(3, 4);
            Pt pt1 = new Pt(1, 2);
            Pt pt2 = new Pt(3, 4);
            Point p3 = new Point(2, 3);
            Point p4 = p1 with {x=20};
            Console.WriteLine(p1 == p2);

            var products = new List<OrderItem>
            {
                new("Хлеб", 50),
                new("Молоко", 80),
                new("Сыр", 200)
            };

            Order order = new(products, products.Sum(p => p.Price), DateTime.Now);
            Console.WriteLine($"Дата: {order.Date:d}");
            Console.WriteLine($"Товаров: {order.Products.Count}");
            Console.WriteLine($"Сумма: {order.Sum} руб.");
            Console.WriteLine($"GetTotal: {order.GetTotal()} руб.");

            Book[] books =
            {
                new("Герой нашего времени", "Лермонтов", 1837),
                new("Война и мир", "Толстой", 1869),
                new("Преступление и наказание", "Достоевский", 1866),
                new("Анна Каренина", "Толстой", 1877),
                new("Идиот", "Достоевский", 1869),
                new("Морфий", "Булгаков", 1926)
            };

            Console.WriteLine("\nпоиск по автору: Толстой");
            foreach (Book book in FindByAuthor(books, "Толстой"))
                Console.WriteLine($"{book.Title} ({book.Year})");

            Console.WriteLine("\nсортировка по году");
            foreach (Book book in SortByYear(books))
                Console.WriteLine($"{book.Year}: {book.Title} — {book.Author}");

        //     Bank bank = new Bank();
        //
        //     bank.BalanceChanged += (b, balance) =>
        //         Console.WriteLine($"Баланс изменён: {balance} руб.");
        //
        //     while (true) 
        //     {
        //         Console.WriteLine();
        //         Console.WriteLine("1 — пополнить, 2 — снять, 3 — баланс, 0 — выход");
        //         Console.Write("Выберите операцию: ");
        //
        //         if (!int.TryParse(Console.ReadLine(), out int choice))
        //         {
        //             Console.WriteLine("Введите число");
        //             continue;
        //         }
        //
        //         switch (choice)
        //         {
        //             case 1:
        //                 Console.Write("Сумма пополнения: ");
        //                 if (decimal.TryParse(Console.ReadLine(), out decimal deposit))
        //                     bank.Deposit(deposit);
        //                 else
        //                     Console.WriteLine("Неверная сумма");
        //                 break;
        //
        //             case 2:
        //                 Console.Write("Сумма снятия: ");
        //                 if (decimal.TryParse(Console.ReadLine(), out decimal withdraw))
        //                     bank.Withdraw(withdraw);
        //                 else
        //                     Console.WriteLine("Неверная сумма");
        //                 break;
        //
        //             case 3:
        //                 Console.WriteLine($"Текущий баланс: {bank.Balance} руб.");
        //                 break;
        //
        //             case 0:
        //                 return;
        //
        //             default:
        //                 Console.WriteLine("Нет такой операции");
        //                 break;
        //         }
        //     }
        //
        }

        static IEnumerable<Book> FindByAuthor(IEnumerable<Book> books, string author) =>
            books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

        static IEnumerable<Book> SortByYear(IEnumerable<Book> books) =>
            books.OrderBy(b => b.Year);
    }

    class Bank
    {
        public decimal Balance { get; private set; }

        public event Action<Bank, decimal> BalanceChanged;

        public void Deposit(decimal amount)
        {
            Balance += amount;
            BalanceChanged?.Invoke(this, Balance);
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Недостаточно средств");
                return;
            }

            Balance -= amount;
            BalanceChanged?.Invoke(this, Balance);
        }
    }
}
