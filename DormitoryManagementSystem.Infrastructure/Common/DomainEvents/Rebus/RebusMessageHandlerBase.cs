using Rebus.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Infrastructure.Common.DomainEvents.Rebus;
public class RebusMessageHandlerBase<T>
{
    private IBus bus;

    public RebusMessageHandlerBase(IBus bus)
    {
        this.bus = bus;
    }
}
