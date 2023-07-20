using DijitaruVatigoGengar.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DijitaruVatigoGengar.Enums;
using Newtonsoft.Json.Converters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace DijitaruVatigoGengar.Data.Configs;

public class CollaboratorConfig : IEntityTypeConfiguration<Collaborator>
{
    public void Configure(EntityTypeBuilder<Collaborator> builder)
    {

        builder
            .HasKey(collaborator => collaborator.Id);

        builder
            .Property(collaborator => collaborator.Name)
            .IsRequired()
            .HasConversion(v => v.ToLowerInvariant(), v => v);

        builder
            .Property(collaborator => collaborator.BirthDate)
            .IsRequired();

        builder
            .Property(collaborator => collaborator.GenderText)
            .HasColumnName("Gender")
            .IsRequired()
            .HasConversion(v => v.ToLowerInvariant(), v => v);

        builder
            .Ignore(collaborator => collaborator.Gender);

        builder
            .Property(collaborator => collaborator.ModalityText)
        .HasColumnName("ContractModality")
            .IsRequired()
            .HasConversion(v => v.ToLowerInvariant(), v => v);

        builder
            .Ignore(collaborator => collaborator.Modality);

        builder
            .Property(collaborator => collaborator.RoleText)
            .HasColumnName("Role")
            .IsRequired()
            .HasConversion(v => v.ToLowerInvariant(), v => v);

        builder
            .Ignore(collaborator => collaborator.CollaboratorRole);

        builder
            .HasMany(collaborator => collaborator.PendingHours)
            .WithOne(pendingHour => pendingHour.Collaborator)
            .HasForeignKey(pendingHour => pendingHour.CollaboratorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}
