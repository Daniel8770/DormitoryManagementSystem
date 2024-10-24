using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
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

    public static Kitchen CreateNew(string name) => new Kitchen(KitchenId.Next(), name);

    private Kitchen(KitchenId id, string name)
    {
        Id = id;
        Information = KitchenInformation.Create(name);
    }

    private Kitchen(KitchenId id, string name, string description, string rules, KitchenAccountId? kitchenAccountId, IEnumerable<Resident> residents)
    {
        Id = id;
        Information = KitchenInformation.CreateWithDescriptionAndRules(name, description, rules);
        KitchenAccountId = kitchenAccountId;
        this.residents = residents.ToList();
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

    public KitchenBalance OpenKitchenBalanceWithAllResidents(string name, Currency currency) 
    {
        return OpenKitchenBalance(name, GetResidentIds(), currency);
    }

    public KitchenBalance OpenKitchenBalanceWithSomeResidents(string name, IEnumerable<ResidentId> residents, Currency currency)
    {
        if (residents.Any(r => !GetResidentIds().Contains(r)))
            throw new DomainException("Some of the residents are not part of this kitchen.");

        return OpenKitchenBalance(name, residents, currency);
    }

    private KitchenBalance OpenKitchenBalance(string name, IEnumerable<ResidentId> participants, Currency currency)
    {
        return KitchenBalance.CreateNewBalanceOnKitchen(KitchenBalanceId.Next(), name, Id, participants, currency);
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

    private IEnumerable<ResidentId> GetResidentIds() => Residents.Select(r => r.Id);
}
