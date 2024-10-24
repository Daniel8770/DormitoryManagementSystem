using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.Economy;

namespace DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
public class NewBalanceExpenseCreatedEvent : DomainEvent
{
    private BalanceExpense newExpense;

    public NewBalanceExpenseCreatedEvent(BalanceExpense newExpense)
    {
        this.newExpense = newExpense;
    }
}