using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.Common.MoneyModel;

public class Money
{
    public decimal Value { get; init; }

    public Currency Currency { get; init; }

    public bool IsZero => Value == 0 && Currency == Currency.Empty;   

    public static Money CreateNew(decimal amount, Currency currency) => 
        currency is Currency.Empty 
            ? throw new ArgumentException("Currency cannot be empty.") 
            : amount == 0
                ? throw new ArgumentException("Amount cannot be zero.")
                : new Money(amount, currency);

    public static Money ZeroMoney() => new Money(0, Currency.Empty);

    private Money(decimal amount, Currency currency)
    {
        Value = amount;
        Currency = currency;
    }

    /// <summary>
    /// Rounds the value to two digits, using Bankers' Rounding: 
    /// https://docs.alipayplus.com/alipayplus/alipayplus/reconcile_mpp/bank_rounding?role=MPP&product=Payment1&version=1.5.4
    /// </summary>
    public decimal GetRoundedValue()
    {
        return Math.Round(Value, 2, MidpointRounding.ToEven);
    }

    public static Money operator +(Money x, Money y)
    {
        if (x.IsZero)
            return y;
        if (y.IsZero)
            return x;

        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y); 

        return new Money(x.Value + y.Value, x.Currency);
    }

    public static Money operator -(Money x, Money y)
    {
        if (x.IsZero)
            return new Money(-y.Value, y.Currency);
        if (y.IsZero)
            return x;

        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y); 

        return new Money(x.Value - y.Value, x.Currency);
    }

    public static Money operator *(Money x, Money y)
    {
        if (x.IsZero || y.IsZero)
            return ZeroMoney();

        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y); 

        return new Money(x.Value * y.Value, x.Currency);
    }

    public static Money operator /(Money x, Money y)
    {
        if (x.IsZero)
            return ZeroMoney();
        if (y.IsZero)
            throw new DivideByZeroException("Cannot divide by zero money.");

        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y); 

        return new Money(x.Value / y.Value, x.Currency);
    }

    public static bool operator ==(Money x, Money y) => x.Equals(y);
    public static bool operator !=(Money x, Money y) => !(x == y);

    public static bool operator >(Money x, Money y)
    {
        if (x.IsZero)
            return 0 > y.Value;
        if (y.IsZero)
            return x.Value > 0;

        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y);

        return x.Value > y.Value;
    }

    public static bool operator <(Money x, Money y)
    {
        if (x.IsZero)
            return 0 < y.Value;
        if (y.IsZero)
            return x.Value < 0;

        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y);

        return x.Value < y.Value;
    }

    public Money DivideBy(int n)
    {
        if (IsZero)
            return ZeroMoney();
        
        return new(Value / n, Currency);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Money other)
        {
            if (IsZero && other.IsZero)
                return true;

            if (IsZero || other.IsZero)
                return false;

            if (Currency != other.Currency)
                throw CurrencyMismatchException.CreateExceptionForOperator(this, other);

            return Value == other.Value;
        }
        else
            return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Currency);
    }
}

public static class MoneyExtensions
{
    public static Money Sum(this IEnumerable<Money> moneyCollection) =>
        moneyCollection.Aggregate(Money.ZeroMoney(), (sum, money) => sum + money);
}
