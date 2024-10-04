using Castle.Components.DictionaryAdapter;
using DormitoryManagementSystem.Application.AccountingContext;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows.Transactions;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Transactions;
using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Infrastructure.AccountingContext;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDormitoryManagementStystem.UnitTests.Application.AccountingContext;

public class AccountServiceTest
{
    private IAccountRepository accountRepository;
    private AccountService accountService;

    private AccountId accountId;
    private decimal expectedBalance;

    public AccountServiceTest()
    {
        Currency currency = Currency.EUR;
        List<Entry> entries = new()
        {
            new Deposit(DepositId.Next(), new Money(100.25m, currency)),
            new Deposit(DepositId.Next(), new Money(300, currency)),
            new Withdrawal(WithdrawalId.Next(), new Money(100.75m, currency)),
            new Withdrawal(WithdrawalId.Next(), new Money(100, currency))
        };

        EntryList entryList = new(entries);

        accountId = new AccountId(Guid.NewGuid());
        List<Account> accounts = new()
        {
            new Account(accountId, new BankInformation(), new Administrator(), entryList)
        };

        accountRepository = new InMemoryAccountRepository(accounts);
        accountService = new AccountService(accountRepository);
        expectedBalance = 100.25m + 300 - 100.75m - 100;
    }

    [Fact]
    public void GetAccountService()
    {
        Money result = accountService.GetAccountBalance(accountId);
        result.Value.Should().Be(expectedBalance);
        result.Currency.Should().Be(Currency.EUR);
    }

    [Fact]
    public void GetAccountService_WhenWrongId_ShouldThrow()
    {
        Assert.Throws<Exception>(() =>
        {
            Money result = accountService.GetAccountBalance(AccountId.Next());
        });
    }
}
