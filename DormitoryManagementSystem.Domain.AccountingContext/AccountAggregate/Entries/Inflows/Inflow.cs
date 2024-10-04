
using DormitoryManagementSystem.Domain.Common.MoneyModel;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows;

public abstract class Inflow : Entry
{

    protected Inflow(Money amount) : base(amount)
    {
        
    }

    public override Money GetRelativeAmount()
    {
        return new Money(Amount.Value, Amount.Currency);
    }


}
