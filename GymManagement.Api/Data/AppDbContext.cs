using GymManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace GymManagement.Api.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasQueryFilter(c =>  !c.IsDeleted)
            .HasIndex(c => c.Email)
            .IsUnique();
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<WorkoutSession> WorkoutSessions { get; set; }
}