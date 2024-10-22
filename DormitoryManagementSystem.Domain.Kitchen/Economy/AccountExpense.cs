using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy;
public class AccountExpense : Expense
{
    public DateTime? DateApproved { get; private set; }
    public ResidentId? ApprovedBy { get; private set; }
    public bool Approved => DateApproved.HasValue;

    public static Expense CreateNew(string title, string description, Money amount, ResidentId creator, ResidentId creditor, List<ResidentId> debtors) =>
        new AccountExpense(ExpenseId.Next(), title, description, amount, DateTime.Now, null, null, creator, creditor, debtors);

    public static Expense CreateNewWhereCreatorIsCreditor(string title, string description, Money amount, ResidentId creator, List<ResidentId> debtors) =>
        new AccountExpense(ExpenseId.Next(), title, description, amount, DateTime.Now, null, null, creator, creator, debtors);

    protected AccountExpense(
        ExpenseId id,
        string title,
        string description,
        Money amount,
        DateTime dateCreated,
        DateTime? dateApproved,
        ResidentId? approvedBy,
        ResidentId creator,
        ResidentId creditor,
        List<ResidentId> debtors)
        : base(
            id,
            title,
            description,
            amount,
            dateCreated,
            creator,
            creditor,
            debtors)
    {
        DateApproved = dateApproved;
        ApprovedBy = approvedBy;
    }

    public void Approve(ResidentId approver)
    {
        DateApproved = DateTime.Now;
        ApprovedBy = approver;
    }
}
