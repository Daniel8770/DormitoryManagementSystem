using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.Common.Accounting;


namespace DormitoryManagementSystem.Infrastructure.AccountingContext;

public class InMemoryAccountRepository : IAccountRepository
{
    private List<Account> accounts = new();

    public InMemoryAccountRepository() { }

    public InMemoryAccountRepository(List<Account> accounts)
    {
        this.accounts = accounts;
    }

    public Account? GetById(AccountId id)
    {
        return accounts.Find(a => a.Id.Value == id.Value);
    }

    public void Save(Account account)
    {
        accounts.Add(account);
    }
}
