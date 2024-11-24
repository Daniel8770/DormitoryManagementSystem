using DormitoryManagementSystem.Domain.ClubsContext.IntegrationMessages;
using DormitoryManagementSystem.Domain.NotificationContext;
using Rebus.Handlers;

namespace DormitoryManagementSystem.Application.NotificationContext.Handlers;
internal class NotifyResourceBookedMessageHandler : IHandleMessages<NotifyResourceBookedMessage>
{
    public Task Handle(NotifyResourceBookedMessage message)
    {
        Notification notification = new Notification(NotificationId.Next(), message.RecipientId,
            $"You have booked unit '{message.UnitName}' on resource '{message.ResourceName}' at {message.DateBooked}.");
        return Task.CompletedTask;
    }
}

