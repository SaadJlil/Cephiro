using Cephiro.Listings.Application.Reservation.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Reservation.Contracts.Validators.Request;



public sealed class ListingReservationDatesRequestValidator: AbstractValidator<ListingReservationDatesRequest>
{
    public ListingReservationDatesRequestValidator()
    {
        //Not null
        RuleFor(x => x.ListingId).NotEmpty().WithMessage("ListingId is required");
    }
}