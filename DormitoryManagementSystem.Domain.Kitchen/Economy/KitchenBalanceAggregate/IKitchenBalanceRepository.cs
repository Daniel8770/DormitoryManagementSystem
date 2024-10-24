using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
public interface IKitchenBalanceRepository
{
    Task<KitchenBalance?> GetById(KitchenBalanceId id);
    Task Save(KitchenBalance kitchenBalance);

    Task Update(KitchenBalance kitchenBalance);

    Task<bool> AlreadyExistsWithName(string name);


}
