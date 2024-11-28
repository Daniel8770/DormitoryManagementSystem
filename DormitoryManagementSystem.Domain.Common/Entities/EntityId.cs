namespace DormitoryManagementSystem.Domain.Common.Entities;

public abstract record EntityId;

public abstract record EntityId<TIdentity>(TIdentity Value) : EntityId
    where TIdentity : notnull;

