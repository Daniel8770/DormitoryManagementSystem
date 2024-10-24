using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.Common.DomainEvents;
public static class DomainEventStore
{
    [ThreadStatic] private static List<DomainEvent> events = new();

    // null check because of ThreadStatic events
    // if we are in a thread that has not called the constructor 
    // and no event has been raised events will be null and is thus
    // set to an empty list instead
    public static IEnumerable<DomainEvent> Events 
    { 
        get 
        { 
            if (events is null) 
                events = new(); 
            return events; 
        } 
    }

    public static void ClearEventStore()
    {
        events = new List<DomainEvent>();
    }

    public static void Raise(DomainEvent domainEvent)
    {
        // have to check for null because of ThreadStatic
        // 'events' is only initialized in the thread that
        // runs the constructor - for all other threads it will be null
        if (events is null)
            events = new();

        events.Add(domainEvent);
    }
}
