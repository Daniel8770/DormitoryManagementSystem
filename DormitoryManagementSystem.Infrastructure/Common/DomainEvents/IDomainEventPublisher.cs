using DormitoryManagementSystem.Domain.Common.DomainEvents;
using Microsoft.Data.SqlClient;


namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
public interface IDomainEventPublisher
{
    Task PublishEvents(IEnumerable<DomainEvent> events);
}
