using DormitoryManagementSystem.Domain.Common.Entities;



namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
public class Participant : Entity
{
    public Guid Id { get; init; }

    public Participant(Guid id)
    {
        Id = id;
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
