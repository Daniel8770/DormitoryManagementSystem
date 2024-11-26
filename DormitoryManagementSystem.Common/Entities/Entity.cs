using DormitoryManagementSystem.Domain.Common.DomainEvents;

namespace DormitoryManagementSystem.Domain.Common.Entities;

public abstract class Entity<TIdentity> : DomainEventRaiser 
    where TIdentity : EntityId
{
    public TIdentity Id { get; init; }

    public Entity(TIdentity id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Entity<TIdentity> entity)
            return Id.Equals(entity.Id);
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator !=(Entity<TIdentity> entity1, Entity<TIdentity> entity2)
    {
        return !(entity1 == entity2);
    }

    public static bool operator ==(Entity<TIdentity> entity1, Entity<TIdentity> entity2)
    {
        return entity1.Equals(entity2);
    }
}
