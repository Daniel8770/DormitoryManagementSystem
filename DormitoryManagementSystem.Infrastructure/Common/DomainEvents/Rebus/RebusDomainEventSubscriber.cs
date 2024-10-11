using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.KitchenContext.DomainEvents;
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
        Task kitchenAccountCreatedSubscription = bus.Subscribe<KitchenAccountCreated>();

        await kitchenAccountCreatedSubscription;
    }


}
