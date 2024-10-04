using DormitoryManagementSystem.Domain.Common.MoneyModel;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Transactions;

public class Withdrawal : Outflow
{
    public WithdrawalId Id { get; init; }

    public Withdrawal(WithdrawalId id, Money amount) : base(amount)
    {
        Id = id;
    }
}
