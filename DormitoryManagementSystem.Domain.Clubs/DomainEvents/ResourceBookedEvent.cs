using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using DormitoryManagementSystem.Domain.Common.DomainEvents;

namespace DormitoryManagementSystem.Domain.ClubsContext.DomainEvents;
public class ResourceBookedEvent(int bookingId, Guid resourceId, int unitId, Guid memberId) 
    : DomainEvent
{
    public int BookingId { get; init; } = bookingId;
    public Guid ResourceId { get; init; } = resourceId;
    public int UnitId { get; init; } = unitId;
    public Guid MemberId { get; init;} = memberId;
}
