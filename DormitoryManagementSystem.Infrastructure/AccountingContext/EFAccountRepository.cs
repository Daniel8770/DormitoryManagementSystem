using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.Entities;

namespace DormitoryManagementSystem.Infrastructure.AccountingContext;
public class EFAccountRepository : IAccountRepository
{
    private AccountDbContext dbContext;
    
    public EFAccountRepository(AccountDbContext dbContext)
    {

        this.dbContext = dbContext;
    }

    public Account? GetById(AccountId id)
    {
        return dbContext.Accounts.Find(id.Value);
    }

    public void SaveOrUpdate(Account account)
    {
        var transaction = dbContext.Database.BeginTransaction();

        Account? foundAccount = dbContext.Accounts.Find(account.Id.Value);

        if (foundAccount is null)
            dbContext.Accounts.Add(account);
        else
        {
            dbContext.Accounts.Remove(foundAccount!);
            dbContext.Accounts.Add(account);
        }

        dbContext.SaveChanges();
        transaction.Commit();
    }
}
