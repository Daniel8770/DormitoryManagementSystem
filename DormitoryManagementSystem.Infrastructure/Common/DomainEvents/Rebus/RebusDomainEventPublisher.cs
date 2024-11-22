using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using Microsoft.Data.SqlClient;
using Rebus.Bus;
using Rebus.Config.Outbox;
using Rebus.Transport;
using System.Data.Common;

namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents.Rebus;
public class RebusDomainEventPublisher : IDomainEventPublisher
{
    // TODO: Rebus Outbox pattern should be used, therefore this class should have reference to database transaction

    private IBus bus;
    private SqlTransaction? transaction;

    public RebusDomainEventPublisher(IBus bus)
    {
        this.bus = bus;
    }

    public void SetTransaction(SqlTransaction transaction)
    {
        this.transaction = transaction;
    }

    public async Task PublishAllEventsInEventStore()
    {
        if (transaction is null)
            throw new InvalidOperationException("Transaction is not set.");

        using RebusTransactionScope rebusScope = new();
        rebusScope.UseOutbox(transaction.Connection, transaction);

        await PublishEvents(DomainEventStore.Events);
        await rebusScope.CompleteAsync();

        DomainEventStore.ClearEventStore();
    }

    private async Task PublishEvents(IEnumerable<DomainEvent> events)
    {
        IEnumerable<Task> publishingTasks = events
            .Select(domainEvent => bus.Publish(domainEvent));

        await Task.WhenAll(publishingTasks);
    }
}
