using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Obligations;
public class CreditId
{
    public Guid Value { get; init; }

    public static CreditId Next() => new CreditId(Guid.NewGuid());

    public CreditId(Guid value)
    {
        Value = value;
    }
}
