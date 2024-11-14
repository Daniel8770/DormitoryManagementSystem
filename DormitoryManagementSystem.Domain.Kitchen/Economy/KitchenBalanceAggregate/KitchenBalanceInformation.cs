using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;

public class KitchenBalanceInformation
{
    public string Name { get; private set; }
    public string? Description { get; private set; } 

    public KitchenBalanceInformation(string name) : this(name, null) { }

    private KitchenBalanceInformation(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public KitchenBalanceInformation ChangeName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new DomainException("Title cannot be empty");

        return new KitchenBalanceInformation(newName, Description);
    }

    public KitchenBalanceInformation ChangeDescription(string newDescription) => 
        new KitchenBalanceInformation(Name, newDescription);

    public KitchenBalanceInformation RemoveDescription() => new(Name);

}