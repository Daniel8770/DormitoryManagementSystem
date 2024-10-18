using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.KitchenContext;
public class Resident
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Phonenumber { get; private set; }
    public string Email { get; private set; }
    public string RoomNumber { get; private set; }

    public static Resident CreateNew(string name, string phoneNumber, string email, string roomNumber) => 
        new(Guid.NewGuid(), name, phoneNumber, email, roomNumber);

    private Resident(Guid id, string name, string phonenumber, string email, string roomNumber)
    {
        Id = id;
        Name = name;
        Phonenumber = phonenumber;
        Email = email;
        RoomNumber = roomNumber;
    }
}
