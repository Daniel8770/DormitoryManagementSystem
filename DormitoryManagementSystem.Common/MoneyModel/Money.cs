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
            throw CurrencyMismatchException.CreateExceptionWith(x, y); 

        return new Money(x.Value + y.Value, x.Currency);
    }

    public static Money operator -(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionWith(x, y); 

        return new Money(x.Value - y.Value, x.Currency);
    }

    public static Money operator *(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionWith(x, y); 

        return new Money(x.Value * y.Value, x.Currency);
    }

    public static Money operator /(Money x, Money y)
    {
        if (x.Currency != y.Currency)
            throw CurrencyMismatchException.CreateExceptionWith(x, y); 

        return new Money(x.Value / y.Value, x.Currency);
    }

}
