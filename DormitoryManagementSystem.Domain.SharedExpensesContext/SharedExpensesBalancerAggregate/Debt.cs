using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.Common.ValueObjects;


namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

public record Debt : ValueObject
{
    public ExpenseId ExpenseId { get; init; }
    public ParticipantId Debtor { get; init; }
    public Money Amount { get; private set; }

    public Debt(ExpenseId expenseId, ParticipantId debtor, Money amount)
    {
        ExpenseId = expenseId;
        Debtor = debtor;
        Amount = amount;
    }
}
