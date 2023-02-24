using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Search.Contracts.Request;
using Cephiro.Listings.Application.Search.Contracts.Response;


namespace Cephiro.Listings.Application.Search.Queries;


public interface ISearchAccess
{
    public Task<ListingSearchResponse> GetListingSearch(ListingSearchRequest Search, CancellationToken token);
}