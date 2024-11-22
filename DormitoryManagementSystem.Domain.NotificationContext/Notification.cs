using DormitoryManagementSystem.Domain.Common.Entities;

namespace DormitoryManagementSystem.Domain.NotificationContext;

public record class NotificationId(Guid Value) : EntityId<Guid>(Value)
{
    public static NotificationId Next() => new(Guid.NewGuid());
}

public class Notification : Entity<NotificationId>
{
    public string Message { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public Notification(NotificationId id, string message) : base(id)
    {
        Message = message;
    }
}
