using DormitoryManagementSystem.Domain.Common.Entities;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public class BookingId
{
    public int Value { get; init; }
    public BookingId(int value)
    {
        Value = value;
    }
}

public class Booking : Entity<int>
{
    public DateTime DateBooked { get; private set; }
    public TimePeriod TimePeriod { get; private set; }
    public Guid MemberId { get; private set; }
    public UnitId UnitId { get; private set; }

    public Booking(BookingId id, DateTime dateBooked, TimePeriod timePeriod, Guid member, UnitId unit) : base(id.Value)
    {
        DateBooked = dateBooked;
        TimePeriod =  timePeriod;
        MemberId = member;
        UnitId = unit;
    }

    public bool IsExpired() => DateTime.Now > TimePeriod.EndDate;  
}
