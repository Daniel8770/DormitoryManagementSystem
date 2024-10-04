using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using FluentAssertions;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows.Transactions;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Transactions;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows.Obligations;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Obligations;

namespace TestDormitoryManagementStystem.UnitTests.Domain.AccountingContext.AccountAggregate;

public class AccountTest
{
    [Fact]
    public void GetBalance()
    {
        Currency currency = Currency.DKK;

        List<Entry> entries = new()
        {
            new Deposit(DepositId.Next(), new Money(100.25m, currency)),
            new Deposit(DepositId.Next(), new Money(300, currency)),
            new Withdrawal(WithdrawalId.Next(), new Money(100.75m, currency)),
            new Withdrawal(WithdrawalId.Next(), new Money(100, currency))
        };

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator(),
            new EntryList(entries));

        Money actualBalance = account.GetBalance();
        decimal expectedBalance = 100.25m + 300 - 100.75m - 100;

        actualBalance.Value.Should().Be(new Money(expectedBalance, currency).Value);
    }

    [Fact]
    public void GetBalance_WhenMismatchingCurrencies_ShouldThrow()
    {
        Currency currency1 = Currency.DKK;
        Currency currency2 = Currency.USD;

        List<Entry> entries = new()
        {
            new Deposit(DepositId.Next(), new Money(100.25m, currency1)),
            new Deposit(DepositId.Next(), new Money(300, currency1)),
            new Withdrawal(WithdrawalId.Next(), new Money(100.75m, currency2)),
            new Withdrawal(WithdrawalId.Next(), new Money(100, currency2))
        };

        Assert.Throws<CurrencyMismatchException>(() =>
        {
            Account account = new Account(
                AccountId.Next(),
                new BankInformation(),
                new Administrator(),
                new EntryList(entries));
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
        Currency currency = Currency.USD;

        Money depositAmount = new Money(amount, currency);

        List<Entry> entries = new()
        {
            new Deposit(DepositId.Next(), new Money(100.25m, currency)),
            new Withdrawal(WithdrawalId.Next(), new Money(100.75m, currency))
        };

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator(),
            new EntryList(entries));

        Money oldBalance = account.GetBalance();
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
        Currency currency = Currency.USD;

        Money withdrawalAmount = new Money(amount, currency);

        List<Entry> entries = new()
        {
            new Deposit(DepositId.Next(), new Money(100.25m, currency)),
            new Withdrawal(WithdrawalId.Next(), new Money(100.75m, currency))
        };

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator(),
            new EntryList(entries));

        Money oldBalance = account.GetBalance();
        account.RegisterWithdrawal(withdrawalAmount);
        Money newBalance = account.GetBalance();

        newBalance.Value.Should().Be((oldBalance - withdrawalAmount).Value);
    }

    [Fact]
    public void GetTotalCredit()
    {
        Currency currency = Currency.EUR;

        List<Credit> credits = new()
        {
            new Credit(CreditId.Next(), new Money(100, currency)),
            new Credit(CreditId.Next(), new Money(300, currency)),
            new Credit(CreditId.Next(), new Money(3.145m, currency)),
            new Credit(CreditId.Next(), new Money(200.58m, currency))
        };

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator(),
            credits);

        decimal expected = 100 + 300 + 3.145m + 200.58m;

        Money totalCredit = account.GetTotalCredit();

        totalCredit.Value.Should().Be(expected);
    }

    [Fact]
    public void GetTotalDebit()
    {
        Currency currency = Currency.EUR;

        List<Debit> debits = new()
        {
            new Debit(DebitId.Next(), new Money(100, currency)),
            new Debit(DebitId.Next(), new Money(300, currency)),
            new Debit(DebitId.Next(), new Money(3.145m, currency)),
            new Debit(DebitId.Next(), new Money(200.58m, currency))
        };

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator(),
            debits);

        decimal expected = 100 + 300 + 3.145m + 200.58m;

        Money totalDebit = account.GetTotalDebit();

        totalDebit.Value.Should().Be(expected);
    }

    [Fact]
    public void GetDisposableAmount()
    {
        Currency currency = Currency.EUR;

        List<Credit> credits = new()
        {
            new Credit(CreditId.Next(), new Money(3.145m, currency)),
            new Credit(CreditId.Next(), new Money(200.58m, currency))
        };

        List<Debit> debits = new()
        {
            new Debit(DebitId.Next(), new Money(100, currency)),
            new Debit(DebitId.Next(), new Money(300, currency)),
        };

        List<Entry> entries = new()
        {
            new Deposit(DepositId.Next(), new Money(100.25m, currency)),
            new Withdrawal(WithdrawalId.Next(), new Money(100.75m, currency))
        };

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator(),
            new(entries),
            debits,
            credits);

        decimal expected = 100.25m - 100.75m - 3.145m - 200.58m;

        Money disposableAmount = account.GetDisposableAmount();

        disposableAmount.Value.Should().Be(expected);
    }

    [Fact]
    public void GetEquity()
    {
        Currency currency = Currency.EUR;

        List<Credit> credits = new()
        {
            new Credit(CreditId.Next(), new Money(3.145m, currency)),
            new Credit(CreditId.Next(), new Money(200.58m, currency))
        };

        List<Debit> debits = new()
        {
            new Debit(DebitId.Next(), new Money(100, currency)),
            new Debit(DebitId.Next(), new Money(300, currency)),
        };

        List<Entry> entries = new()
        {
            new Deposit(DepositId.Next(), new Money(100.25m, currency)),
            new Withdrawal(WithdrawalId.Next(), new Money(100.75m, currency))
        };

        Account account = new Account(
            AccountId.Next(),
            new BankInformation(),
            new Administrator(),
            new(entries),
            debits,
            credits);

        decimal expected = 100.25m - 100.75m + 100 + 300 - 3.145m - 200.58m;

        Money equity = account.GetEquity();

        equity.Value.Should().Be(expected);
    }

}
