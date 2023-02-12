using entities = Cephiro.Listings.Domain.Entities ;
using Microsoft.EntityFrameworkCore;

namespace Cephiro.Listings.Infrastructure.Data;

public class ListingsDbContext : DbContext
{
    public ListingsDbContext(DbContextOptions<ListingsDbContext> options) : base(options)
    {
        
    }
    public required DbSet<entities.Listings> Listing { get; set; }
    public required DbSet<entities.Photos> Image { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<entities.Listings>()
            .OwnsOne(e => e.Addresse);
    }
}