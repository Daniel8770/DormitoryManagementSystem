using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
public interface IKitchenRepository
{
    Kitchen? GetById(KitchenId id);

    void SaveOrUpdate(Kitchen kitchen);
}
