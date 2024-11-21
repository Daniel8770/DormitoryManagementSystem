using DormitoryManagementSystem.Domain.Common.Entities;

namespace DormitoryManagementSystem.Domain.Common.Aggregates;

public abstract class AggregateRoot<TIdentity> : Entity<TIdentity> where TIdentity : EntityId
{
    public AggregateRoot(TIdentity id) : base(id)
    {
    }
}
