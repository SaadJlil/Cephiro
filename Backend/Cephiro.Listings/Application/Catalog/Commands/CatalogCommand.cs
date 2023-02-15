using Cephiro.Listings.Application.Catalog.Contracts.Request;
using MassTransit;

namespace Cephiro.Listings.Application.Catalog.Commands;

public static class CatalogCommands
{
    public static void AddCatalogCommands(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<CreationRequest>();
    }
}