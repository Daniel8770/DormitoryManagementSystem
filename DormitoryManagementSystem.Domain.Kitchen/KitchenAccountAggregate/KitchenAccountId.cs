﻿
namespace DormitoryManagementSystem.Domain.Kitchen.KitchenAccountAggregate;
public class KitchenAccountId
{
    public Guid Value { get; init; }

    public static KitchenAccountId Next() => new(Guid.NewGuid());
    private KitchenAccountId(Guid value)
    {
        Value = value;
    }
}