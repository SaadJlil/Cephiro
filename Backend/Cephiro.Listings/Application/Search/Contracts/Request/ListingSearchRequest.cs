using Cephiro.Listings.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Application.Search.Contracts.Enums;
using Cephiro.Listings.Application.Shared.CustomAttributes;


namespace Cephiro.Listings.Application.Search.Contracts.Request;

public sealed class ListingSearchRequest 
{
    // Might want to add coordinates for a better algo
    //Add number of rooms and surface
    [Range(1, 30, ErrorMessage = "Take has to be between 1 and 30")] public int take { get; set; } = 1;
    public int skip { get; set; } = 0;
    public ListingType? Type { get; set; }
    [StringLength(150, ErrorMessage = "Query has to be less than 150 characters long")] public string? QueryString { get; set; }
    public int MinimumPrice { get; set; }
    public int MaximumPrice { get; set; }
    [StringLength(100, ErrorMessage = "The country name must be less than 100 characters")] public required string Country{ get; set; }
    [StringLength(100, ErrorMessage = "The city name must be less than 100 characters")] public string? City { get; set; }
    //Add Condtiions on Startdate and enddate
    [StartTimeLimiter] public DateTime StartDate { get; set; } = DateTime.Now.ToUniversalTime();
    [EndTimeLimiter] public required DateTime EndDate { get; set; } 
    public PreferenceOrderBy? OrderBy { get; set; }
    [Range(100, 0)] public int? Beds { get; set; }
    [Range(100, 0)] public int? Bedrooms { get; set; }
    [Range(100, 0)] public int? Bathrooms { get; set; }
    public bool? Wifi { get; set; }
    public bool? AirConditioning { get; set; }
    public bool? Smoking { get; set; }
    public bool? WashingMachine { get; set; }
    public bool? DishWasher { get; set; }

}
