using DormitoryManagementSystem.Domain.Common.ValueObjects;
using System.Runtime.CompilerServices;

namespace DormitoryManagementSystem.Domain.KitchenContext.KitchenAggregate;

public record KitchenInformation : ValueObject
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public string? Rules { get; init; }

    public static KitchenInformation Create(string name) => 
        new KitchenInformation(name, null, null);

    public static KitchenInformation CreateWithDescription(string name, string description) => 
        new KitchenInformation(name, description, null);

    public static KitchenInformation CreateWithRules(string name, string rules) =>
        new KitchenInformation(name, null, rules);

    public static KitchenInformation CreateWithDescriptionAndRules(string name, string description, string rules) => 
        new KitchenInformation(name, description, rules);

    private KitchenInformation(string name, string? description, string? rules)
    {
        Name = name;
        Description = description;
        Rules = rules;
    }

    public KitchenInformation UpdateRules(string rules) => this with { Rules = rules };
    public KitchenInformation DeleteRules() => this with { Rules = null };

    public KitchenInformation UpdateDescription(string description) => this with { Description = description };
    public KitchenInformation DeleteDescription() => this with { Description = null };
}
