using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;

namespace Cephiro.Listings.Application.Catalog.Contracts.Response;


public sealed class ListingInfoIntern
{
    public List<Uri>? Images { get; set; }
    public Location? Addresse { get; set; }
    public string? Description { get; set; }

    public float Price_day { get; set; }
    public DateTime Creation_date { get; set; }
    public ListingType Type { get; set; }
    public float Average_stars { get; set; } = 0;
    public Guid UserId{ get; set; }
    public string? Name { get; set; }

} 