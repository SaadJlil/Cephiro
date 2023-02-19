using Cephiro.Listings.Application.Catalog.Contracts.Request;
using MassTransit;

namespace Cephiro.Listings.Application.Catalog.Queries;

public static class CatalogQuery
{
    public static void AddCatalogQueries(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<ListingInfoRequest>();
    }
}