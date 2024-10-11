using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAccountAggregate;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

public class Kitchen : AggregateRoot
{
    public KitchenId Id { get; init; }
    public KitchenDescription Description { get; private set; }
    public KitchenAccountId? KitchenAccountId { get; private set; }

    public Kitchen(KitchenId id, string name)
    {
        Id = id;
        Description = KitchenDescription.Create(name);
    }

    public void AddKitchenAccount(KitchenAccount kitchenAccount)
    {
        if (KitchenAccountId is not null)
            throw new Exception($"There already exists a kitchen account on this kitchen.");

        KitchenAccountId = kitchenAccount.Id;
    }

    public void RemoveKitchenAccount()
    {
        if (KitchenAccountId is null)
            throw new Exception($"There is no existing kitchen account on this kitchen.");

        KitchenAccountId = null;
    }

    public void UpdateRules(string newRules)
    {
        Description = Description.UpdateRules(newRules);
    }

    public void DeleteRules() 
    {
        Description = Description.DeleteRules();
    }

    public void UpdateDescription(string newDescription)
    {
        Description = Description.UpdateDescription(newDescription);
    }

    public void DeleteDescription()
    {
        Description = Description.DeleteDescription();
    }
}
