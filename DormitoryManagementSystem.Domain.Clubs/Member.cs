using DormitoryManagementSystem.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.ClubsContext;
public class Member : Entity<Guid>
{
    public Member(Guid id) : base(id)
    {
    }
}
