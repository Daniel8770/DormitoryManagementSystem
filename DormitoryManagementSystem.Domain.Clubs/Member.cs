using DormitoryManagementSystem.Domain.Common.Entities;


namespace DormitoryManagementSystem.Domain.ClubsContext;

public record MemberId(Guid Value) : EntityId<Guid>(Value)
{
    public static MemberId Next() => new(Guid.NewGuid());
}

public class Member : Entity<MemberId>
{
    public Member(MemberId id) : base(id)
    {
    }
}
