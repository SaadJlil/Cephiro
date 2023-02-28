using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.Enums;
using Cephiro.Listings.Application.Search.Contracts.Enums;


namespace Cephiro.Listings.Application.Search.Contracts.Request;

public sealed class ListingSearchRequest 
{
    // Might want to add coordinates for a better algo
    //Add number of rooms and surface
    public int take { get; set; } = 1;
    public int skip { get; set; } = 0;
    public ListingType? Type { get; set; }
    public string? QueryString { get; set; }
    public int MinimumPrice { get; set; }
    public int MaximumPrice { get; set; }
    public required string Country{ get; set; }
    public string? City { get; set; }
    //Add Condtiions on Startdate and enddate
    public DateTime StartDate { get; set; } = DateTime.Now.ToUniversalTime();
    public required DateTime EndDate { get; set; } 
    public PreferenceOrderBy? OrderBy { get; set; }
    public int? Beds { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public bool? Wifi { get; set; }
    public bool? AirConditioning { get; set; }
    public bool? Smoking { get; set; }
    public bool? WashingMachine { get; set; }
    public bool? DishWasher { get; set; }

}