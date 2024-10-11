using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Messages.Control;

namespace DormitoryManagementSystem.Application.KitchenContext;
public class KitchenAccountCreatedHandler : IHandleMessages<KitchenAccountCreated>
{
    public Task Handle(KitchenAccountCreated message)
    {
        return Task.CompletedTask;
    }
}

