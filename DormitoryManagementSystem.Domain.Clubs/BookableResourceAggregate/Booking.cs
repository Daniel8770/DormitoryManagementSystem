using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public class BookingId
{
    public int Value { get; init; }
    public BookingId(int value)
    {
        Value = value;
    }
}


public abstract record TimePeriod : ValueObject
{
    public DateTime StartDate { get; init; }
    public abstract DateTime EndDate { get; init; }

    public TimePeriod(DateTime startDate)
    {
        StartDate = startDate;
    }

    public bool Overlaps(TimePeriod other) =>
        StartDate < other.EndDate && other.StartDate < EndDate;
}

public record DaysTimePeriod : TimePeriod
{
    public override DateTime EndDate { get; init; }
    public int Days { get; init; }

    public DaysTimePeriod(DateTime startDate, int days) : base(startDate)
    {
        Days = days;
        EndDate = startDate.AddDays(days);
    }
}

public record HoursTimePeriod : TimePeriod
{
    public override DateTime EndDate { get; init; }
    public int Hours { get; init; }

    public HoursTimePeriod(DateTime startDate, int hours) : base(startDate)
    {
        Hours = hours;
        EndDate = startDate.AddHours(hours);
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
