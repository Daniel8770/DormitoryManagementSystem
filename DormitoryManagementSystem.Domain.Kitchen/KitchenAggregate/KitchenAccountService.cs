using DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenAccountAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
public class KitchenAccountService
{
    private IKitchenRepository kitchenRepository;
    private IKitchenAccountRepository kitchenAccountRepository;

    public KitchenAccountService(IKitchenRepository kitchenRepository, IKitchenAccountRepository kitchenAccountRepository)
    {
        this.kitchenRepository = kitchenRepository;
        this.kitchenAccountRepository = kitchenAccountRepository;
    }

    public void CreateNewOnKitchen(KitchenId id)
    {
        Kitchen? kitchen = kitchenRepository.GetById(id);

        if (kitchen is null)
            throw new Exception($"Kitchen {id.Value} not found.");

        KitchenAccountId kitchenAccountId = KitchenAccountId.Next();
        KitchenAccount newKitchenAccount = new(kitchenAccountId);

        kitchen.AddKitchenAccount(newKitchenAccount);

        kitchenRepository.SaveOrUpdate(kitchen);
        kitchenAccountRepository.SaveOrUpdate(newKitchenAccount);

        // TODO: raise event
    }

    public void GetKitchenAccount(KitchenAccountId id)
    {
        KitchenAccount? kitchenAccount = kitchenAccountRepository.GetById(id);

        if (kitchenAccount is null)
            throw new Exception($"Kitchen account {id.Value} not found.");

        
    }
}
