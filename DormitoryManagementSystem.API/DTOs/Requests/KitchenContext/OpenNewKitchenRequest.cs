namespace DormitoryManagementSystem.API.DTOs.Requests.KitchenContext;

public class OpenNewKitchenRequest(string name)
{
    public string Name { get; set; } = name;
}
