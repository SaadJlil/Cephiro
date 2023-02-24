using Cephiro.Listings.Application.Shared.Contracts.Internal;
using ErrorOr;


namespace Cephiro.Listings.Application.Search.Contracts.Response;


public sealed class ListingSearchResponse 
{
    public IEnumerable<MinimalListingInfoInternal>? minilistings { get; set; }
    public bool IsError { get; set; } = false;
    public string? Message { get; set; }
    public int? code { get; set; }
}