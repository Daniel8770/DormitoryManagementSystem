using DormitoryManagementSystem.Domain.Common.DomainEvents;


namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
public abstract class DomainEventPublisher
{
    public async Task PublishAllEventsInEventStore()
    {
        IEnumerable<Task> publishingTasks = DomainEventStore.Events
            .ToList()
            .Select(Publish);

        await Task.WhenAll(publishingTasks);

        DomainEventStore.ClearEventStore();
    }

    protected abstract Task Publish(DomainEvent domainEvent);
}
