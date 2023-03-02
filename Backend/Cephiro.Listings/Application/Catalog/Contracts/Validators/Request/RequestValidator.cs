using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public static class RequestValidator
{
    public static void AddCatalogRequest(this IServiceCollection cfg)
    {
        cfg.AddValidatorsFromAssemblyContaining<CreationRequestValidator>();
        cfg.AddValidatorsFromAssemblyContaining<DeleteListingRequestValidator>();
        cfg.AddValidatorsFromAssemblyContaining<DeleteUserListingsRequestValidator>();
        cfg.AddValidatorsFromAssemblyContaining<ListingInfoRequestValidator>();
        cfg.AddValidatorsFromAssemblyContaining<UpdateListingRequestValidator>();
        cfg.AddValidatorsFromAssemblyContaining<UserListingsRequestValidator>();
    }
}