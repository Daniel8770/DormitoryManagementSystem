using Microsoft.Data.SqlClient;


namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
public interface IDomainEventPublisher
{
    Task PublishAllEventsInEventStore();
    void SetTransaction(SqlTransaction transaction);
}
