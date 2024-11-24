namespace DormitoryManagementSystem.Domain.Common.DomainEvents;

public abstract class DomainEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreationTime { get; init; } = DateTime.Now;
}
