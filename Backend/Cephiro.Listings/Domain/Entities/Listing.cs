using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;


namespace Cephiro.Listings.Domain.Entities;

[Table(name: "listing")]
public sealed class Listings: BaseEntity<Guid>
{
    [Column("images")] public List<Photos>? Images { get; set; }
    [Column("location")] public required Location Addresse { get; set; }
    [Column("description")] [MaxLength(500)] public string? Description { get; set; }
    [Column("price_day")] public required float Price_day { get; set; }
    [Column("number_reserved_days")] public int Number_reserved_days { get; set; } = 0;
    [Column("creation_date")] public required DateTime Creation_date { get; set; }
    [Column("listing_type")] public required ListingType Type { get; set; }
    [Column("average_stars")] public float Average_stars { get; set; } = 0;
    [Column("userid")] public required Guid UserId{ get; set; }
    [Column("name")] [MaxLength(70)] public required string Name { get; set; }
    [Column("number_views")] public int Views { get; set; } = 0;
    [Column("number_reviews")] public int NumberReviews { get; set; } = 0;
}
