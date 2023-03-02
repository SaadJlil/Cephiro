using Cephiro.Listings.Application.Reservation.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Reservation.Contracts.Validators.Request;



public sealed class SubmitReviewRequestValidator: AbstractValidator<SubmitReviewRequest>
{
    public SubmitReviewRequestValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.ReservationId).NotEmpty().WithMessage("ReservationId is required");;

        //stars and desc
        RuleFor(x => x.Stars).NotNull().WithMessage("Star scoring is required")
                    .InclusiveBetween(0, 5)
                    .WithMessage("The number of stars has to be between 0 and 5");

        RuleFor(x => x.Description)
                    .MaximumLength(500)
                    .WithMessage("Review Description has to be less than 500 characters long")
                    .When(x => x.Description is not null);


   }
}