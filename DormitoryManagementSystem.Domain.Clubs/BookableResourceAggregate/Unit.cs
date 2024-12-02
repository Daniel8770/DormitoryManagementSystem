﻿using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.ValueObjects;
using System.Xml.Linq;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public record UnitId(int Value) : EntityId<int>(Value);

public class Unit : Entity<UnitId>
{
    public Name Name { get; private set; }
    public BookableResourceId BookableResourceId { get; private set; }

    public Unit(UnitId id, BookableResourceId bookableResourceId, string name) : base(id)
    {
        Name = new Name(name);
        BookableResourceId = bookableResourceId;
    }

    public void ChangeName(string name)
    {
        Name = Name.ChangeName(name);
    }
}


