using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.Kitchen.KitchenAggregate;

public class KitchenDescription : ValueObject
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? Rules { get; init; }

    public static KitchenDescription Create(string name) => 
        new KitchenDescription(name, null, null);

    public static KitchenDescription Create(string name, string description) => 
        new KitchenDescription(name, description, null);

    public static KitchenDescription Create(string name, string description, string rules) => 
        new KitchenDescription(name, description, rules);

    private KitchenDescription(string name, string? description, string? rules)
    {
        Name = name;
        Description = description;
        Rules = rules;
    }

    public KitchenDescription UpdateRules(string rules) => new(Name, Description, rules);
    public KitchenDescription DeleteRules() => new(Name, Description, null);

    public KitchenDescription UpdateDescription(string description) => new(Name, description, Rules);
    public KitchenDescription DeleteDescription() => new(Name, null, Rules);
}
