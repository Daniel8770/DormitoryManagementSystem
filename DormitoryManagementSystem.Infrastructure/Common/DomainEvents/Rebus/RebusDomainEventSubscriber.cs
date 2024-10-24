using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.IntegrationMessages;
using DormitoryManagementSystem.Domain.SharedExpensesContext.IntegrationMessages;
using Rebus.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task kitchenBalanceCreatedEventSubscription = bus.Subscribe<KitchenBalanceCreatedEvent>();
        Task createSharedExpenseBalancerMessageSubscription = bus.Subscribe<CreateSharedExpenseBalancerMessage>();
        Task sharedExpenseBalancerCreatedMessageSubscription = bus.Subscribe<SharedExpenseBalancerCreatedMessage>();
        
        await kitchenBalanceCreatedEventSubscription;
        await createSharedExpenseBalancerMessageSubscription;
        await sharedExpenseBalancerCreatedMessageSubscription;
    }


}
