using Cephiro.Listings.Application.Reservation.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Reservation.Contracts.Validators.Request;



public sealed class CancelReservationRequestValidator: AbstractValidator<CancelReservationRequest>
{
    public CancelReservationRequestValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.ReservationId).NotEmpty().WithMessage("ReservationId is required");
    }
}