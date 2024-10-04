using DormitoryManagementSystem.Domain.Common.MoneyModel;


namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Obligations;

public class Credit
{
    public CreditId Id { get; init; }
    public Money Amount { get; init; }

    public Credit(CreditId id, Money amount)
    {
        Id = id;
        Amount = amount;
    }
}
