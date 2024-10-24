using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
using System.Collections.Immutable;


namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;

public class KitchenBalance : AggregateRoot
{
    public KitchenBalanceId Id { get; init; }
    public KitchenBalanceInformation Information { get; private set; }
    public KitchenId KitchenId { get; init; }
    public ImmutableList<ResidentId> Participants => participants.ToImmutableList();
    public Currency Currency;
    public Guid? SharedExpensesBalancerId { get; set; }

    private List<ResidentId> participants;
    
    internal static KitchenBalance CreateNewBalanceOnKitchen(
        KitchenBalanceId id,
        string name,
        KitchenId kitchenId,
        IEnumerable<ResidentId> participants,
        Currency currency)
    {
        KitchenBalance newBalance = new KitchenBalance(id, name, kitchenId, participants, currency);
        DomainEventStore.Raise(new KitchenBalanceCreatedEvent(newBalance.Id.Value, participants.Select(id => id.Value), currency.ToString()));
        return newBalance;
    }

    internal KitchenBalance(KitchenBalanceId id, string name, KitchenId kitchenId, IEnumerable<ResidentId> participants, Currency currency)
    {
        Information = new KitchenBalanceInformation(name);
        this.participants = participants.ToList();
        KitchenId = kitchenId;  
        Id = id;
        Currency = currency;
    }

    public void ChangeName(string name)
    {
        Information = Information.ChangeName(name);
    }

    public void ChangeDescription(string description)
    {
        Information = Information.ChangeDescription(description);
    }

    public void RemoveDescription()
    {
        Information = Information.RemoveDescription();
    }

    



}
