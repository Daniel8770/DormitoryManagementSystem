using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
using DormitoryManagementSystem.Domain.Common.MoneyModel;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;

public class Withdrawal : Entry
{
    public WithdrawalId Id { get; init; }

    public Withdrawal(WithdrawalId id, Money amount) : base(amount)
    {
        Id = id;
    }

    public override Money GetRelativeAmount()
    {
        return Money.CreateNew(-Amount.Value, Amount.Currency);
    }
}
