using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;


namespace DormitoryManagementSystem.Domain.KitchenContext.IntegrationMessages;
public class CreateSharedExpenseBalancerMessage : IntegrationMessage
{
    public Guid KitchenId { get; set; }
    public IEnumerable<Participant> Participants { get; set; }
    public Currency Currency { get; set; }

    public CreateSharedExpenseBalancerMessage(Guid kitchenId, IEnumerable<Participant> participants, Currency currency)
    {
        KitchenId = kitchenId;
        Participants = participants;
        Currency = currency;
    }
}
