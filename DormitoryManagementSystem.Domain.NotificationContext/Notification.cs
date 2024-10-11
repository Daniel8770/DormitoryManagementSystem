using DormitoryManagementSystem.Domain.Common.Entities;

namespace DormitoryManagementSystem.Domain.NotificationContext;

public class Notification : Entity
{
    public NotificationId Id { get; init; }
    public string Message { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;

    public Notification(NotificationId id, string message)
    {
        Id = id;
        Message = message;
    }
}
