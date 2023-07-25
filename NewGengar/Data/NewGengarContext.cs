namespace NewGengar.Data;

using Microsoft.EntityFrameworkCore;
using NewGengar.Data.Configs;
using NewGengar.Models;

public class NewGengarContext : DbContext
{
    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<PendingHour> PendingHours { get; set; }

    public NewGengarContext(DbContextOptions<NewGengarContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectConfig());
        modelBuilder.ApplyConfiguration(new CollaboratorConfig());
        modelBuilder.ApplyConfiguration(new PendingHourConfig());
    }
}
