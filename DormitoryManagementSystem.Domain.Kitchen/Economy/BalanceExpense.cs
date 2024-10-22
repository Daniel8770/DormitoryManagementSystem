using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy;
public class BalanceExpense : Expense
{
    public static BalanceExpense CreateNew(
        string title,
        string description,
        Money amount,
        ResidentId creator,
        ResidentId creditor,
        List<ResidentId> debtors)
    {

        BalanceExpense newExpense = new BalanceExpense(
            ExpenseId.Next(),
            title,
            description,
            amount,
            DateTime.Now,
            creator,
            creditor,
            debtors);

        DomainEventStore.Raise(new NewBalanceExpenseCreated(newExpense));

        return newExpense;
    }

    public static BalanceExpense CreCreateNewWhereCreatorIsCreditorateNew(
        string title,
        string description,
        Money amount,
        ResidentId creator,
        List<ResidentId> debtors) =>
        new BalanceExpense(
            ExpenseId.Next(),
            title,
            description,
            amount,
            DateTime.Now,
            creator,
            creator,
            debtors);

    protected BalanceExpense(
        ExpenseId id,
        string title,
        string description,
        Money amount,
        DateTime dateCreated,
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

    }
}
