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
    public bool IsRead { get; private set; } = false;
    public Guid RecipientId { get; init; }

    public Notification(NotificationId id, Guid recipientId, string message) : base(id)
    {
        RecipientId = recipientId;
        Message = message;
    }

    public void Read()
    {
        IsRead = true;
    }   
}
