using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;


namespace DormitoryManagementSystem.Domain.KitchenContext.IntegrationMessages;
public class CreateSharedExpenseBalancerMessage : IntegrationMessage
{
    public Guid KitchenBalanceId { get; set; }
    public List<Participant> Participants { get; set; }
    public string Currency { get; set; }

    public CreateSharedExpenseBalancerMessage(Guid kitchenBalanceId, IEnumerable<Participant> participants, string currency)
    {
        KitchenBalanceId = kitchenBalanceId;
        Participants = participants.ToList();
        Currency = currency;
    }
}
