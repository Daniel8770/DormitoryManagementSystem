using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;

public class DebitId
{
    public Guid Value { get; init; }

    public static DebitId Next() => new DebitId(Guid.NewGuid());

    public DebitId(Guid value)
    {
        Value = value;
    }
}
