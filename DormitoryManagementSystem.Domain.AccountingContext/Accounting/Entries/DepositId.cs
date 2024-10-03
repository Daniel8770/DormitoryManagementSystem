using DormitoryManagementSystem.Domain.Common.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.Accounting.Entries;
public class DepositId
{
    public Guid Value { get; init; }

    public static DepositId Next() => new DepositId(Guid.NewGuid());

    public DepositId(Guid value)
    {
        Value = value;
    }
}
