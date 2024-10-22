using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.Economy;

namespace DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
public class NewBalanceExpenseCreated : DomainEvent
{
    private BalanceExpense newExpense;

    public NewBalanceExpenseCreated(BalanceExpense newExpense)
    {
        this.newExpense = newExpense;
    }
}