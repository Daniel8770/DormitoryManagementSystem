using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.Accounting.Entries;

public class Withdrawal : Entry
{
    public WithdrawalId Id { get; init; }

    public Withdrawal(WithdrawalId id, Money amount) : base(amount)
    {
        Id = id;
    }

    public override Money GetRelativeAmount()
    {
        return new Money(-Amount.Value, Amount.Currency);
    }
}
