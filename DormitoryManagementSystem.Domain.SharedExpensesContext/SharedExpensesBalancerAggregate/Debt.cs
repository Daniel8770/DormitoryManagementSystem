using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;


namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
public class Debt : Entity
{
    public Guid Id { get; init; }
    public Guid PaymentId { get; init; }
    public Participant Debtor { get; init; }
    public Money Amount { get; private set; }

    public Debt(Guid id, Guid paymentId, Participant debtor, Money amount)
    {
        Id = id;
        PaymentId = paymentId;
        Debtor = debtor;
        Amount = amount;
    }
}
