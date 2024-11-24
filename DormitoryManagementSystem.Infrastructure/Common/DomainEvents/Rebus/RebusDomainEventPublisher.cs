using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using Microsoft.Data.SqlClient;
using Rebus.Bus;
using Rebus.Config.Outbox;
using Rebus.Transport;
using System.Data.Common;

namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents.Rebus;
internal class RebusDomainEventPublisher : IDomainEventPublisher
{
    private IBus bus;

    public RebusDomainEventPublisher(IBus bus)
    {
        this.bus = bus;
    }

    public async Task PublishAllEventsInEventStore()
    {
        await PublishEvents(DomainEventStore.Events);
        DomainEventStore.ClearEventStore();
    }

    private async Task PublishEvents(IEnumerable<DomainEvent> events)
    {
        IEnumerable<Task> publishingTasks = events
            .Select(domainEvent => bus.Publish(domainEvent));

        await Task.WhenAll(publishingTasks);
    }
}
