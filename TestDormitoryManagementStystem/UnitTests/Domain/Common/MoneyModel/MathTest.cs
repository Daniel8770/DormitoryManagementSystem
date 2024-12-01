﻿using DormitoryManagementSystem.Domain.Common.MoneyModel;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TestDormitoryManagementStystem.UnitTests.Domain.Common.MoneyModel;

public class MathTest
{
    [Theory]
    [InlineData(100, 200, Currency.DKK)]
    [InlineData(-1000, 300, Currency.DKK)]
    [InlineData(10000000, -1000000, Currency.DKK)]
    [InlineData(1.4736498, 3289.324897, Currency.DKK)]
    [InlineData(1000, -1000000.329048, Currency.DKK)]
    public void Addition(decimal amount1, decimal amount2, Currency currency)
    {
        Money money1 = Money.CreateNew(amount1, currency);
        Money money2 = Money.CreateNew(amount2, currency);

        Money result = money1 + money2;

        result.Value.Should().Be(money1.Value + money2.Value);
    }

    [Theory]
    [InlineData(100, 200, Currency.DKK)]
    [InlineData(-1000, 300, Currency.DKK)]
    [InlineData(10000000, -1000000, Currency.DKK)]
    [InlineData(1.4736498, 3289.324897, Currency.DKK)]
    [InlineData(1000, -1000000.329048, Currency.DKK)]
    public void Subtraction(decimal amount1, decimal amount2, Currency currency)
    {
        Money money1 = Money.CreateNew(amount1, currency);
        Money money2 = Money.CreateNew(amount2, currency);

        Money result = money1 - money2;

        result.Value.Should().Be(money1.Value - money2.Value);
    }

    [Theory]
    [InlineData(100, 200, Currency.DKK)]
    [InlineData(-1000, 300, Currency.DKK)]
    [InlineData(10000000, -1000000, Currency.DKK)]
    [InlineData(1.4736498, 3289.324897, Currency.DKK)]
    [InlineData(1000, -1000000.329048, Currency.DKK)]
    public void Multiplication(decimal amount1, decimal amount2, Currency currency)
    {
        Money money1 = Money.CreateNew(amount1, currency);
        Money money2 = Money.CreateNew(amount2, currency);

        Money result = money1 * money2;

        result.Value.Should().Be(money1.Value * money2.Value);
    }

    [Theory]
    [InlineData(100, 200, Currency.DKK)]
    [InlineData(-1000, 300, Currency.DKK)]
    [InlineData(10000000, -1000000, Currency.DKK)]
    [InlineData(1.4736498, 3289.324897, Currency.DKK)]
    [InlineData(1000, -1000000.329048, Currency.DKK)]
    public void Divition(decimal amount1, decimal amount2, Currency currency)
    {
        Money money1 = Money.CreateNew(amount1, currency);
        Money money2 = Money.CreateNew(amount2, currency);

        Money result = money1 / money2;

        result.Value.Should().Be(money1.Value / money2.Value);
    }

    [Theory]
    [InlineData(100, Currency.DKK, 200, Currency.USD)]
    public void WhenCurrencyMismatchInAddition_ShouldThrow(decimal amount1, Currency currency1, decimal amount2, Currency currency2)
    {
        Money money1 = Money.CreateNew(amount1, currency1);
        Money money2 = Money.CreateNew(amount2, currency2);

        Assert.Throws<CurrencyMismatchException>(() =>
        {
            Money result = money1 + money2;
        });
    }

    [Theory]
    [InlineData(100, Currency.DKK, 200, Currency.USD)]
    public void WhenCurrencyMismatchInSubtraction_ShouldThrow(decimal amount1, Currency currency1, decimal amount2, Currency currency2)
    {
        Money money1 = Money.CreateNew(amount1, currency1);
        Money money2 = Money.CreateNew(amount2, currency2);

        Assert.Throws<CurrencyMismatchException>(() =>
        {
            Money result = money1 - money2;
        });
    }

    [Theory]
    [InlineData(100, Currency.DKK, 200, Currency.USD)]
    public void WhenCurrencyMismatchInMultiplication_ShouldThrow(decimal amount1, Currency currency1, decimal amount2, Currency currency2)
    {
        Money money1 = Money.CreateNew(amount1, currency1);
        Money money2 = Money.CreateNew(amount2, currency2);

        Assert.Throws<CurrencyMismatchException>(() =>
        {
            Money result = money1 * money2;
        });
    }

    [Theory]
    [InlineData(100, Currency.DKK, 200, Currency.USD)]
    public void WhenCurrencyMismatchInDivition_ShouldThrow(decimal amount1, Currency currency1, decimal amount2, Currency currency2)
    {
        Money money1 = Money.CreateNew(amount1, currency1);
        Money money2 = Money.CreateNew(amount2, currency2);

        Assert.Throws<CurrencyMismatchException>(() =>
        {
            Money result = money1 / money2;
        });
    }

    [Theory]
    [InlineData(100, 4, 25)]
    [InlineData(375.25, 2, 187.625)]
    [InlineData(3.141, 1, 3.141)]
    [InlineData(3.141, 100, 0.03141)]
    public void DivideBy(decimal amount, int n, decimal expected)
    {
        Money money = Money.CreateNew(amount, Currency.DKK);
        Money result = money.DivideBy(n);
        result.Value.Should().Be(expected);  
    }

}
