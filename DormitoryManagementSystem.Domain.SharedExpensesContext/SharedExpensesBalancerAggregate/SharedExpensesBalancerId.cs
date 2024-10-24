namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

public class SharedExpensesBalancerId
{
    public Guid Value { get; init; }

    public static SharedExpensesBalancerId Next() => new(Guid.NewGuid());

    public SharedExpensesBalancerId(Guid value)
    {
        Value = value;
    }


}