using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using ErrorOr;

namespace Cephiro.Listings.Application.Catalog.Queries;


public interface ICatalogAccess 
{
    public Task<ErrorOr<ListingInfoIntern>> GetListingInfo(ListingInfoRequest InfoListing, CancellationToken token);
}