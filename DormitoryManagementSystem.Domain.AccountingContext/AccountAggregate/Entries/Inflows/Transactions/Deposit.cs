using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows.Transactions;

public class Deposit : Inflow
{
    public DepositId Id { get; init; }

    public Deposit(DepositId id, Money amount) : base(amount)
    {
        Id = id;
    }
}
