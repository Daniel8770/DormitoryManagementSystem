using DormitoryManagementSystem.Domain.Common.Entities;



namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

public record class ParticipantId(Guid Value) : EntityId<Guid>(Value)
{
    public static ParticipantId Next() => new(Guid.NewGuid());
}

public class Participant : Entity<ParticipantId>
{
    public Participant(ParticipantId id) : base(id)
    {
    }

    public static bool operator ==(Participant left, Participant right) =>
        left.Equals(right);

    public static bool operator !=(Participant left, Participant right) =>
        !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is Participant residentId)
            return residentId.Id == Id;

        return false;
    }

    public override int GetHashCode() => Id.GetHashCode();

}
