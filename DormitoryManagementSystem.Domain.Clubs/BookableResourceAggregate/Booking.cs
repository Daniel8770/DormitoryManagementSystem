using DormitoryManagementSystem.Domain.Common.Entities;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public record BookingId(int Value) : EntityId<int>(Value);

public class Booking : Entity<BookingId>
{
    public BookableResourceId BookableResourceId { get; private set; }
    public DateTime DateBooked { get; private set; }
    public TimePeriod TimePeriod { get; private set; }
    public MemberId MemberId { get; private set; }
    public UnitId UnitId { get; private set; }

    public Booking(BookingId id, BookableResourceId bookableResourceId, DateTime dateBooked, TimePeriod timePeriod, MemberId member, UnitId unit) : base(id)
    {
        BookableResourceId = bookableResourceId;
        DateBooked = dateBooked;
        TimePeriod = timePeriod;
        MemberId = member;
        UnitId = unit;
    }

    public bool IsExpired() => DateTime.Now > TimePeriod.EndDate;
}
