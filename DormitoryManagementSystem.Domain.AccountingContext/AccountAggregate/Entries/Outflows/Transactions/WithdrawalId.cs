using DormitoryManagementSystem.Domain.Common.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Transactions;
public class WithdrawalId
{
    public Guid Value { get; init; }

    public static WithdrawalId Next() => new WithdrawalId(Guid.NewGuid());

    public WithdrawalId(Guid value)
    {
        Value = value;
    }

}
