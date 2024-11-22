using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

public record ExpenseId(Guid Value) : EntityId<Guid>(Value)
{
    public static ExpenseId Next() => new(Guid.NewGuid());
}

public class Expense : Entity<ExpenseId>
{
    public Money Amount { get; private set; }
    public Participant Creditor { get; private set; }
    public List<Participant> Debtors { get; private set; }

    public Expense(ExpenseId id, Money amount, Participant creditor, List<Participant> debtors) : base(id)
    {
        Amount = amount;
        Creditor = creditor;
        Debtors = debtors;
    }

}
