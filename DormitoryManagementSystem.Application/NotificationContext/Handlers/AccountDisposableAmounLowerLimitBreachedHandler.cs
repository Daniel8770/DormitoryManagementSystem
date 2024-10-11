
using Rebus.Handlers;
using DormitoryManagementSystem.Domain.AccountingContext.DomainEvents;

namespace DormitoryManagementSystem.Application.NotificationContext.Handlers;
public class AccountDisposableAmounLowerLimitBreachedHandler : IHandleMessages<DisposableAmountLowerLimitBreached>
{
    public Task Handle(DisposableAmountLowerLimitBreached message)
    {
        return Task.CompletedTask;
    }
}
