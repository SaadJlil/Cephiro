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
    [Range(100, 0)] [Column("beds")]  public required int Beds { get; set; }
    [Range(100, 0)] [Column("surface")]  public required int Surface { get; set; }
    [Range(100, 0)] [Column("bedrooms")]  public required int Bedrooms { get; set; }
    [Range(100, 0)] [Column("bathrooms")]  public required int Bathrooms { get; set; }
    [Column("wifi")]  public required bool Wifi { get; set; }
    [Column("airconditioning")]  public required bool AirConditioning { get; set; }
    [Column("smoking")]  public required bool Smoking { get; set; }
    [Column("washing_machine")]  public required bool WashingMachine { get; set; }
    [Column("dish_washer")]  public required bool DishWasher { get; set; }

}
