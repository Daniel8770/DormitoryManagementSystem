namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

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
