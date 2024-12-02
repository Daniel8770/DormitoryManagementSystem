﻿namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
public class DepositId
{
    public Guid Value { get; init; }

    public static DepositId Next() => new DepositId(Guid.NewGuid());

    public DepositId(Guid value)
    {
        Value = value;
    }
}
