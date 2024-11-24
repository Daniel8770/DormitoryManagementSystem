using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using DormitoryManagementSystem.Domain.Common.DomainEvents;

namespace DormitoryManagementSystem.Domain.ClubsContext.DomainEvents;
public class ResourceBookedEvent(
    int bookingId,
    Guid resourceId,
    int unitId,
    string unitName,
    Guid memberId,
    string bookableResourceName,
    DateTime dateBooked)
    : DomainEvent
{
    public int BookingId { get; init; } = bookingId;
    public Guid ResourceId { get; init; } = resourceId;
    public int UnitId { get; init; } = unitId;
    public string UnitName { get; init; } = unitName;
    public Guid MemberId { get; init; } = memberId;
    public string ResourceName { get; init; } = bookableResourceName;
    public DateTime DateBooked { get; init; } = dateBooked;
}
