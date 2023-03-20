using Cephiro.Listings.Application.Reservation.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Reservation.Contracts.Validators.Request;



public sealed class UserReservationRequestValidator: AbstractValidator<UserReservationsRequest>
{
    public UserReservationRequestValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}