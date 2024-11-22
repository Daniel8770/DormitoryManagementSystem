using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;

public class Deposit : Entry
{
    public DepositId Id { get; init; }

    public Deposit(DepositId id, Money amount) : base(amount)
    {
        Id = id;
    }

    public override Money GetRelativeAmount()
    {
        return Money.CreateNew(Amount.Value, Amount.Currency);
    }
}
