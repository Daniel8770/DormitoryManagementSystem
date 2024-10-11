using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAccountAggregate;
public interface IKitchenAccountRepository
{
    void SaveOrUpdate(KitchenAccount kitchenAccount);
    KitchenAccount? GetById(KitchenAccountId id);

}
