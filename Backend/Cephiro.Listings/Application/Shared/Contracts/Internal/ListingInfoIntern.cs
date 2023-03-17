using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;

namespace Cephiro.Listings.Application.Catalog.Contracts.Response;


public sealed class ListingInfoIntern
{
    public Guid ListingId { get; set; }
    public List<Uri>? Images { get; set; }
    public required Location Addresse { get; set; }
    public string? Description { get; set; }
    public float Price_day { get; set; }
    public DateTime Creation_date { get; set; }
    public ListingType Type { get; set; }
    public required float Average_stars { get; set; }
    public Guid UserId{ get; set; }
    public required string Name { get; set; }
    public int NumberReviews { get; set; }
    public required int Beds { get; set; }
    public required int Bedrooms { get; set; }
    public required int Surface { get; set; }
    public required int Bathrooms { get; set; }
    public bool Wifi { get; set; }
    public bool AirConditioning { get; set; }
    public bool Smoking { get; set; } 
    public bool WashingMachine { get; set; } 
    public bool DishWasher { get; set; } 

} 