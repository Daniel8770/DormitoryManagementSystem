using Castle.Components.DictionaryAdapter;
using DormitoryManagementSystem.Application.AccountingContext;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
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

        accountId = AccountId.Next();
        Account persistedAccount = new Account(accountId, new BankInformation(), new Administrator());
        persistedAccount.RegisterDeposit(new Money(100.25m, currency));
        persistedAccount.RegisterDeposit(new Money(300, currency));
        persistedAccount.RegisterWithdrawal(new Money(100.75m, currency));
        persistedAccount.RegisterWithdrawal(new Money(100, currency));

        List<Account> persistedAccounts = new()
        {
            persistedAccount
        };

        accountRepository = new InMemoryAccountRepository(persistedAccounts);
        accountService = new AccountService(accountRepository);
        expectedBalance = 100.25m + 300 - 100.75m - 100;
    }

    [Fact]
    public void GetAccountBalance()
    {
        Money result = accountService.GetAccountBalance(accountId);
        result.Value.Should().Be(expectedBalance);
        result.Currency.Should().Be(Currency.EUR);
    }

    [Fact]
    public void GetAccountBalance_WhenWrongIdProvided_ShouldThrow()
    {
        Assert.Throws<Exception>(() =>
        {
            Money result = accountService.GetAccountBalance(AccountId.Next());
        });
    }
}
