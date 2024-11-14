using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;


namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

// TODO: Maybe value object?
public class Debt 
{
    public Guid Id { get; init; }
    public Guid ExpenseId { get; init; }
    public Participant Debtor { get; init; }
    public Money Amount { get; private set; }

    public Debt(Guid id, Guid paymentId, Participant debtor, Money amount)
    {
        Id = id;
        ExpenseId = paymentId;
        Debtor = debtor;
        Amount = amount;
    }
}
