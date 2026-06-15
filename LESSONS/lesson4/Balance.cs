namespace ConsoleApp1;

/// <summary>
/// Баланс пользователя для оплаты покупок.
/// </summary>
public class Balance
{
    public int Amount { get; private set; }

    public Balance(int initialAmount = 0)
    {
        if (initialAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(initialAmount), "Баланс не может быть отрицательным.");
        Amount = initialAmount;
    }

    public void TopUp(int sum)
    {
        if (sum < 0)
            throw new ArgumentOutOfRangeException(nameof(sum), "Сумма пополнения не может быть отрицательной.");
        Amount += sum;
    }

    /// <summary>
    /// Списывает сумму, если на балансе достаточно средств.
    /// </summary>
    public bool TryPay(int sum)
    {
        if (sum < 0)
            return false;
        if (sum > Amount)
            return false;
        Amount -= sum;
        return true;
    }
}
