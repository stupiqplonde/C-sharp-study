namespace ConsoleApp1;

public class BankAccount
{
    public int Balance { get; protected set; }

    public BankAccount(int startBalance)
    {
        if (startBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(startBalance));
        Balance = startBalance;
    }

    public void Deposit(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        Balance += amount;
    }

    protected bool TryTake(int amount)
    {
        if (amount < 0)
            return false;
        if (amount > Balance)
            return false;
        Balance -= amount;
        return true;
    }

    public bool TransferTo(BankAccount other, int amount)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));
        if (ReferenceEquals(this, other))
            return false;
        if (!TryTake(amount))
            return false;
        other.Deposit(amount);
        return true;
    }
}
