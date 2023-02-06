using entities = Cephiro.Listings.Domain.Entities ;
using Microsoft.EntityFrameworkCore;

namespace Cephiro.Listings.Infrastructure.Data;

public class ListingsDbContext : DbContext
{
    public ListingsDbContext(DbContextOptions<ListingsDbContext> options) : base(options)
    {
        
    }
    public required DbSet<entities.Tag> Tag { get; set; }
    public required DbSet<entities.Listings> Listings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<entities.Tag>()
            .HasIndex(e => e.Tag_string)
            .IsUnique();
   }
}