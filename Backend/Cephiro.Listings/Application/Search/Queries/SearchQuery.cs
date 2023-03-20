using Cephiro.Listings.Application.Search.Contracts.Request;
using MassTransit;

namespace Cephiro.Listings.Application.Search.Queries;

public static class SearchQuery 
{
    public static void AddSearchQueries(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<ListingSearchRequest>();
    }
}