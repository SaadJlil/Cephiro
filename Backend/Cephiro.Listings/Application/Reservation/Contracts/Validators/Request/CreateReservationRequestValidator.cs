using Cephiro.Listings.Application.Reservation.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Reservation.Contracts.Validators.Request;



public sealed class CreateReservationRequestValidator: AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.ListingId).NotEmpty().WithMessage("ListingId is required");;

        //Date 
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required").
            Must(   
                x => DateTime.Today <= x && 
                DateTime.Today.AddYears(1) > x
            ).WithMessage("Start Date must be between today and next year");
 
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("EndDate is required").
            Must(   
                x => DateTime.Today.AddDays(1) <= x && 
                DateTime.Today.AddYears(1) >  x 
            ).WithMessage("End Date must be between tomorrow and next year").
            GreaterThanOrEqualTo(x => x.StartDate.AddDays(1)).
            WithMessage("EndDate cannot be before StartDate");
    }
}