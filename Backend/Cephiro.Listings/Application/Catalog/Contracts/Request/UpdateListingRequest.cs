using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;


namespace Cephiro.Listings.Application.Catalog.Contracts.Request;

public sealed class UpdateListingRequest 
{
    public  IEnumerable<Uri>? Images { get; set; }
    public  Location? Addresse { get; set; }
    public string? Description { get; set; }
    public  float? Price_day { get; set; }
    public ListingType? Type { get; set; }
    public required Guid UserId{ get; set; }
    public required Guid ListingId{ get; set; }
    public string? Name { get; set; }
    public int? Beds { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public int? Surface { get; set; }
    public bool? Wifi { get; set; }
    public bool? AirConditioning { get; set; }
    public bool? Smoking { get; set; }
    public bool? WashingMachine { get; set; }
    public bool? DishWasher { get; set; }

}