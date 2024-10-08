using DormitoryManagementSystem.Domain.Common.MoneyModel;


namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;

public class Credit : Entry
{
    public CreditId Id { get; init; }

    public Credit(CreditId id, Money amount) : base(amount)
    {
        Id = id;
    }

    public override Money GetRelativeAmount()
    {
        return new Money(-Amount.Value, Amount.Currency);
    }
}
