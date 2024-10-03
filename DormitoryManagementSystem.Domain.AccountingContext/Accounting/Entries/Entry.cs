using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.Accounting.Entries;

public abstract class Entry : Entity
{
    public Money Amount { get; init; }
    public DateTime RegistrationDate { get; init; } = DateTime.Now;

    protected Entry(Money amount)
    {
        if (amount.Value < 0)
            throw new ArgumentException("Amount cannot be negative.");

        Amount = amount;
    }

    public abstract Money GetRelativeAmount();
}
