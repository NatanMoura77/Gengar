using DijitaruVatigoGengar.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DijitaruVatigoGengar.Enums;

namespace DijitaruVatigoGengar.Data.Configs;

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
            .Property(pendingHour => pendingHour.RegisterHour)
            .IsRequired();

        builder
            .Property(collaborator => collaborator.StatusText)
            .HasColumnName("Status")
            .IsRequired()
            .HasConversion(v => v.ToLowerInvariant(), v => v);

        builder
            .Ignore(collaborator => collaborator.Status);
    }
}