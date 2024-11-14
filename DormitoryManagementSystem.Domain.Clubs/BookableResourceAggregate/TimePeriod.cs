using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

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
