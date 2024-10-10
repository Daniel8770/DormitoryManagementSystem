using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.Common.DomainEvents;
public abstract class DomainEvent
{
    public Guid Id = Guid.NewGuid();
    public DateTime CreationTime = DateTime.Now;
}
