using DormitoryManagementSystem.Domain.Common.MoneyModel;

namespace DormitoryManagementSystem.API.DTOs.Requests.KitchenContext;

public class OpenNewKitchenBalanceRequest(string name, string currency)
{
    public string Name { get; set; } = name;
    public string Currency { get; set; } = currency;
}
