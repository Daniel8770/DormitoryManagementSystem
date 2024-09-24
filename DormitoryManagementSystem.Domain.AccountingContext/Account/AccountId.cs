using DormitoryManagementSystem.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.Account;

public class AccountId
{
    public Guid Value { get; init; }

    public static AccountId Next() => new AccountId(Guid.NewGuid());

    public AccountId(Guid value)
    {
        Value = value;
    }
}
