namespace ConsoleApp1;

/// <summary>
/// Каталог товаров магазина.
/// </summary>
public class Products
{
    private readonly List<Product> _items = new();

    public int Count => _items.Count;

    public IReadOnlyList<Product> Items => _items;

    public void Add(string name, int price)
    {
        _items.Add(new Product(name, price));
    }

    /// <summary>
    /// index — номер в списке начиная с 1 (как в меню для пользователя).
    /// </summary>
    public bool TryGetByDisplayIndex(int index, out Product? product)
    {
        product = null;
        int i = index - 1;
        if (i < 0 || i >= _items.Count)
            return false;
        product = _items[i];
        return true;
    }

    public void PrintCatalog()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine("Каталог пуст.");
            return;
        }

        Console.WriteLine("Каталог товаров:");
        for (int i = 0; i < _items.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {_items[i].Name} — {_items[i].Price} руб.");
        }
    }
}
