using DormitoryManagementSystem.Domain.ClubsContext;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext.EFCore;

public class ClubsDBContext : DbContext
{
    public DbSet<BookableResource> BookableResources { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Member> Members { get; set; }

    public ClubsDBContext(DbContextOptions<ClubsDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UnitEntityConfiguration).Assembly);
    }
}
