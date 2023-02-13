using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;


namespace Cephiro.Listings.Application.Catalog.Contracts.Request;

public sealed class UpdateListingRequest 
{
    public  IEnumerable<Uri>? Images { get; set; }
    public  Location? Addresse { get; set; }
    [MaxLength(500)] public string? Description { get; set; }
    public  float? Price_day { get; set; }
    public ListingType? Type { get; set; }
    public required Guid UserId{ get; set; }
    public required Guid ListingId{ get; set; }
    [MaxLength(70)] public string? Name { get; set; }
}