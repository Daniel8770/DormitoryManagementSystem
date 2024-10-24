using DormitoryManagementSystem.Domain.Common.Accounting;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenAccountAggregate;

public class KitchenAccount
{
    public KitchenAccountId Id { get; init; }
    public AccountId? AccountId { get; init; }
    public bool CanBeClosed => !hasDebit && !hasCredit;

    private bool hasDebit;
    private bool hasCredit;

    public KitchenAccount(KitchenAccountId id)
    {
        Id = id;
    }


}
