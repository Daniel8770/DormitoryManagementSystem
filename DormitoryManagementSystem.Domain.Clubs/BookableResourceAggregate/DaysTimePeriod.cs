namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

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
