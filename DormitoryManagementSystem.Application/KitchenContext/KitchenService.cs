using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAccountAggregate;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Application.KitchenContext;
public class KitchenService
{
    DomainEventPublisher domainEventPublisher;

    public KitchenService(DomainEventPublisher domainEventPublisher)
    {
        this.domainEventPublisher = domainEventPublisher;
    }

    public async Task AddKitchenAccount()
    {
        DomainEventStore.Raise(new KitchenAccountCreated());
        await domainEventPublisher.PublishAllEventsInEventStore();
    }
}
