using Cephiro.Listings.Application.Shared.Contracts.Internal;
using ErrorOr;



namespace Cephiro.Listings.Application.Catalog.Contracts.Response;

public sealed class ListingInfoResponse
{
    public ErrorOr<ListingInfoIntern> Info {get; set;}

}