using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;

public class Debit : Entry
{
    public DebitId Id { get; init; }

    public Debit(DebitId id, Money amount) : base(amount)
    {
        Id = id;
    }

    public override Money GetRelativeAmount()
    {
        return Money.CreateNew(Amount.Value, Amount.Currency);
    }
}
