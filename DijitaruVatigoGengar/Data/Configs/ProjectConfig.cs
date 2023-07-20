using DijitaruVatigoGengar.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DijitaruVatigoGengar.Enums;

namespace DijitaruVatigoGengar.Data.Configs;

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
            .HasKey(project => project.Id);

        builder
            .Property(project => project.Name)
            .IsRequired()
            .HasConversion(v => v.ToLowerInvariant(), v => v);

        builder
            .Property(project => project.Budget)
            .IsRequired();


        builder
            .Property(collaborator => collaborator.TypeText)
            .HasColumnName("ProjectType")
            .IsRequired()
            .HasConversion(v => v.ToLowerInvariant(), v => v);

        builder
            .Ignore(collaborator => collaborator.Type);

        builder
            .HasMany(project => project.PendingHours)
            .WithOne(pendingHour => pendingHour.Project)
            .HasForeignKey(pendingHour => pendingHour.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
