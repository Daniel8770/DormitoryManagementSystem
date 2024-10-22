using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;


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
