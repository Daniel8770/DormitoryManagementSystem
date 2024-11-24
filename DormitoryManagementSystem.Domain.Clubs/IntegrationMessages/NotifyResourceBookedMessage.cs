

namespace DormitoryManagementSystem.Domain.ClubsContext.IntegrationMessages;

public class NotifyResourceBookedMessage(
    Guid recipientId,
    Guid resourceId,
    string resourceName,
    int bookingId,
    int unitId,
    string unitName,
    DateTime dateBooked)
{
    public Guid RecipientId { get; init; } = recipientId;
    public Guid ResourceId { get; init; } = resourceId;
    public int BookingId { get; init; } = bookingId;
    public int UnitId { get; init; } = unitId;
    public string UnitName { get; init; } = unitName;
    public string ResourceName { get; init; } = resourceName;
    public DateTime DateBooked { get; init; } = dateBooked;
}