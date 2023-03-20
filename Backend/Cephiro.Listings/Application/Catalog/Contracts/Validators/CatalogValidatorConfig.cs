using Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;
using Microsoft.Extensions.DependencyInjection;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators;


public static class CatalogValidatorConfig
{
    public static void AddCatalogValidator(this IServiceCollection cfg)
    {
        cfg.AddCatalogRequest();
        //cfg.AddCatalogResponse();
 
    }
}