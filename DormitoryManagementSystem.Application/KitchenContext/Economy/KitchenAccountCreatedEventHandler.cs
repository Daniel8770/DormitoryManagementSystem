using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Messages.Control;

namespace DormitoryManagementSystem.Application.KitchenContext.Economy;
public class KitchenAccountCreatedEventHandler : IHandleMessages<KitchenAccountCreatedEvent>
{
    public Task Handle(KitchenAccountCreatedEvent message)
    {
        return Task.CompletedTask;
    }
}

