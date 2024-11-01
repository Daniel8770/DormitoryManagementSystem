using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
using System;

namespace DormitoryManagementSystem.Infrastructure.SharedExpensesContext;

public class InMemorySharedExpensesBalancerRepository : ISharedExpensesBalancerRepository
{
    private List<SharedExpensesGroup> sharedExpensesBalancers = new();

    public async Task<SharedExpensesGroup?> GetById(SharedExpensesGroupId id)
    {
        SharedExpensesGroup? result = sharedExpensesBalancers.Find(s => s.Id == id);
        return await Task.FromResult(result);
    }

    public async Task Save(SharedExpensesGroup sharedExpenseBalancer)
    {
        if (await GetById(sharedExpenseBalancer.Id) is not null)
            throw new InfrastructureException($"Kitchen balance {sharedExpenseBalancer.Id} already exists.");

        sharedExpensesBalancers.Add(sharedExpenseBalancer);

        await Task.CompletedTask;
    }

    public async Task Update(SharedExpensesGroup sharedExpenseBalancer)
    {
        SharedExpensesGroup existing = await GetById(sharedExpenseBalancer.Id) ??
            throw new InfrastructureException($"Kitchen balance {sharedExpenseBalancer.Id} doesn't exist.");

        sharedExpensesBalancers.Remove(existing);
        sharedExpensesBalancers.Add(sharedExpenseBalancer);

        await Task.CompletedTask;
    }
}
