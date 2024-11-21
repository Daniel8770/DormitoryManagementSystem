using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext.EFCore;
internal class BookableResourceEntityConfiguration : IEntityTypeConfiguration<BookableResource>
{
    public void Configure(EntityTypeBuilder<BookableResource> builder)
    {
        builder.HasKey(br => br.Id);

        builder.Property(br => br.Id).HasConversion(
            id => id.Value,
            value => new BookableResourceId(value));

        builder.Property(br => br.Name).HasConversion(
            name => name.Value,
            value => new Name(value));

        builder.Property(br => br.Rules).HasConversion(
            rules => rules.Information,
            value => new Rules(value));

        // TODO: how do I handle polymorphic mapping of max bookings?

        builder.HasMany(br => br.Units)
            .WithOne()
            .HasForeignKey(u => u.BookableResourceId);

        builder.HasMany(br => br.Bookings)
            .WithOne()
            .HasForeignKey(b => b.BookableResourceId);
    }
}
