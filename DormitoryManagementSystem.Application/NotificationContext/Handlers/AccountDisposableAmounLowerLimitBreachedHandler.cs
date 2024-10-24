
using Rebus.Handlers;
using DormitoryManagementSystem.Domain.AccountingContext.DomainEvents;

namespace DormitoryManagementSystem.Application.NotificationContext.Handlers;
public class AccountDisposableAmounLowerLimitBreachedHandler : IHandleMessages<DisposableAmountLowerLimitBreachedEvent>
{
    public Task Handle(DisposableAmountLowerLimitBreachedEvent message)
    {
        return Task.CompletedTask;
    }
}
