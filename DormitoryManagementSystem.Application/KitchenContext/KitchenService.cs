using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
using DormitoryManagementSystem.Infrastructure.Common.DomainEvents;


namespace DormitoryManagementSystem.Application.KitchenContext;
public class KitchenService
{
    IKitchenRepository kitchenRepository;
    IKitchenBalanceRepository kitchenBalanceRepository;

    public KitchenService(IKitchenRepository kitchenRepository, IKitchenBalanceRepository kitchenBalanceRepository, DomainEventPublisher domainEventPublisher)
    {
        this.kitchenRepository = kitchenRepository;
        this.kitchenBalanceRepository = kitchenBalanceRepository;
    }

    public async Task<Kitchen> OpenNewKitchen(string name)
    {
        Kitchen newKitchen = new Kitchen(KitchenId.Next(), name);
        await kitchenRepository.Save(newKitchen);
        return newKitchen;
    }

    public async Task<KitchenBalance> OpenNewKitchenBalanceOnKitchenWithAllResidents(KitchenId id, string name, Currency currency)
    {
        if (await kitchenBalanceRepository.AlreadyExistsWithName(name))
            throw new ApplicationException($"Kitchen balance with name {name} already exists on kitchen.");

        Kitchen? kitchen = await kitchenRepository.GetById(id) ??
            throw new ApplicationException($"Could not find kitchen {id.Value}.");

        KitchenBalance newKitchenBalance = kitchen.OpenKitchenBalanceWithAllResidents(name, currency);

        await kitchenBalanceRepository.Save(newKitchenBalance);

        return newKitchenBalance;
    }
}
