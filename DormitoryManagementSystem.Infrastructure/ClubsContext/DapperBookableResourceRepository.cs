using Dapper;
using DormitoryManagementSystem.Domain.ClubsContext;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
using DormitoryManagementSystem.Infrastructure.Common.Persistence;
using Microsoft.Data.SqlClient;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext;

public class DapperBookableResourceRepository : IBookableResourceRepository
{
    private string connectionString;
    private DBConnection connection;

    public DapperBookableResourceRepository(string connectionString, DBConnection dbConnection)
    {
        this.connectionString = connectionString;
        this.connection = dbConnection;
    }

    public async Task<BookableResource?> GetById(BookableResourceId id)
    {
        await connection.OpenConnectionAsync();
        
        string bookableResourseQuery = "SELECT * FROM [Clubs].[BookableResource] WHERE Id = @bookableResourceId";
        string unitsQuery = "SELECT * FROM [Clubs].[Unit] where [BookableResourceId] = @bookableResourceId";
        string bookingsQuery = "SELECT * FROM [Clubs].[Booking] where [BookableResourceId] = @bookableResourceId";

        Task<dynamic?> bookableResourceTask = connection.QueryFirstOrDefaultAsync(bookableResourseQuery, 
            new { bookableResourceId = id.Value });
        dynamic? bookableResource = await bookableResourceTask;

        if (bookableResource == null)
            return null;

        Task<IEnumerable<dynamic>> unitsTask = connection.QueryAsync(unitsQuery, new { bookableResourceId = id.Value });
        Task<IEnumerable<dynamic>> bookingsTask = connection.QueryAsync(bookingsQuery, new { bookableResourceId = id.Value });
        IEnumerable<dynamic> units = await unitsTask;
        IEnumerable<dynamic> bookings = await bookingsTask;

        return new BookableResource(
            new BookableResourceId(bookableResource.Id),
            bookableResource.Name,
            bookableResource.OpenDate,
            bookableResource.EndDate,
            bookableResource.MaxBookingsPerMember is null
                ? new Rules(bookableResource.Rules)
                : new MaxBookingsPerMemberRules(bookableResource.Rules, bookableResource.MaxBookingsPerMember),
            units.Select(u => new Unit(
                new UnitId(u.Id),
                new BookableResourceId(u.BookableResourceId),
                u.Name)).ToList(),
            bookings.Select(b => new Booking(
                new BookingId(b.Id),
                new BookableResourceId(b.BookableResourceId),
                b.DateBooked,
                (TimePeriod)(b.Days is not null
                    ? new DaysTimePeriod(b.StartDate, b.Days)
                    : new HoursTimePeriod(b.StartDate, b.Hours)),
                new MemberId(b.MemberId),
                new UnitId(b.UnitId)
            )).ToList());
    }

    public async Task Save(BookableResource bookableResource)
    {
        await connection.OpenConnectionAsync();
        await connection.BeginTransactionAsync();

        string insertBookableResource = 
            @"INSERT INTO [Clubs].[BookableResource] 
            (Id, Name, OpenDate, EndDate, Rules, MaxBookingsPerMember) 
            VALUES (@Id, @Name, @OpenDate, @EndDate, @Rules, @MaxBookingsPerMember)";
        string insertUnit = 
            @"INSERT INTO [Clubs].[Unit] 
            (Id, Name, BookableResourceId) 
            VALUES (@Id, @Name, @BookableResourceId)";
        string insertBooking = 
            @"INSERT INTO [Clubs].[Booking] 
            (Id, MemberId, BookableResourceId, UnitId, StartDate, EndDate, DateBooked) 
            VALUES (@Id, @MemberId, @BookableResourceId, @UnitId, @StartDate, @EndDate, @DateBooked)";

        try
        {
            await connection.ExecuteAsync(insertBookableResource, parameters: new
            {
                Id = bookableResource.Id.Value,
                Name = bookableResource.Name.Value,
                OpenDate = bookableResource.OpenDate,
                EndDate = bookableResource.EndDate,
                Rules = bookableResource.Rules.Information,
                MaxBookingsPerMember = (int?)(bookableResource.Rules is MaxBookingsPerMemberRules maxBookings 
                    ? maxBookings.MaxBookingsPerMember : null)
            });

            await Task.WhenAll(bookableResource.Units.Select(unit =>
                connection.ExecuteAsync(insertUnit, parameters: new
                {
                    Id = unit.Id.Value,
                    Name = unit.Name,
                    BookableResourceId = unit.BookableResourceId.Value
                })
            ));

            await Task.WhenAll(bookableResource.Bookings.Select(booking =>
                connection.ExecuteAsync(insertBooking, parameters: new
                {
                    Id = booking.Id.Value,
                    MemberId = booking.MemberId.Value,
                    BookableResourceId = booking.BookableResourceId.Value,
                    UnitId = booking.UnitId.Value,
                    StartDate = booking.TimePeriod.StartDate,
                    EndDate = booking.TimePeriod.EndDate,
                    DateBooked = booking.DateBooked
                })
            ));
            
            await connection.CommitTransactionAndDisposeAsync(bookableResource);
        }
        catch (Exception)
        {
            await connection.RollbackTransactionAndDisposeAsync();
            throw;
        }
    }

    public async Task Update(BookableResource bookableResource)
    {
        await connection.OpenConnectionAsync();
        await connection.BeginTransactionAsync();

        string updateBookableResource =
            @"UPDATE [Clubs].[BookableResource] SET
                Name = @Name, 
                OpenDate = @OpenDate, 
                EndDate = @EndDate, 
                Rules = @Rules, 
                MaxBookingsPerMember = @MaxBookingsPerMember
            WHERE Id = @Id";
        string updateUnit =
            @"UPDATE [Clubs].[Unit] SET
                Name = @Name, 
                BookableResourceId = @BookableResourceId
            WHERE Id = @Id
                AND BookableResourceId = @BookableResourceId
            IF @@ROWCOUNT = 0
            INSERT INTO [Clubs].[Unit] 
            (Id, Name, BookableResourceId) 
            VALUES (@Id, @Name, @BookableResourceId)";
        string updateBooking =
            @"UPDATE [Clubs].[Booking] SET
                MemberId = @MemberId, 
                BookableResourceId = @BookableResourceId, 
                UnitId = @UnitId, 
                StartDate = @StartDate, 
                EndDate = @EndDate, 
                DateBooked = @DateBooked,
                Days = @Days,
                Hours = @Hours  
            WHERE Id = @Id
                AND BookableResourceId = @BookableResourceId
            IF @@ROWCOUNT = 0
            INSERT INTO [Clubs].[Booking] 
            (Id, MemberId, BookableResourceId, UnitId, StartDate, EndDate, DateBooked, Days, Hours) 
            VALUES (@Id, @MemberId, @BookableResourceId, @UnitId, @StartDate, @EndDate, @DateBooked, @Days, @Hours)";

        try
        {
            await connection.ExecuteAsync(updateBookableResource, parameters: new
            {
                Id = bookableResource.Id.Value,
                Name = bookableResource.Name.Value,
                OpenDate = bookableResource.OpenDate,
                EndDate = bookableResource.EndDate,
                Rules = bookableResource.Rules.Information,
                MaxBookingsPerMember = (int?)(bookableResource.Rules is MaxBookingsPerMemberRules maxBookings 
                    ? maxBookings.MaxBookingsPerMember : null)
            });

            await Task.WhenAll(bookableResource.Units.Select(unit =>
                connection.ExecuteAsync(updateUnit, parameters: new
                {
                    Id = unit.Id.Value,
                    Name = unit.Name.Value,
                    BookableResourceId = unit.BookableResourceId.Value
                })
            ));

            await Task.WhenAll(bookableResource.Bookings.Select(booking =>
                connection.ExecuteAsync(updateBooking, parameters: new
                {
                    Id = booking.Id.Value,
                    MemberId = booking.MemberId.Value,
                    BookableResourceId = booking.BookableResourceId.Value,
                    UnitId = booking.UnitId.Value,
                    StartDate = booking.TimePeriod.StartDate,
                    EndDate = booking.TimePeriod.EndDate,
                    DateBooked = booking.DateBooked,
                    Days = (int?)(booking.TimePeriod is DaysTimePeriod daysTimePeriod 
                        ? daysTimePeriod.Days : null),
                    Hours = (int?)(booking.TimePeriod is HoursTimePeriod hoursTimePeriod 
                        ? hoursTimePeriod.Hours : null)
                })
            ));

            await connection.CommitTransactionAndDisposeAsync(bookableResource);
        }
        catch (Exception)
        {
            await connection.RollbackTransactionAndDisposeAsync();
            throw;
        }
    }
}
