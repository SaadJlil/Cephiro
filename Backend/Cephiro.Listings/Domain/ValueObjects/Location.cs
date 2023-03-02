using Cephiro.Listings.Domain.ValueObjects;
using Geolocation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Domain.ValueObjects;

public class Location: ValueObject
{
    [StringLength(100, ErrorMessage = "The street name must be less than 50 characters")] [Column("street")] public string Street {get; set;}
    [StringLength(100, ErrorMessage = "The country name must be less than 100 characters")] [Column("country")] public string Country {get; set;}
    [StringLength(100, ErrorMessage = "The city name must be less than 100 characters")] [Column("city")] public string City {get; set;}
    [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip code has 5 digits")] [Column("zipcode")] public string ZipCode {get; set;}
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")] [Column("longitude")] public double? Longitude { get; set; }
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")] [Column("latitude")] public double? Latitude { get; set; }
    public Location(string country, string street, string zipCode, string city, double? latitude, double? longitude)
    {
        Country = country;
        Street = street;
        ZipCode = zipCode;
        City = city;
        Latitude = latitude;
        Longitude = longitude;
    }

    public bool Is_Valid()
    {
        //Verify the Location (Youssef)
        return true;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        if(this.Longitude == null || this.Latitude == null)
        {
            return new List<object>(){this.Country ?? "", this.City ?? "", this.Street ?? ""};
        }
        else{
            return new List<object>(){this.Longitude, this.Latitude};
        }
        
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (Location)obj;

        if(this.Latitude == null || this.Longitude == null  || other.Latitude == null || other.Longitude == null)
        {
            return false; 
        }
        else if(GeoCalculator.GetDistance((double) this.Latitude,(double) this.Longitude, (double) other.Latitude, (double) other.Longitude, distanceUnit: DistanceUnit.Meters) < 200)
        {
            return true;
        }
        return false;
    }

    public static double operator -(Location a, Location b)
    {
        if (b == null || a == null || a.GetType() != b.GetType())
        {
            return -1.0;
        }

        if(a.Latitude == null || a.Longitude == null  || b.Latitude == null || b.Longitude == null)
        {
            return -1.0; 
        }
 
        return GeoCalculator.GetDistance((double) a.Latitude,(double) a.Longitude, (double) b.Latitude, (double) b.Longitude, distanceUnit: DistanceUnit.Meters);
    }

}