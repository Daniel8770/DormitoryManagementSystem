using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

namespace DormitoryManagementSystem.Infrastructure.KitchenContext;
public class InMemoryKitchenRepository : IKitchenRepository
{
    private List<Kitchen> kitchens = new();

    public async Task<Kitchen?> GetById(KitchenId id)
    {
        Kitchen? kitchen = kitchens.Find(k => k.Id.Value == id.Value);
        return await Task.FromResult(kitchen);
    }

    public async Task Save(Kitchen kitchen)
    {
        if (await GetById(kitchen.Id) is not null)
            throw new InfrastructureException($"Kitchen balance {kitchen.Id} already exists.");

        kitchens.Add(kitchen);

        await Task.CompletedTask;
    }

    public async Task Update(Kitchen kitchen)
    {
        Kitchen existing = await GetById(kitchen.Id) ??
            throw new InfrastructureException($"Kitchen balance {kitchen.Id} doesn't exist.");

        kitchens.Remove(kitchen);
        kitchens.Add(kitchen);

        await Task.CompletedTask;
    }
}
