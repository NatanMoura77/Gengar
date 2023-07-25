using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NewGengar.Models;

namespace NewGengar.Data.Configs;

public class CollaboratorConfig : IEntityTypeConfiguration<Collaborator>
{
    public void Configure(EntityTypeBuilder<Collaborator> builder)
    {
        builder
            .HasKey(collaborator => collaborator.Id);

        builder
            .Property(collaborator => collaborator.Name)
            .IsRequired()
            .HasConversion(collaborator => collaborator.ToLowerInvariant(), collaborator => collaborator);

        builder
            .Property(collaborator => collaborator.DateOfBirth)
            .IsRequired();

        builder
            .Property(collaborator => collaborator.GenderText)
            .HasColumnName("GenderText")
            .IsRequired()
            .HasConversion(value => value.ToLowerInvariant(), value => value);
        builder
            .Ignore(collaborator => collaborator.Gender);

        builder
            .Property(collaborator => collaborator.RoleText)
            .HasColumnName("RoleText")
            .IsRequired()
            .HasConversion(value => value.ToLowerInvariant(), value => value);

        builder
            .Ignore(collaborator => collaborator.CollaboratorRole);

        builder
           .Property(collaborator => collaborator.ModalityText)
           .HasColumnName("ModalityText")
           .IsRequired()
           .HasConversion(value => value.ToLowerInvariant(), value => value);

        builder
            .Ignore(collaborator => collaborator.Modality);

        builder
           .HasMany(collaborator => collaborator.PendingHours)
           .WithOne(pendingHour => pendingHour.Collaborator)
           .HasForeignKey(pendingHour => pendingHour.CollaboratorId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);
    }
}