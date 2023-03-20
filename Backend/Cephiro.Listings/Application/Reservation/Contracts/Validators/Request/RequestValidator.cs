using Microsoft.Extensions.DependencyInjection;
using FluentValidation;


namespace Cephiro.Listings.Application.Reservation.Contracts.Validators.Request;



public static class RequestValidator
{
    public static void AddReservationRequest(this IServiceCollection cfg)
    {
        cfg.AddValidatorsFromAssemblyContaining<CancelReservationRequestValidator>();
        cfg.AddValidatorsFromAssemblyContaining<CreateReservationRequestValidator>();
    }
}