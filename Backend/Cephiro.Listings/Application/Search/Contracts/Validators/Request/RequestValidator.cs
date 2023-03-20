using Microsoft.Extensions.DependencyInjection;
using FluentValidation;


namespace Cephiro.Listings.Application.Search.Contracts.Validators.Request;



public static class RequestValidator
{
    public static void AddSearchRequest(this IServiceCollection cfg)
    {
        cfg.AddValidatorsFromAssemblyContaining<ListingSearchRequestValidator>();
    }
}