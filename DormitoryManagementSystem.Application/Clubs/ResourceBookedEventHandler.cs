using DormitoryManagementSystem.Domain.ClubsContext.DomainEvents;
using DormitoryManagementSystem.Domain.ClubsContext.IntegrationMessages;
using Rebus.Bus;
using Rebus.Handlers;

namespace DormitoryManagementSystem.Application.Clubs;

internal class ResourceBookedEventHandler : IHandleMessages<ResourceBookedEvent>
{
    private IBus bus;

    public ResourceBookedEventHandler(IBus bus)
    {
        this.bus = bus; 
    }

    public async Task Handle(ResourceBookedEvent message)
    {
        await bus.Send(new NotifyResourceBookedMessage(
            message.MemberId,    // here a member from clubs context is mapped to a recipient in notification context
            message.ResourceId,
            message.ResourceName,
            message.BookingId,
            message.UnitId,
            message.UnitName,
            message.DateBooked));
    }
}
