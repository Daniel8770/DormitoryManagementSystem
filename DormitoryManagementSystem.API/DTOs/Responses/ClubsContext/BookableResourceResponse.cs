using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using System.Runtime.CompilerServices;

namespace DormitoryManagementSystem.API.DTOs.Responses.ClubsContext;

public record BookableResourceResponse
(
    Guid Id,
    string Name,
    string Rules,
    int? MaxBookingsPerMember,
    DateTime OpenDate,
    DateTime EndDate,
    List<UnitResponse> Units,
    List<BookingResponse> Bookings
);

public record BookingResponse
(
    int Id,
    int UnitId,
    Guid MemberId,
    DateTime DateBooked,
    DateTime StartDate,
    DateTime EndDate,
    int? Hours,
    int? Days
);

public record UnitResponse
(
    int Id,
    string Name
);

public static class DomainObjectToResponseMapper
{
    public static IEnumerable<BookableResourceResponse> ConvertToResponses(this IEnumerable<BookableResource> resources)
    {
        return resources.Select(resource => resource.ConvertToResponse());
    }

    public static BookableResourceResponse ConvertToResponse(this BookableResource resource)
    {
        return new BookableResourceResponse(
            resource.Id.Value,
            resource.Name.Value,
            resource.Rules.Information,
            resource.Rules is MaxBookingsPerMemberRules maxBookingsRules
                ? maxBookingsRules.MaxBookingsPerMember : null,
            resource.OpenDate,
            resource.EndDate,
            resource.Units.ConvertToResponses().ToList(),
            resource.Bookings.ConvertToResponses().ToList()
        );
    }

    public static IEnumerable<UnitResponse> ConvertToResponses(this IEnumerable<Unit> units)
    {
        return units.Select(unit => unit.ConvertToResonse());
    }

    public static IEnumerable<BookingResponse> ConvertToResponses(this IEnumerable<Booking> bookings)
    {
        return bookings.Select(booking => booking.ConvertToRespone());
    }   

    public static UnitResponse ConvertToResonse(this Unit unit)
    {
        return new UnitResponse(
            unit.Id.Value,
            unit.Name.Value
        );
    }

    public static BookingResponse ConvertToRespone(this Booking booking)
    {
        return new BookingResponse(
            booking.Id.Value,
            booking.UnitId.Value,
            booking.MemberId.Value,
            booking.DateBooked,
            booking.TimePeriod.StartDate,
            booking.TimePeriod.EndDate,
            booking.TimePeriod is HoursTimePeriod hoursTimePeriod
                ? hoursTimePeriod.Hours : null,
            booking.TimePeriod is DaysTimePeriod daysTimePeriod
                ? daysTimePeriod.Days : null
        );
    }   
}