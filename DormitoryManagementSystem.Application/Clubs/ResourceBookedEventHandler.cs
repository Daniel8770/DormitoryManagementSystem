using DormitoryManagementSystem.Domain.ClubsContext.DomainEvents;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Application.Clubs;

internal class ResourceBookedEventHandler : IHandleMessages<ResourceBookedEvent>
{
    public Task Handle(ResourceBookedEvent message)
    {
        return Task.CompletedTask;
    }
}
