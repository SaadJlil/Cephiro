using Cephiro.Listings.Application.Reservation.Contracts.Validators.Request;
using Microsoft.Extensions.DependencyInjection;


namespace Cephiro.Listings.Application.Reservation.Contracts.Validators;


public static class ReservationValidatorConfig
{
    public static void AddReservationValidator(this IServiceCollection cfg)
    {
        cfg.AddReservationRequest();
        //cfg.AddCatalogResponse();
 
    }
}