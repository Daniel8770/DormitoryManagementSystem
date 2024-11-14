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

    public static BookableResource CreateNewWithMaximumBookingsPerMember(
        string name,
        string rules,
        int maxBookingsPerMember,
        DateTime openDate,
        DateTime endDate) =>
            new BookableResource(
                BookableResourceId.Next(),
                name,
                openDate,
                endDate,
                new MaxBookingsPerMemberRules(rules, maxBookingsPerMember),
                new(),
                new());

    private BookableResource(
        BookableResourceId id,
        string name,
        DateTime openDate,
        DateTime endDate,
        Rules rules,
        List<Unit> units,
        List<Booking> bookings) 
        : base(id.Value)
    {
        Name = new Name(name);
        OpenDate = openDate;
        EndDate = endDate;
        Rules = rules;
        this.units = units;
        this.bookings = bookings;
    }

    public IEnumerable<Booking> GetNonExpiredBookingsOfMember(Guid memberId) =>
        GetAllBookingsOfMember(memberId).Where(booking => !booking.IsExpired());

    public IEnumerable<Booking> GetAllBookingsOfMember(Guid memberId) =>
        bookings.Where(booking => booking.MemberId == memberId);

    public IEnumerable<Booking> GetNonExpiredBookings() =>
        bookings.Where(booking => !booking.IsExpired());

    public IEnumerable<Unit> GetBookableUnits() => 
        units.Where(unit => !(GetNonExpiredBookings().Any(booking => booking.UnitId.Value == unit.Id)));

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
        if (!ResourceContainsUnit(newBooking.UnitId))
            throw new DomainException($"Cannot book unit {newBooking.UnitId} for member " +
                $"{newBooking.MemberId} because the unit is not part of the bookable resource.");

        if (UnitAlreadyBooked(newBooking.UnitId, newBooking.TimePeriod))
            throw new DomainException($"Cannot book unit {newBooking.UnitId} for member " +
                $"{newBooking.MemberId} because there is another booking in the same time period.");

        if (MaxBookingsForMemberReached(newBooking.MemberId))
            throw new DomainException($"Cannot book unit {newBooking.UnitId} for member " +
                $"{newBooking.MemberId} because the member has reached the maximum number of bookings of " +
                $"{((MaxBookingsPerMemberRules)Rules).MaxBookingsPerMember}.");
        
        bookings.Add(newBooking);
    }
    
    private int GetNextBookingId() => 
        bookings.Any() ? bookings.Max(booking => booking.Id) + 1 : 1;

    public void ChangeName(string name)
    {
        Name = Name.ChangeName(name);
    }

    public void AddUnit(string name)
    {
        int newId = GetNextUnitId();
        units.Add(new Unit(new UnitId(newId), name));
    }

    private int GetNextUnitId() => 
        units.Any() ? units.Max(unit => unit.Id) + 1 : 1;

    public void RemoveUnit(UnitId unitId) => 
        units.RemoveAll(unit => unit.Id == unitId.Value);

    public bool IsAvailable()
    {
        DateTime now = DateTime.Now;
        return OpenDate <= now &&  now < EndDate;
    }

    private bool ResourceContainsUnit(UnitId unitId) =>
        units.Find(unit => unit.Id == unitId.Value) is null ? false : true;

    private bool UnitAlreadyBooked(UnitId unitId, TimePeriod timePeriod) => 
        bookings.Find(booking =>
            booking.UnitId.Value == unitId.Value
            && timePeriod.Overlaps(booking.TimePeriod))
        is null ? false : true;

    private bool MaxBookingsForMemberReached(Guid memberId)
    {
        if (Rules is MaxBookingsPerMemberRules rules)
        {
            IEnumerable<Booking> membersOtherBookings = GetNonExpiredBookingsOfMember(memberId);
            return membersOtherBookings.Count() >= rules.MaxBookingsPerMember;
        }

        return false;
    }
}
