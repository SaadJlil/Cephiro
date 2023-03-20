using Cephiro.Listings.Application.Search.Contracts.Validators.Request;
using Microsoft.Extensions.DependencyInjection;


namespace Cephiro.Listings.Application.Search.Contracts.Validators;


public static class SearchValidatorConfig
{
    public static void AddSearchValidator(this IServiceCollection cfg)
    {
        cfg.AddSearchRequest();
        //cfg.AddSearchResponse();
    }
}