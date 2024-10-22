using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy;
public class ExpenseId
{

    public Guid Value { get; init; }

    public static ExpenseId Next() => new(Guid.NewGuid());

    public ExpenseId(Guid value)
    {
        Value = value;
    }

}
