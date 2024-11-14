using DormitoryManagementSystem.Domain.Clubs.BookableResourceAggregate;
using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public class BookableResourceId
{
    public Guid Value { get; init; }
    public static BookableResourceId Next() => new BookableResourceId(Guid.NewGuid());
    public BookableResourceId(Guid value)
    {
        Value = value;
    }
}

public class BookableResource : AggregateRoot<Guid>
{
    public Name Name { get; private set; }
    public DateTime OpenDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Rules Rules { get; private set; }
    public ImmutableList<Unit> Units => units.ToImmutableList();
    public ImmutableList<Booking> Bookings => bookings.ToImmutableList();

    private List<Unit> units;
    private List<Booking> bookings;

    public static BookableResource CreateNew(string name, string rules, DateTime openDate, DateTime endDate) =>
        new BookableResource(BookableResourceId.Next(), name, openDate, endDate, new Rules(rules), new(), new());

    public static BookableResource CreateNewWithMaximumBookingsPerMember(string name, string rules, int maxBookingsPerMember, DateTime openDate, DateTime endDate) =>
       new BookableResource(BookableResourceId.Next(), name, openDate, endDate, new MaxBookingsPerMemberRules(rules, maxBookingsPerMember), new(), new());

    private BookableResource(BookableResourceId id, string name, DateTime openDate, DateTime endDate, Rules rules, List<Unit> units, List<Booking> bookings) 
        : base(id.Value)
    {
        Name = new Name(name);
        OpenDate = openDate;
        EndDate = endDate;
        Rules = rules;
        this.units = units;
        this.bookings = bookings;
    }

    public void BookDays(Guid memberId, UnitId unitId, DateTime date, int days)
    {
        Booking newBooking = CreateNewBooking(memberId, unitId, date, new DaysTimePeriod(date, days));
        Book(newBooking);
    }

    public void BookHours(Guid memberId, UnitId unitId, DateTime date, int hours)
    {
        Booking newBooking = CreateNewBooking(memberId, unitId, date, new HoursTimePeriod(date, hours));
        Book(newBooking);
    }

    private Booking CreateNewBooking(Guid memberId, UnitId unitId, DateTime date, TimePeriod timePeriod) 
    {
        return new Booking(
            new(GetNextBookingId()),
            DateTime.Now,
            timePeriod,
            memberId,
            unitId);
    }

    private void Book(Booking newBooking)
    {
        bool resourceContainsUnit = units.Find(unit => unit.Id == newBooking.UnitId.Value) is null ? false : true;

        if (!resourceContainsUnit)
            throw new DomainException($"Cannot book unit {newBooking.UnitId} for member {newBooking.MemberId} because the unit is not part of the bookable resource.");

        Booking? sameBooking = bookings.Find(booking => booking.UnitId.Value == newBooking.UnitId.Value && newBooking.TimePeriod.Overlaps(booking.TimePeriod));

        if (sameBooking is not null)
            throw new DomainException($"Cannot book unit {newBooking.UnitId} for member {newBooking.MemberId} because there is another booking in the same time period.");

        if (Rules is MaxBookingsPerMemberRules rules)
        {
            IEnumerable<Booking> membersOtherBookings = bookings.Where(booking => booking.MemberId == newBooking.MemberId && !booking.IsExpired());
            if (membersOtherBookings.Count() >= rules.MaxBookingsPerMember)
                throw new DomainException($"Cannot book unit {newBooking.UnitId} for member {newBooking.MemberId} because the member has reached the maximum number of bookings of {rules.MaxBookingsPerMember}.");
        }

        bookings.Add(newBooking);
    }
    
    private int GetNextBookingId() => bookings.Any() ? bookings.Max(booking => booking.Id) + 1 : 1;

    public void ChangeName(string name)
    {
        Name = Name.ChangeName(name);
    }

    public void AddUnit(string name)
    {
        int newId = GetNextUnitId();
        units.Add(new Unit(new UnitId(newId), name));
    }

    private int GetNextUnitId() => units.Any() ? units.Max(unit => unit.Id) + 1 : 1;    

    public void RemoveUnit(UnitId unitId)
    {
        units.RemoveAll(unit => unit.Id == unitId.Value);
    }

    public bool IsAvailable()
    {
        DateTime now = DateTime.Now;
        return OpenDate <= now &&  now < EndDate;
    }
}
