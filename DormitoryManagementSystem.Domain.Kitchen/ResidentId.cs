using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext;
public class ResidentId
{
    public Guid Value { get; init; }

    public static ResidentId Next() => new(Guid.NewGuid());

    private ResidentId(Guid value)
    {
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is ResidentId residentId)
            return residentId.Value == Value;

        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();
}
