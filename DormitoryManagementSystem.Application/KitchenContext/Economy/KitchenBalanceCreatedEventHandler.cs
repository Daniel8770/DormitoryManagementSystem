using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using Rebus.Handlers;
using Rebus.Bus;
using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
using DormitoryManagementSystem.Domain.KitchenContext.IntegrationMessages;
using DormitoryManagementSystem.Domain.SharedExpensesContext.IntegrationMessages;

namespace DormitoryManagementSystem.Application.KitchenContext.Economy;

public class KitchenBalanceCreatedEventHandler : IHandleMessages<KitchenBalanceCreatedEvent>,
    IHandleMessages<SharedExpenseBalancerCreatedMessage>
{
    private IBus bus;
    private IKitchenBalanceRepository kitchenBalanceRepository;

    public KitchenBalanceCreatedEventHandler(IBus bus, IKitchenBalanceRepository kitchenBalanceRepository)
    {
        this.bus = bus;
        this.kitchenBalanceRepository = kitchenBalanceRepository;
    }

    public async Task Handle(KitchenBalanceCreatedEvent message)
    {
        await bus.Send(new CreateSharedExpenseBalancerMessage(
            message.KitchenBalanceId,
            message.Participants.Select(p => new Participant(p)),
            message.Currency));
    }

    public async Task Handle(SharedExpenseBalancerCreatedMessage message)
    {
        KitchenBalance kitchenBalance = await kitchenBalanceRepository.GetById(new KitchenBalanceId(message.KitchenBalanceId)) ??
            throw new ApplicationException($"Could not find kitchen balance {message.KitchenBalanceId}.");

        kitchenBalance.SharedExpensesBalancerId = message.SharedExpensesId;

        await kitchenBalanceRepository.Update(kitchenBalance);
    }
}


