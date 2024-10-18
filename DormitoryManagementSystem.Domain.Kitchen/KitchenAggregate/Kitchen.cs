using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenAccountAggregate;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using System.Collections.Immutable;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

public class Kitchen : AggregateRoot
{
    public KitchenId Id { get; init; }
    public KitchenInformation Information { get; private set; }
    public KitchenAccountId? KitchenAccountId { get; private set; }
    public ImmutableList<Resident> Residents => residents.ToImmutableList();

    private List<Resident> residents = new();

    public Kitchen(KitchenId id, string name)
    {
        Id = id;
        Information = KitchenInformation.Create(name);
    }

    public void OpenKitchenAccount(KitchenAccountId id)
    {
        if (KitchenAccountId is not null)
            throw new DomainException($"There already exists a kitchen account on this kitchen.");

        KitchenAccountId = id;
    }

    public void RemoveKitchenAccount()
    {
        if (KitchenAccountId is null)
            throw new DomainException($"There is no existing kitchen account on this kitchen.");

        KitchenAccountId = null;
    }

    public KitchenBalance OpenKitchenBalanceWithAllResidents(string name) 
    {
        return OpenKitchenBalance(name, Residents);
    }

    public KitchenBalance OpenKitchenBalanceWithAllResidentsPLusExternalPartcipants(string name, IEnumerable<Resident> participants)
    {
        return OpenKitchenBalance(name, Residents.Concat(participants));
    }

    public KitchenBalance OpenKitchenBalanceWithSomeResidents(string name, IEnumerable<Resident> residents)
    {
        if (residents.Any(r => !Residents.Contains(r)))
            throw new DomainException("Some of the residents are not part of this kitchen.");

        return OpenKitchenBalance(name, residents);
    }

    public KitchenBalance OpenKitchenBalance(string name, IEnumerable<Resident> participants)
    {
        return new KitchenBalance(Guid.NewGuid(), name, Id, participants);
    }

    public void UpdateRules(string newRules)
    {
        Information = Information.UpdateRules(newRules);
    }

    public void DeleteRules() 
    {
        Information = Information.DeleteRules();
    }

    public void UpdateDescription(string newDescription)
    {
        Information = Information.UpdateDescription(newDescription);
    }

    public void DeleteDescription()
    {
        Information = Information.DeleteDescription();
    }
}
