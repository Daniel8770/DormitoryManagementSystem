using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

namespace DormitoryManagementSystem.Application.KitchenContext;
public interface IKitchenService
{
    Task<Kitchen> OpenNewKitchen(string name);
    Task<KitchenBalance> OpenNewKitchenBalanceOnKitchenWithAllResidents(KitchenId id, string name, Currency currency);
}