using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext.EFCore;
internal class UnitEntityConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
            id => id.Value,
            value => new UnitId(value));

        builder.Property(u => u.Name).HasConversion(
            name => name.Value,
            value => new Name(value));

        builder.Property(u => u.BookableResourceId).HasConversion(
            id => id.Value,
            value => new BookableResourceId(value));

    }
}
