using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.ValueObjects;
using System.Xml.Linq;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public class UnitId
{
    public int Value { get; init; }
    public UnitId(int value)
    {
        Value = value;
    }
}

public class Unit : Entity<int>
{
    public Name Name { get; private set; }

    public Unit(UnitId id, string name) : base(id.Value)
    {
        Name = new Name(name);
    }

    public void ChangeName(string name)
    {
        Name = Name.ChangeName(name);
    }
}

 
