using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

namespace DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
public class KitchenBalanceCreatedEvent : DomainEvent
{

    public KitchenId KitchenId { get; init; }
    public IEnumerable<ResidentId> Participants { get; init; }
    public Currency Currency { get; init; }

    public KitchenBalanceCreatedEvent(KitchenId kitchenId, IEnumerable<ResidentId> participants, Currency currency)
    {
        KitchenId = kitchenId;
        Participants = participants;
        Currency = currency;
    }


}