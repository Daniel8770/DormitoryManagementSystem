using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.Entities;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenAccountAggregate;

public record KitchenAccountId(Guid Value) : EntityId<Guid>(Value)
{
    public static KitchenAccountId Next() => new(Guid.NewGuid());
}

public class KitchenAccount : Entity<KitchenAccountId>
{
    public AccountId? AccountId { get; init; }
    public bool CanBeClosed => !hasDebit && !hasCredit;

    private bool hasDebit;
    private bool hasCredit;

    public KitchenAccount(KitchenAccountId id) : base(id)
    {
    }


}
