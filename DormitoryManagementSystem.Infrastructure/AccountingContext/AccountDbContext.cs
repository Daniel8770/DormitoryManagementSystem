using DormitoryManagementSystem.Domain.Common.Accounting;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.AccountingContext;

public class AccountDbContext : DbContext
{
    public DbSet<Account> Accounts { get; private set; }

}
