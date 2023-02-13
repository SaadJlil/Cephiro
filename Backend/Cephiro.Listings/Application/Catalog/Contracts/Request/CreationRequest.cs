using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;


namespace Cephiro.Listings.Application.Catalog.Contracts.Request;

public sealed class CreationRequest
{
    public required IEnumerable<Uri> Images { get; set; }
    public required Location Addresse { get; set; }
    [MaxLength(500)] public string? Description { get; set; }
    public required float Price_day { get; set; }
    public required ListingType Type { get; set; }
    public required Guid UserId{ get; set; }
    [MaxLength(70)] public required string Name { get; set; }
}