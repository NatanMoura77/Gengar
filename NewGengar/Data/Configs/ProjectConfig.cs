using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NewGengar.Models;

namespace NewGengar.Data.Configs;

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
            .HasKey(project => project.Id);

        builder
            .Property(project => project.Name)
            .IsRequired()
            .HasConversion(project => project.ToLowerInvariant(), project => project);

        builder
            .Property(project => project.Budget)
            .IsRequired();


        builder
            .Property(collaborator => collaborator.TypeText)
            .HasColumnName("TypeText")
            .IsRequired()
            .HasConversion(value => value.ToLowerInvariant(), value => value);

        builder
            .Ignore(collaborator => collaborator.Type);

        builder.HasMany(project => project.Collaborators)
            .WithMany(collaborator => collaborator.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "ProjectCollaborators",
                j => j.HasOne<Collaborator>().WithMany().HasForeignKey("CollaboratorId")
                .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId")
                .OnDelete(DeleteBehavior.Cascade)
            );

        builder.HasMany(project => project.Approvers)
            .WithMany(collaborator => collaborator.ApproversProject)
            .UsingEntity<Dictionary<string, object>>(
                "ApproversProject",
                j => j.HasOne<Collaborator>().WithMany().HasForeignKey("ApproverId")
                .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId")
                .OnDelete(DeleteBehavior.Cascade)
            );

        builder
            .HasMany(project => project.PendingHours)
            .WithOne(pendingHour => pendingHour.Project)
            .HasForeignKey(pendingHour => pendingHour.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


    }
}