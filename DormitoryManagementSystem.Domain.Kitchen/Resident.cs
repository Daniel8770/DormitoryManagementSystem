using DormitoryManagementSystem.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext;

public record ResidentId(Guid Value) : EntityId<Guid>(Value)
{
    public static ResidentId Next() => new(Guid.NewGuid());
}

public class Resident : Entity<ResidentId>
{
    public string Name { get; private set; }
    public string Phonenumber { get; private set; }
    public string Email { get; private set; }
    public string RoomNumber { get; private set; }

    public static Resident CreateNew(string name, string phoneNumber, string email, string roomNumber) => 
        new(ResidentId.Next(), name, phoneNumber, email, roomNumber);

    private Resident(ResidentId id, string name, string phonenumber, string email, string roomNumber)
        : base(id)
    {
        Name = name;
        Phonenumber = phonenumber;
        Email = email;
        RoomNumber = roomNumber;
    }
}
