using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

public class KitchenInformation
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? Rules { get; init; }

    public static KitchenInformation Create(string name) => 
        new KitchenInformation(name, null, null);

    public static KitchenInformation CreateWithDescription(string name, string description) => 
        new KitchenInformation(name, description, null);

    public static KitchenInformation CreateWith(string name, string rules) =>
        new KitchenInformation(name, null, rules);

    public static KitchenInformation CreateWithDescriptionAndRules(string name, string description, string rules) => 
        new KitchenInformation(name, description, rules);

    private KitchenInformation(string name, string? description, string? rules)
    {
        Name = name;
        Description = description;
        Rules = rules;
    }

    public KitchenInformation UpdateRules(string rules) => new(Name, Description, rules);
    public KitchenInformation DeleteRules() => new(Name, Description, null);

    public KitchenInformation UpdateDescription(string description) => new(Name, description, Rules);
    public KitchenInformation DeleteDescription() => new(Name, null, Rules);
}
