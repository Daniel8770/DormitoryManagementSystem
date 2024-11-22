using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.KitchenContext.Economy.KitchenBalanceAggregate;

public record KitchenBalanceInformation(string Name, string? Description)
{
    public static KitchenBalanceInformation Create(string name) => new(name, null);

    public KitchenBalanceInformation ChangeName(string newName)
    {
        if (string.IsNullOrEmpty(newName))
            throw new DomainException("Title cannot be empty");

        return this with { Name = newName };
    }

    public KitchenBalanceInformation ChangeDescription(string newDescription) => 
        this with { Description = newDescription };

    public KitchenBalanceInformation RemoveDescription() => this with { Description = null };

}