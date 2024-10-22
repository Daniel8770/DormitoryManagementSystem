using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.SharedExpensesContext;
internal class Expense : Entity
{
    public Guid Id { get; init; }
    public Money Amount { get; private set; }
    public Participant Creditor { get; private set; }
    public List<Participant> Debtors { get; private set; }

    public Expense(Guid id, Money amount, Participant creditor, List<Participant> debtors)
    {
        Id = id;
        Amount = amount;
        Creditor = creditor;
        Debtors = debtors;
    }

}
