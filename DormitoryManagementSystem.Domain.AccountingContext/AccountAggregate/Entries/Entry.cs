using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;

public abstract class Entry : Entity
{
    public Money Amount { get; init; }

    // TODO: This is bad since the object will get a new data when constructed through EF - retreieved from persistence.
    public DateTime RegistrationDate { get; init; } = DateTime.Now;

    protected Entry(Money amount)
    {
        if (amount.Value < 0)
            throw new ArgumentException("Amount cannot be negative.");

        Amount = amount;
    }

    public abstract Money GetRelativeAmount();
}
