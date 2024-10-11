using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.Common.MoneyModel;

public class Money : ValueObject
{
    public decimal Value { get; init; }

    public Currency Currency { get; init; }

    public Money(decimal amount, Currency currency)
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
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y); 

        return new Money(x.Value + y.Value, x.Currency);
    }

    public static Money operator -(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y); 

        return new Money(x.Value - y.Value, x.Currency);
    }

    public static Money operator *(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y); 

        return new Money(x.Value * y.Value, x.Currency);
    }

    public static Money operator /(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y); 

        return new Money(x.Value / y.Value, x.Currency);
    }

    public static bool operator ==(Money x, Money y)
    {
        if (x is null && y is null)
            return true;

        if (x is Money && y is null)
            return false;

        if (x is null && y is Money)
            return false;

        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y);

        return x.Value == y.Value;
    }

    public static bool operator !=(Money x, Money y)
    {
        return !(x == y);
    }

    public static bool operator >(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y);

        return x.Value > y.Value;
    }

    public static bool operator <(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionForOperator(x, y);

        return x.Value < y.Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Money other = (Money)obj;
        return this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Currency);
    }
}
