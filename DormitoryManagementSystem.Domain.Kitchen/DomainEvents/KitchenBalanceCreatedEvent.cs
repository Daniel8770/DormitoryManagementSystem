using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

namespace DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;

public class KitchenBalanceCreatedEvent : DomainEvent
{
    public Guid KitchenBalanceId { get; init; }
    public List<Guid> Participants { get; init; }
    public string Currency { get; init; }

    public KitchenBalanceCreatedEvent(Guid kitchenBalanceId, IEnumerable<Guid> participants, string currency)
    {
        KitchenBalanceId = kitchenBalanceId;
        Participants = participants.ToList();
        Currency = currency;
    }


}