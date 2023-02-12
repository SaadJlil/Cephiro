using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;


namespace Cephiro.Listings.Application.Catalog.Contracts.Request;

public sealed class CreationRequest
{
    [Required] public required IEnumerable<Uri> Images { get; set; }
    [Required] public Location? Addresse { get; set; }
    [MaxLength(500)] public string? Description { get; set; }
    [Required] public float Price_day { get; set; }
    [Required] public ListingType Type { get; set; }

    public IEnumerable<string>? Tags { get; set; }
    public required Guid UserId{ get; set; }
    [MaxLength(70)] [Required] public string? Name { get; set; }
}