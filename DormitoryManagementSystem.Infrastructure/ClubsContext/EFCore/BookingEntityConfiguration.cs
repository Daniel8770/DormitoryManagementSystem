
using DormitoryManagementSystem.Domain.ClubsContext;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext.EFCore;
internal class BookingEntityConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).HasConversion(
            id => id.Value,
            value => new BookingId(value));

        builder.Property(b => b.UnitId).HasConversion(
            id => id.Value,
            value => new UnitId(value));

        builder.Property(b => b.MemberId).HasConversion(
            id => id.Value,
            value => new MemberId(value));

        builder.Property(b => b.BookableResourceId).HasConversion(
            id => id.Value,
            value => new BookableResourceId(value));

        builder.Property(b => b.TimePeriod.StartDate)
            .HasColumnName("StartDate");

        builder.Property(b => b.TimePeriod.EndDate)
            .HasColumnName("EndDate");

        // TODO: how do I handle polymorphic mapping of days and hours?
    }
}

