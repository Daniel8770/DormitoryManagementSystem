using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.KitchenContext.IntegrationMessages;
using DormitoryManagementSystem.Domain.SharedExpensesContext.IntegrationMessages;
using DormitoryManagementSystem.Domain.SharedExpensesContext.MinimumTransactionDebtSettlementAlgorithm;
using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
using Rebus.Bus;
using Rebus.Handlers;

namespace DormitoryManagementSystem.Application.SharedExpensesContext;

public class CreateSharedExpenseBalancerMessageHandler : 
    IHandleMessages<CreateSharedExpenseBalancerMessage>
{
    private ISharedExpensesBalancerRepository sharedExpensesBalancerRepository;
    private IBus bus;

    public CreateSharedExpenseBalancerMessageHandler(ISharedExpensesBalancerRepository sharedExpensesBalancerRepository, IBus bus)
    {
        this.sharedExpensesBalancerRepository = sharedExpensesBalancerRepository;
        this.bus = bus;
    }

    public async Task Handle(CreateSharedExpenseBalancerMessage message)
    {
        SharedExpensesBalancer newBalancer = SharedExpensesBalancer.CreateNew(
            Enum.Parse<Currency>(message.Currency),
            message.Participants.ToList(),
            new RandomMinimumTransactionsDebtSettler());

        await sharedExpensesBalancerRepository.Save(newBalancer);
        await bus.Send(new SharedExpenseBalancerCreatedMessage(newBalancer.Id.Value, message.KitchenBalanceId));
    }
}