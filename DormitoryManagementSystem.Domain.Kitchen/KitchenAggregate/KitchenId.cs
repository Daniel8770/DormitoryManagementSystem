namespace DormitoryManagementSystem.Domain.Kitchen.KitchenAggregate;

public class KitchenId
{
    public Guid Value { get; init; }


    public static KitchenId Next() => new(Guid.NewGuid());
    private KitchenId(Guid value)
    {
        Value = value;
    }
}
