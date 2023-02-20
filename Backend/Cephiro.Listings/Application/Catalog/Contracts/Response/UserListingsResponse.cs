using Cephiro.Listings.Application.Shared.Contracts.Internal;
using ErrorOr;


namespace Cephiro.Listings.Application.Catalog.Contracts.Response;


public sealed class UserListingsResponse
{
    public IEnumerable<MinimalListingInfoInternal>? minilistings { get; set; }
    public bool IsError { get; set; } = false;
    public string? Message { get; set; }
    public int? code { get; set; }
}