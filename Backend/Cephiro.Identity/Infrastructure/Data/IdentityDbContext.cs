using Cephiro.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cephiro.Identity.Infrastructure.Data;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
        
    }

    public required DbSet<User> Users { get; set; }
    public required DbSet<Metrics> Metrics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(e => e.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasAlternateKey(e => e.Email);

        modelBuilder.Entity<User>()
            .HasIndex(e => e.PhoneNumber)
            .IsUnique();

        modelBuilder.Entity<Metrics>()
            .HasIndex(e => e.Id)
            .IsUnique();
    }
}