using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NewGengar.Models;

namespace NewGengar.Data.Configs;

public class PendingHourConfig : IEntityTypeConfiguration<PendingHour>
{
    public void Configure(EntityTypeBuilder<PendingHour> builder)
    {
        builder
            .HasKey(pendingHour => pendingHour.Id);

        builder
            .Property(pendingHour => pendingHour.HourAmount)
            .IsRequired();

        builder
            .Property(pendingHour => pendingHour.TimeMark)
            .IsRequired();

        builder
            .Property(pendingHour => pendingHour.StatusText)
            .HasColumnName("StatusText")
            .IsRequired()
            .HasConversion(value => value.ToLowerInvariant(), value => value);

        builder
            .Ignore(collaborator => collaborator.Status);
    }
}