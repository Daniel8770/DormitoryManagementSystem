namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

public class SharedExpensesGroupId
{
    public Guid Value { get; init; }

    public static SharedExpensesGroupId Next() => new(Guid.NewGuid());

    public SharedExpensesGroupId(Guid value)
    {
        Value = value;
    }


}