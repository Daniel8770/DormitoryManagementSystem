using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.MoneyModel;

namespace DormitoryManagementSystem.Domain.AccountingContext.DomainEvents;
public class DisposableAmountLowerLimitBreached(AccountId id, decimal limit, Money disposableAmount) : DomainEvent
{
    public AccountId Id { get; private set; } = id;
    public decimal Limit { get; private set; } = limit;
    public Money DisposableAmount { get; private set; } = disposableAmount;
}
