using Cephiro.Listings.Application.Catalog.Commands;
using MassTransit;

namespace Cephiro.Listings.Application.Catalog;

public static class CatalogMediator
{
    public static void AddCatalogMediator(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddCatalogCommands();
    }
}