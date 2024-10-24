using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.MoneyModel;

namespace DormitoryManagementSystem.Domain.AccountingContext.DomainEvents;
public class DisposableAmountLowerLimitBreachedEvent(AccountId id, decimal limit, Money disposableAmount) : DomainEvent
{
    public AccountId AccountId { get; private set; } = id;
    public decimal Limit { get; private set; } = limit;
    public Money DisposableAmount { get; private set; } = disposableAmount;
}
