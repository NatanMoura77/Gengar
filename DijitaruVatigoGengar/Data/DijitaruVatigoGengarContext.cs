using DijitaruVatigoGengar.Data.Configs;
using DijitaruVatigoGengar.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace DijitaruVatigoGengar.Data;

public class DijitaruVatigoGengarContext : DbContext
{
    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<PendingHour> PendingHours { get; set; }
    public DbSet<ProjectCollaborator> ProjectCollaborators { get; set; }

    public DijitaruVatigoGengarContext(DbContextOptions<DijitaruVatigoGengarContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectConfig());
        modelBuilder.ApplyConfiguration(new CollaboratorConfig());
        modelBuilder.ApplyConfiguration(new PendingHourConfig());
        modelBuilder.ApplyConfiguration(new ProjectCollaboratorConfig());
    }
}
