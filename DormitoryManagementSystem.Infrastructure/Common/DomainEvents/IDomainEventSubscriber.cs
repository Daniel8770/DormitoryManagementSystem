using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents;
public interface IDomainEventSubscriber
{
    Task SubscribeToAllEvents();
}
