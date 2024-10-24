namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;

public class KitchenBalanceId
{
    public Guid Value { get; init; }

    public static KitchenBalanceId Next() => new KitchenBalanceId(Guid.NewGuid());

    public KitchenBalanceId(Guid value)
    {
        Value = value;
    }   


}