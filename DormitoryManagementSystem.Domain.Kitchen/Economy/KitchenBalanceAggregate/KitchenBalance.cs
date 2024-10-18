using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;
public class KitchenBalance : Entity
{
    public Guid Id { get; init; }
    public KitchenBalanceInformation Information { get; private set; }
    public KitchenId KitchenId { get; init; }
    public ImmutableList<Resident> Residents => residents.ToImmutableList();

    private List<Resident> residents;


    internal KitchenBalance(Guid id, string name, KitchenId kitchenId, IEnumerable<Resident> residents)
    {
        Information = new KitchenBalanceInformation(name);
        this.residents = residents.ToList();
        KitchenId = kitchenId;  
        Id = id;
    }



}
