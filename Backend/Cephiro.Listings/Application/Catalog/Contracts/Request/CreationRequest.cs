using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;


namespace Cephiro.Listings.Application.Catalog.Contracts.Request;

public sealed class CreationRequest
{
    public required IEnumerable<Uri> Images { get; set; }
    public required Location Addresse { get; set; }
    public string? Description { get; set; }
    public required float Price_day { get; set; }
    public required ListingType Type { get; set; }
    public required Guid UserId{ get; set; }
    public required string Name { get; set; }
    public required int Beds { get; set; }
    public required int Bedrooms { get; set; }
    public required int Bathrooms { get; set; }
    public bool Wifi { get; set; } = false;
    public bool AirConditioning { get; set; } = false;
    public bool Smoking { get; set; } = false;
    public bool WashingMachine { get; set; } = false;
    public bool DishWasher { get; set; } = false;

}


/*
public sealed class CreationRequest
{
    public required IEnumerable<Uri> Images { get; set; }
    public required Location Addresse { get; set; }
    [MaxLength(500)] public string? Description { get; set; }
    public required float Price_day { get; set; }
    public required ListingType Type { get; set; }
    public required Guid UserId{ get; set; }
    [MaxLength(70)] public required string Name { get; set; }
    public required int Beds { get; set; }
    public required int Bedrooms { get; set; }
    public required int Bathrooms { get; set; }
    public required bool Wifi { get; set; }
    public required bool AirConditioning { get; set; }
    public required bool Smoking { get; set; }
    public required bool WashingMachine { get; set; }
    public required bool DishWasher { get; set; }

}
*/