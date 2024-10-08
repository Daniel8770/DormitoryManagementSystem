using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using FluentAssertions;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;

namespace TestDormitoryManagementStystem.UnitTests.Domain.AccountingContext.AccountAggregate;

public class AccountTest
{
    [Fact]
    public void GetBalance()
    {
        Currency currency = Currency.EUR;

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator());

        account.RegisterDeposit(new Money(100.25m, currency));
        account.RegisterDeposit(new Money(1000.45m, currency));
        account.RegisterWithdrawal(new Money(100.75m, currency));
        account.RegisterDebit(new Money(100, currency));
        account.RegisterDebit(new Money(300, currency));
        account.RegisterCredit(new Money(3.145m, currency));
        account.RegisterCredit(new Money(200.58m, currency));

        decimal expectedBalance = 1000.45m + 100.25m - 100.75m;
        Money actualBalance = account.GetBalance();

        actualBalance.Value.Should().Be(new Money(expectedBalance, currency).Value);
    }

    [Fact]
    public void GetBalance_WhenMismatchingCurrencies_ShouldThrow()
    {
        Currency currency1 = Currency.DKK;
        Currency currency2 = Currency.USD;
        
        Account account = new Account(
                AccountId.Next(),
                new BankInformation(),
                new Administrator());


        Assert.Throws<CurrencyMismatchException>(() =>
        {
            account.RegisterDebit(new Money(100, currency1));
            account.RegisterCredit(new Money(100, currency2));
        });
    }

    [Theory]
    [InlineData(300)]
    [InlineData(3.148975)]
    [InlineData(1000.3248927432908243)]
    [InlineData(1000000000.328497243)]
    [InlineData(0.0000001)]
    public void RegisterDeposit_WhenDepositRegistered_BalanceShouldIncrease(decimal amount)
    {
        Currency currency = Currency.EUR;

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator());

        account.RegisterDeposit(new Money(100.25m, currency));
        account.RegisterWithdrawal(new Money(100.75m, currency));
        account.RegisterDebit(new Money(100, currency));
        account.RegisterDebit(new Money(300, currency));
        account.RegisterCredit(new Money(3.145m, currency));
        account.RegisterCredit(new Money(200.58m, currency));

        Money oldBalance = account.GetBalance();
        Money depositAmount = new Money(amount, currency);
        account.RegisterDeposit(depositAmount);
        Money newBalance = account.GetBalance();

        newBalance.Value.Should().Be((oldBalance + depositAmount).Value);
    }

    [Theory]
    [InlineData(300)]
    [InlineData(3.148975)]
    [InlineData(1000.3248927432908243)]
    [InlineData(1000000000.328497243)]
    [InlineData(0.0000001)]
    public void RegisterWithdrawal_WhenWithdrawalRegistered_BalanceShouldDecrease(decimal amount)
    {
        Currency currency = Currency.EUR;

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator());

        account.RegisterDeposit(new Money(100.25m, currency));
        account.RegisterWithdrawal(new Money(100.75m, currency));
        account.RegisterDebit(new Money(100, currency));
        account.RegisterDebit(new Money(300, currency));
        account.RegisterCredit(new Money(3.145m, currency));
        account.RegisterCredit(new Money(200.58m, currency));

        Money oldBalance = account.GetBalance();
        Money withdrawalAmount = new Money(amount, currency);
        account.RegisterWithdrawal(withdrawalAmount);
        Money newBalance = account.GetBalance();

        newBalance.Value.Should().Be((oldBalance - withdrawalAmount).Value);
    }

    [Fact]
    public void GetTotalCredit()
    {
        Currency currency = Currency.EUR;

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator());

        account.RegisterDeposit(new Money(100.25m, currency));
        account.RegisterWithdrawal(new Money(100.75m, currency));
        account.RegisterDebit(new Money(100, currency));
        account.RegisterDebit(new Money(300, currency));
        account.RegisterCredit(new Money(3.145m, currency));
        account.RegisterCredit(new Money(200.58m, currency));

        decimal expected = 3.145m + 200.58m;

        Money totalCredit = account.GetTotalCredit();

        totalCredit.Value.Should().Be(expected);
    }

    [Fact]
    public void GetTotalDebit()
    {
        Currency currency = Currency.EUR;

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator());

        account.RegisterDeposit(new Money(100.25m, currency));
        account.RegisterWithdrawal(new Money(100.75m, currency));
        account.RegisterDebit(new Money(100, currency));
        account.RegisterDebit(new Money(300, currency));
        account.RegisterCredit(new Money(3.145m, currency));
        account.RegisterCredit(new Money(200.58m, currency));

        decimal expected = 100 + 300;

        Money totalDebit = account.GetTotalDebit();

        totalDebit.Value.Should().Be(expected);
    }

    [Fact]
    public void GetDisposableAmount()
    {
        Currency currency = Currency.EUR;

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator());

        account.RegisterDeposit(new Money(100.25m, currency));
        account.RegisterWithdrawal(new Money(100.75m, currency));
        account.RegisterDebit(new Money(100, currency));
        account.RegisterDebit(new Money(300, currency));
        account.RegisterCredit(new Money(3.145m, currency));
        account.RegisterCredit(new Money(200.58m, currency));

        decimal expected = 100.25m - 100.75m - 3.145m - 200.58m;

        Money disposableAmount = account.GetDisposableAmount();

        disposableAmount.Value.Should().Be(expected);
    }

    [Fact]
    public void GetEquity()
    {
        Currency currency = Currency.EUR;

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator());

        account.RegisterDeposit(new Money(100.25m, currency));
        account.RegisterWithdrawal(new Money(100.75m, currency));
        account.RegisterDebit(new Money(100, currency));
        account.RegisterDebit(new Money(300, currency));
        account.RegisterCredit(new Money(3.145m, currency));
        account.RegisterCredit(new Money(200.58m, currency));

        decimal expected = 100.25m - 100.75m + 100 + 300 - 3.145m - 200.58m;

        Money equity = account.GetEquity();

        equity.Value.Should().Be(expected);
    }

}
