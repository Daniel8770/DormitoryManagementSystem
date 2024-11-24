namespace DormitoryManagementSystem.API.DTOs.Requests.ClubsContext;

public record CreateNewBookableResourceRequest
(
    string Name,
    string Rules,
    DateTime OpenDate,
    DateTime EndDate
);