using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Catalog.Contracts.Request;

namespace Cephiro.Listings.Application.Catalog.Queries;


public interface ICatalogAccess 
{
    public Task<DbQueryInternal> UpdateListing(ListingInfoRequest Uplisting, CancellationToken token);
}