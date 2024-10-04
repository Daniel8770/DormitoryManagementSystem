using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows.Obligations;

public class Debit
{
    public DebitId Id { get; init; }
    public Money Amount { get; init; }

    public Debit(DebitId id, Money amount)
    {
        Id = id;
        Amount = amount;
    }
}
