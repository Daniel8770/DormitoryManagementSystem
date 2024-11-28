using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.Common.DomainEvents;
public abstract class DomainEventRaiser
{
    private List<DomainEvent> domainEvents = new();
    public IEnumerable<DomainEvent> DomainEvents => domainEvents;
    public void Raise(DomainEvent domainEvent) =>
        domainEvents.Add(domainEvent);
}
