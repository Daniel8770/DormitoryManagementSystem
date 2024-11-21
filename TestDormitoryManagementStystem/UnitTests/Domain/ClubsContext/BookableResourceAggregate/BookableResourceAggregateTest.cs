using DormitoryManagementSystem.Domain.Clubs.BookableResourceAggregate;
using DormitoryManagementSystem.Domain.ClubsContext;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using DormitoryManagementSystem.Domain.Common.Exceptions;


namespace TestDormitoryManagementStystem.UnitTests.Domain.ClubsContext.BookableResourceAggregate;

public class BookableResourceAggregateTest
{
    private BookableResource bookableResource;

    public BookableResourceAggregateTest()
    {
        bookableResource = BookableResource.CreateNewWithMaximumBookingsPerMember(
            "Resource 1",
            "These is very important information about the rules of booking this resource.",
            3,
            DateTime.Now.AddDays(-7),
            DateTime.Now.AddDays(7)
        );
    }

    [Fact]
    public void CreateNew()
    {
        Assert.True(bookableResource.Id.Value != Guid.Empty);
        Assert.Equal("Resource 1", bookableResource.Name.Value);
        Assert.Equal("These is very important information about the rules of booking this resource.", bookableResource.Rules.Information);
        Assert.Equal(3, ((MaxBookingsPerMemberRules)bookableResource.Rules).MaxBookingsPerMember);
        Assert.Equal(DateTime.Now.AddDays(-7).Date, bookableResource.OpenDate.Date);
        Assert.Equal(DateTime.Now.AddDays(7).Date, bookableResource.EndDate.Date);
    }

    [Fact]
    public void ChangeName()
    {
        bookableResource.ChangeName("New Name");
        Assert.Equal("New Name", bookableResource.Name.Value);
    }

    [Fact]
    public void AddUnit()
    {
        bookableResource.AddUnit("Unit 1");
        bookableResource.AddUnit("Unit 2");

        Assert.True(bookableResource.Units.Count == 2);
        Assert.Collection(bookableResource.Units,
            unit =>
            {
                Assert.Equal("Unit 1", unit.Name.Value);
                Assert.Equal(1, unit.Id.Value);
            },
            unit =>
            {
                Assert.Equal("Unit 2", unit.Name.Value);
                Assert.Equal(2, unit.Id.Value);
            });
    }

    [Fact]
    public void BookDays()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nDays = 3;

        bookableResource.BookDays(memberId, unitId, date, nDays);

        Assert.True(bookableResource.Bookings.Count == 1);
        Assert.Equal(date.AddDays(nDays), bookableResource.Bookings.First().TimePeriod.EndDate);
    }

    [Fact]
    public void BookHours()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nHours = 3;

        bookableResource.BookHours(memberId, unitId, date, nHours);

        Assert.True(bookableResource.Bookings.Count == 1);
        Assert.Equal(date.AddHours(nHours), bookableResource.Bookings.First().TimePeriod.EndDate);
    }

    [Fact]
    public void BookDays_WhenUnitNotPartOfResource_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = new(100);
        DateTime date = DateTime.Now;
        int nHours = 3;

        Assert.Throws<DomainException>(() => bookableResource.BookHours(memberId, unitId, date, nHours));
    }

    [Fact]
    public void BookDays_WhenStartDateWithinAnotherBookingTimeperiod_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nDays = 3;

        bookableResource.BookDays(memberId, unitId, date, nDays);

        Assert.Throws<DomainException>(() => bookableResource.BookDays(memberId, unitId, date.AddDays(1), nDays));
    }

    [Fact]
    public void BookDays_WhenEndDateWithinAnotherBookingTimeperiod_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nDays = 3;

        bookableResource.BookDays(memberId, unitId, date, nDays);

        Assert.Throws<DomainException>(() => bookableResource.BookDays(memberId, unitId, date.AddDays(-4), 5));
    }

    [Fact]
    public void BookDays_WhenWholeTimeperiodWithinAnotherBookingtimePeriod_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nDays = 4;

        bookableResource.BookDays(memberId, unitId, date, nDays);

        Assert.Throws<DomainException>(() => bookableResource.BookDays(memberId, unitId, date.AddDays(1), 1));
    }

    [Fact]
    public void BookDays_WhenTimePeriodSpansMoreThanAnotherBookingTimePeriod_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nDays = 2;

        bookableResource.BookDays(memberId, unitId, date, nDays);

        Assert.Throws<DomainException>(() => bookableResource.BookDays(memberId, unitId, date.AddDays(-2), 7));
    }

    [Fact]
    public void BookDays_WhenMemberExceedsMaxBookingsPerMember_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nDays = 2;

        bookableResource.BookDays(memberId, unitId, date, nDays);
        bookableResource.BookDays(memberId, unitId, date.AddDays(4), nDays);
        bookableResource.BookDays(memberId, unitId, date.AddDays(7), nDays);

        Assert.Throws<DomainException>(() => bookableResource.BookDays(memberId, unitId, date.AddDays(10), nDays));
    }

    [Fact]
    public void BookHours_WhenStartDateWithinAnotherBookingTimeperiod_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nHours = 3;

        bookableResource.BookHours(memberId, unitId, date, nHours);

        Assert.Throws<DomainException>(() => bookableResource.BookHours(memberId, unitId, date.AddHours(1), nHours));
    }

    [Fact]
    public void BookHours_WhenEndDateWithinAnotherBookingTimeperiod_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nHours = 3;

        bookableResource.BookHours(memberId, unitId, date, nHours);

        Assert.Throws<DomainException>(() => bookableResource.BookHours(memberId, unitId, date.AddHours(-4), 5));
    }

    [Fact]
    public void BookHours_WhenWholeTimeperiodWithinAnotherBookingtimePeriod_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nHours = 4;

        bookableResource.BookHours(memberId, unitId, date, nHours);

        Assert.Throws<DomainException>(() => bookableResource.BookHours(memberId, unitId, date.AddHours(1), 1));
    }

    [Fact]
    public void BookHours_WhenTimePeriodSpansMoreThanAnotherBookingTimePeriod_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nHours = 2;

        bookableResource.BookHours(memberId, unitId, date, nHours);

        Assert.Throws<DomainException>(() => bookableResource.BookHours(memberId, unitId, date.AddHours(-2), 7));
    }

    [Fact]
    public void BookHours_WhenMemberExceedsMaxBookingsPerMember_ShouldThrow()
    {
        bookableResource.AddUnit("Unit 1");

        MemberId memberId = MemberId.Next();
        UnitId unitId = bookableResource.Units.First().Id;
        DateTime date = DateTime.Now;
        int nHours = 2;

        bookableResource.BookHours(memberId, unitId, date, nHours);
        bookableResource.BookHours(memberId, unitId, date.AddHours(4), nHours);
        bookableResource.BookHours(memberId, unitId, date.AddHours(7), nHours);

        Assert.Throws<DomainException>(() => bookableResource.BookHours(memberId, unitId, date.AddHours(10), nHours));
    }

    [Fact]
    public void GetBookableUnits()
    {
        bookableResource.AddUnit("Unit 1");
        bookableResource.AddUnit("Unit 2");
        bookableResource.AddUnit("Unit 3");
        bookableResource.AddUnit("Unit 4");
        bookableResource.AddUnit("Unit 5");

        MemberId memberId1 = MemberId.Next();
        MemberId memberId2 = MemberId.Next();
        MemberId memberId3 = MemberId.Next();
        DateTime date = DateTime.Now;
        int nHours = 2;

        bookableResource.BookHours(memberId1, new(1), date, nHours);
        bookableResource.BookHours(memberId2, new(2), date, nHours);
        bookableResource.BookHours(memberId3, new(3), date, nHours);

        IEnumerable<Unit> bookableUnits = bookableResource.GetBookableUnits();

        Assert.Equal(2, bookableUnits.Count());
        Assert.Collection(bookableUnits,
            unit =>
            {
                Assert.Equal("Unit 4", unit.Name.Value);
            },
            unit =>
            {
                Assert.Equal("Unit 5", unit.Name.Value);
            }
        );
    }
    
}
