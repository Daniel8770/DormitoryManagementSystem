﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.Common.DomainEvents;
public interface IDomainEventPublisher
{
    void Publish(DomainEvent domainEvent);
}