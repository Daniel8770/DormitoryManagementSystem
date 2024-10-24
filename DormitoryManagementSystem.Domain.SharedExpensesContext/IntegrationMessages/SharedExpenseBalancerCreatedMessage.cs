using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

namespace DormitoryManagementSystem.Domain.SharedExpensesContext.IntegrationMessages;
public class SharedExpenseBalancerCreatedMessage
{
    public Guid SharedExpensesId { get; init; }
    public Guid KitchenBalanceId { get; init; }

    public SharedExpenseBalancerCreatedMessage(Guid id, Guid kitchenBalanceId)
    {
        SharedExpensesId = id;
        KitchenBalanceId = kitchenBalanceId;
    }
}