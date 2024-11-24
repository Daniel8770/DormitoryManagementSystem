namespace DormitoryManagementSystem.API.DTOs.Requests.ClubsContext;

public record BookDaysRequest
(
    Guid MemberId,
    int UnitId,
    DateTime Date,
    int Days
);
