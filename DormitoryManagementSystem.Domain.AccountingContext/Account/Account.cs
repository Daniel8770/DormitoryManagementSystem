using DormitoryManagementSystem.Common.Aggregates;
using DormitoryManagementSystem.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.Account;

public class Account : AggregateRoot
{
    public AccountId Id { get; init; }

    public Account(AccountId id)
    {
        Id = id;
    }

}
