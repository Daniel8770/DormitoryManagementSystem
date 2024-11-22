using DormitoryManagementSystem.Application.AccountingContext;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Infrastructure.AccountingContext;
using FluentAssertions;

namespace TestDormitoryManagementStystem.UnitTests.Application.AccountingContext;

public class AccountServiceTest
{
    private IAccountRepository accountRepository;
    private AccountService accountService;

    private AccountId accountId;
    private decimal expectedBalance;
    private Currency currency;

    public AccountServiceTest()
    {
        currency = Currency.EUR;

        accountId = AccountId.Next();
        Account persistedAccount = new Account(accountId, new BankInformation(), new Administrator());
        persistedAccount.RegisterDeposit(Money.CreateNew(100.25m, currency));
        persistedAccount.RegisterDeposit(Money.CreateNew(300, currency));
        persistedAccount.RegisterWithdrawal(Money.CreateNew(100.75m, currency));
        persistedAccount.RegisterWithdrawal(Money.CreateNew(100, currency));

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

    [Fact]
    public void RegisterDepositOnAccount()
    {
        Money depositAmmount = Money.CreateNew(200.55m, currency);

        Money oldBalance = accountService.GetAccountBalance(accountId);
        accountService.RegisterDepositOnAccount(accountId, depositAmmount);
        Money newBalance = accountService.GetAccountBalance(accountId);

        newBalance.Value.Should().Be((oldBalance + depositAmmount).Value); 
    }

    [Fact]
    public void RegisterWithdrawalOnAccount()
    {
        Money withdrawalAmmount = Money.CreateNew(200.55m, currency);

        Money oldBalance = accountService.GetAccountBalance(accountId);
        accountService.RegisterWithdrawalOnAccount(accountId, withdrawalAmmount);
        Money newBalance = accountService.GetAccountBalance(accountId);

        newBalance.Value.Should().Be((oldBalance - withdrawalAmmount).Value);
    }
}
