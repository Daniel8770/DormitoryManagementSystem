using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy;
public abstract class Expense 
{
    public ExpenseId Id { get; init; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Money Amount { get; private set; }
    public DateTime DateCreated { get; private set; }
    public ResidentId Creator { get; private set; }
    public ResidentId Creditor { get; private set; }
    public List<ResidentId> Debtors { get; private set; }

    protected Expense(
        ExpenseId id,
        string title,
        string description,
        Money amount,
        DateTime dateCreated,
        ResidentId creator,
        ResidentId creditor,
        List<ResidentId> debtors)
    {
        Id = id;
        Title = title;
        Description = description;
        Amount = amount;
        DateCreated = DateTime.Now;
        DateCreated = dateCreated;
        Creator = creator;
        Creditor = creditor;
        Debtors = debtors;
    }
}
