using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.MoneyModel;

namespace DormitoryManagementSystem.Application.AccountingContext;

public class AccountService : IAccountService
{
    private IAccountRepository accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public Money GetAccountBalance(AccountId id)
    {
        Account account = GetAccount(id);
        return account.GetBalance();
    }

    public void RegisterDepositOnAccount(AccountId id, Money amount)
    {
        Account account = GetAccount(id);
        account.RegisterDeposit(amount);
        accountRepository.SaveOrUpdate(account);
    }

    public void RegisterWithdrawalOnAccount(AccountId id, Money amount)
    {
        Account account = GetAccount(id);
        account.RegisterWithdrawal(amount);
        accountRepository.SaveOrUpdate(account);
    }

    public void RegisterDebitOnAccount(AccountId id, Money amount)
    {
        Account account = GetAccount(id);
        account.RegisterDebit(amount);
        accountRepository.SaveOrUpdate(account);
    }

    public void RegisterCreditOnAccount(AccountId id, Money amount)
    {
        Account account = GetAccount(id);
        account.RegisterCredit(amount);
        accountRepository.SaveOrUpdate(account);
    }

    private Account GetAccount(AccountId id)
    {
        return accountRepository.GetById(id) ??
            throw new Exception("Not found");
    }
}
