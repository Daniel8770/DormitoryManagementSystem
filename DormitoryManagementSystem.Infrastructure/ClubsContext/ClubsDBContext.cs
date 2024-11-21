using DormitoryManagementSystem.Domain.ClubsContext;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext;
public class ClubsDBContext : DbContext
{
    public  DbSet<BookableResource> BookableResources { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Member> Members { get; set; }

    public ClubsDBContext(DbContextOptions<ClubsDBContext> options) : base(options)
    {
    }
}
