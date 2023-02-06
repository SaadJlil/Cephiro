using Cephiro.Listings.Domain.ValueObjects;
using Geolocation;


namespace Cephiro.Listings.Domain.ValueObjects;

public class Location: ValueObject
{
    public string Street {get; set;}
    public string Country {get; set;}
    public string City {get; set;}
    public string ZipCode {get; set;}
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public Location(string country, string street, string zipCode, string city, double? latitude, double? longitude)
    {
        Country = country;
        Street = street;
        ZipCode = zipCode;
        City = city;
        Latitude = latitude;
        Longitude = longitude;
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