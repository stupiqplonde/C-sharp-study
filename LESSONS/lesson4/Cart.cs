namespace ConsoleApp1;

/// <summary>
/// Корзина: выбранные пользователем товары с ценами из каталога.
/// </summary>
public class Cart
{
    private readonly List<Product> _lines = new();

    public int Total => _lines.Sum(p => p.Price);

    public int ItemCount => _lines.Count;

    public void Add(Product product)
    {
        _lines.Add(new Product(product.Name, product.Price));
    }

    public void Clear() => _lines.Clear();

    public void Print()
    {
        if (_lines.Count == 0)
        {
            Console.WriteLine("Корзина пуста.");
            return;
        }

        Console.WriteLine("В корзине:");
        for (int i = 0; i < _lines.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_lines[i].Name} — {_lines[i].Price} руб.");
        }

        Console.WriteLine($"Итого: {Total} руб.");
    }
}
