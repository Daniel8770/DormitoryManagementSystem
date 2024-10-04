using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows;

public class Outflow : Entry
{
    public Outflow(Money amount) : base(amount)
    {
        
    }

    public override Money GetRelativeAmount()
    {
        return new Money(-Amount.Value, Amount.Currency);
    }
}
