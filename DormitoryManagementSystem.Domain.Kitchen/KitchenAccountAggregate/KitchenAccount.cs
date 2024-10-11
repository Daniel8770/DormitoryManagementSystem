
using DormitoryManagementSystem.Domain.Common.Accounting;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAccountAggregate;

public class KitchenAccount
{
    public KitchenAccountId Id { get; init; }
    public AccountId AccountId { get; init; }

    public KitchenAccount(KitchenAccountId id)
    {
        Id = id;
    }


}
