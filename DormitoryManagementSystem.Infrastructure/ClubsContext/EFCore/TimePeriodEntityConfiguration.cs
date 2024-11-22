using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext.EFCore;
internal class TimePeriodEntityConfiguration : IEntityTypeConfiguration<TimePeriod>
{
    public void Configure(EntityTypeBuilder<TimePeriod> builder)
    {
        builder.HasDiscriminator<string>("TimePeriodType")
            .HasValue<DaysTimePeriod>("Days")
            .HasValue<HoursTimePeriod>("Hours");
    }
}
