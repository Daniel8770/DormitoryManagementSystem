using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using System.Collections.Immutable;


namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public record BookableResourceId(Guid Value) : EntityId<Guid>(Value)
{
    public static BookableResourceId Next() => new BookableResourceId(Guid.NewGuid());
}

public class BookableResource : AggregateRoot<BookableResourceId>
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

    public BookableResource(
        BookableResourceId id,
        string name,
        DateTime openDate,
        DateTime endDate,
        Rules rules,
        List<Unit> units,
        List<Booking> bookings)
        : base(id)
    {
        Name = new Name(name);
        OpenDate = openDate;
        EndDate = endDate;
        Rules = rules;
        this.units = units;
        this.bookings = bookings;
    }

    public IEnumerable<Booking> GetNonExpiredBookingsOfMember(MemberId memberId) =>
        GetAllBookingsOfMember(memberId).Where(booking => !booking.IsExpired());

    public IEnumerable<Booking> GetAllBookingsOfMember(MemberId memberId) =>
        bookings.Where(booking => booking.MemberId == memberId);

    public IEnumerable<Booking> GetNonExpiredBookings() =>
        bookings.Where(booking => !booking.IsExpired());

    public IEnumerable<Unit> GetBookableUnits() =>
        units.Where(unit => !(GetNonExpiredBookings().Any(booking => booking.UnitId == unit.Id)));

    public void BookDays(MemberId memberId, UnitId unitId, DateTime date, int days)
    {
        Booking newBooking = CreateNewBooking(memberId, unitId, date, new DaysTimePeriod(date, days));
        Book(newBooking);
    }

    public void BookHours(MemberId memberId, UnitId unitId, DateTime date, int hours)
    {
        Booking newBooking = CreateNewBooking(memberId, unitId, date, new HoursTimePeriod(date, hours));
        Book(newBooking);
    }

    private Booking CreateNewBooking(MemberId memberId, UnitId unitId, DateTime date, TimePeriod timePeriod)
    {
        return new Booking(
            GetNextBookingId(),
            Id,
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

    private BookingId GetNextBookingId() =>
        bookings.Any() ? new (bookings.Max(booking => booking.Id.Value) + 1) : new (1);

    public void ChangeName(string name)
    {
        Name = Name.ChangeName(name);
    }

    public void AddUnit(string name)
    {
        UnitId newId = GetNextUnitId();
        units.Add(new Unit(newId, Id, name));
    }

    private UnitId GetNextUnitId() =>
        units.Any() ? new (units.Max(unit => unit.Id.Value) + 1) : new (1);

    public void RemoveUnit(UnitId unitId) =>
        units.RemoveAll(unit => unit.Id == unitId);

    public bool IsAvailable()
    {
        DateTime now = DateTime.Now;
        return OpenDate <= now && now < EndDate;
    }

    private bool ResourceContainsUnit(UnitId unitId) =>
        units.Find(unit => unit.Id == unitId) is null ? false : true;

    private bool UnitAlreadyBooked(UnitId unitId, TimePeriod timePeriod) =>
        bookings.Find(booking =>
            booking.UnitId.Value == unitId.Value
            && timePeriod.Overlaps(booking.TimePeriod))
        is null ? false : true;

    private bool MaxBookingsForMemberReached(MemberId memberId)
    {
        if (Rules is MaxBookingsPerMemberRules rules)
        {
            IEnumerable<Booking> membersOtherBookings = GetNonExpiredBookingsOfMember(memberId);
            return membersOtherBookings.Count() >= rules.MaxBookingsPerMember;
        }

        return false;
    }
}
