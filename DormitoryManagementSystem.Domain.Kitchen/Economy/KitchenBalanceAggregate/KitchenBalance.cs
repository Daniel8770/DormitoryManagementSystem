using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
using DormitoryManagementSystem.Domain.SharedExpensesContext;
using System.Collections.Immutable;


namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;

public class KitchenBalance : AggregateRoot
{
    public Guid Id { get; init; }
    public KitchenBalanceInformation Information { get; private set; }
    public KitchenId KitchenId { get; init; }
    public ImmutableList<ResidentId> Participants => participants.ToImmutableList();
    public Currency Currency;
  

    private List<ResidentId> participants;
   

    internal KitchenBalance(Guid id, string name, KitchenId kitchenId, IEnumerable<ResidentId> participants, Currency currency)
    {
        Information = new KitchenBalanceInformation(name);
        this.participants = participants.ToList();
        KitchenId = kitchenId;  
        Id = id;
        Currency = currency;
    }

    public void AddParticpants(IEnumerable<ResidentId> participants)
    {
        foreach (var participant in participants)
        {
            AddParticipant(participant);
        }
    }

    public void AddParticipant(ResidentId participant)
    {
        if (participants.Contains(participant))
            throw new DomainException($"The participant {participant.Value} is already in the kitchen balance.");

        participants.Add(participant);
    }

    public void RemoveParticipant(ResidentId participant)
    {
        if (!participants.Contains(participant))
            throw new DomainException($"The participant {participant.Value} is not in the kitchen balance.");

        participants.Remove(participant);
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
