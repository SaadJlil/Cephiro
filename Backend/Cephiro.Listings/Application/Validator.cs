using Microsoft.Extensions.DependencyInjection;
using Cephiro.Listings.Application.Catalog.Contracts.Validators;
using Cephiro.Listings.Application.Reservation.Contracts.Validators;
using Cephiro.Listings.Application.Search.Contracts.Validators;


namespace Cephiro.Listings.Application;


public static class Validator
{
    public static void AddListingValidator(this IServiceCollection cfg)
    {
        cfg.AddCatalogValidator();
        cfg.AddReservationValidator();
        cfg.AddSearchValidator();
    }
}