using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using Rebus.Bus;

namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents.Rebus;
public class RebusDomainEventPublisher : DomainEventPublisher
{
    // TODO: Rebus Outbox pattern should be used, therefore this class should have reference to database transaction

    private IBus bus;

    public RebusDomainEventPublisher(IBus bus)
    {
        this.bus = bus;
    }

    protected override async Task Publish(DomainEvent domainEvent)
    {
        await bus.Publish(domainEvent);
    }
}
