using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenAccountAggregate;
public interface IKitchenAccountRepository
{
    void SaveOrUpdate(KitchenAccount kitchenAccount);
    KitchenAccount? GetById(KitchenAccountId id);

}
