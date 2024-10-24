using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.IntegrationMessages;
using DormitoryManagementSystem.Domain.SharedExpensesContext.IntegrationMessages;
using Rebus.Bus;

namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents.Rebus;

public class RebusDomainEventSubscriber : IDomainEventSubscriber
{
    private IBus bus;

    public RebusDomainEventSubscriber(IBus bus)
    {
        this.bus = bus;
    }

    public async Task SubscribeToAllEvents()
    {
        //List<Task> subscriptionTasks = new()
        //{
        //    bus.Subscribe<KitchenBalanceCreatedEvent>(),
        //    bus.Subscribe<CreateSharedExpenseBalancerMessage>(),
        //    bus.Subscribe<SharedExpenseBalancerCreatedMessage>()
        //};

        //await Task.WhenAll(subscriptionTasks);    

        Task kitchenBalanceCreatedEventSubscription = bus.Subscribe<KitchenBalanceCreatedEvent>();
        Task createSharedExpenseBalancerMessageSubscription = bus.Subscribe<CreateSharedExpenseBalancerMessage>();
        Task sharedExpenseBalancerCreatedMessageSubscription = bus.Subscribe<SharedExpenseBalancerCreatedMessage>();

        await kitchenBalanceCreatedEventSubscription;
        await createSharedExpenseBalancerMessageSubscription;
        await sharedExpenseBalancerCreatedMessageSubscription;
    }
}
