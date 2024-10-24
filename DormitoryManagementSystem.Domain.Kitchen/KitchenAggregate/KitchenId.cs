namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

public class KitchenId
{
    public Guid Value { get; init; }


    public static KitchenId Next() => new(Guid.NewGuid());
    public KitchenId(Guid value)
    {
        Value = value;
    }
}
