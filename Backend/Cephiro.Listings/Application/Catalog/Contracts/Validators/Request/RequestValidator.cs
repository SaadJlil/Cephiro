using Microsoft.Extensions.DependencyInjection;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public static class RequestValidator
{
    public static void AddCatalogRequest(this IServiceCollection cfg)
    {
        cfg.AddValidatorsFromAssemblyContaining<CreationValidator>();
        cfg.AddValidatorsFromAssemblyContaining<DeleteListingValidator>();
        cfg.AddValidatorsFromAssemblyContaining<DeleteUserListingsValidator>();
        cfg.AddValidatorsFromAssemblyContaining<ListingInfoValidator>();
        cfg.AddValidatorsFromAssemblyContaining<UpdateListingValidator>();
        cfg.AddValidatorsFromAssemblyContaining<UserListingsValidator>();
    }
}