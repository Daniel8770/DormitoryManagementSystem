using DormitoryManagementSystem.Domain.KitchenContext;

namespace DormitoryManagementSystem.API.DTOs.Responses.KitchenContext;

public class OpenNewKitchenResponse(Guid kitchenId, string name, string? description, string? rules, Guid? kitchenAccountId, IEnumerable<Resident> residents)
    : Response
{
    public Guid Id { get; set; } = kitchenId;
    public string Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public string? Rules { get; set; } = rules;
    public Guid? KitchenAccountId { get; set; } = kitchenAccountId;
    public IEnumerable<Resident> Residents { get; set; } = residents;

}
