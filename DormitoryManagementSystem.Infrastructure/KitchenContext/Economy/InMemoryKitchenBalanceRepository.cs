using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;


namespace DormitoryManagementSystem.Infrastructure.KitchenContext.Economy;
public class InMemoryKitchenBalanceRepository : IKitchenBalanceRepository
{
    private List<KitchenBalance> kitchenBalances = new();

    public async Task<bool> AlreadyExistsWithName(string name)
    {
        bool exists = kitchenBalances.Exists(kb => kb.Information.Name == name);
        return await Task.FromResult(exists);
    }

    public async Task<KitchenBalance?> GetById(KitchenBalanceId id)
    {
        KitchenBalance? kitchenBalance = kitchenBalances.Find(kb => kb.Id.Value == id.Value);
        return await Task.FromResult(kitchenBalance);
    }

    public async Task Save(KitchenBalance kitchenBalance)
    {
        if (await GetById(kitchenBalance.Id) is not null)
            throw new InfrastructureException($"Kitchen balance {kitchenBalance.Id.Value} already exists.");

        kitchenBalances.Add(kitchenBalance);

        await Task.CompletedTask;
    }

    public async Task Update(KitchenBalance kitchenBalance)
    {
        KitchenBalance existing = await GetById(kitchenBalance.Id) ??
            throw new InfrastructureException($"Kitchen balance {kitchenBalance.Id.Value} doesn't exist.");

        kitchenBalances.Remove(existing);
        kitchenBalances.Add(kitchenBalance);

        await Task.CompletedTask;
    }
}
