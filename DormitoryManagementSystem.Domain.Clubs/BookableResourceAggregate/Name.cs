﻿using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public record Name : ValueObject
{
    public string Value { get; init; }

    public Name(string name)
    {
        if (NameIsValid(name))
            Value = name;
        else throw NewInvalidNameException(name);
    }

    public Name ChangeName(string newName)
    {
        if (NameIsValid(newName))
            return this with { Value = newName };
        else throw NewInvalidNameException(newName);
    }

    private bool NameIsValid(string name) => !string.IsNullOrEmpty(name)
        && !string.IsNullOrWhiteSpace(name)
        && !name.All(char.IsDigit)
        && !char.IsDigit(name.First());

    private DomainException NewInvalidNameException(string name) =>
        new DomainException($"Name '{name}' is not valid.");
}
