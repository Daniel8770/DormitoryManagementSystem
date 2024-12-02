﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
public interface IKitchenRepository
{
    Task<Kitchen?> GetById(KitchenId id);

    Task Save(Kitchen kitchen);

    Task Update(Kitchen kitchen);

}
