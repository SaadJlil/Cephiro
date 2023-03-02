using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class UpdateListingRequestValidator: AbstractValidator<UpdateListingRequest>
{
    public UpdateListingRequestValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.ListingId).NotEmpty().WithMessage("UserId is required ");

        //String Length
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description length must be less than 500 characters.");
        RuleFor(x => x.Name).MaximumLength(70).WithMessage("Name length must be less than 70 characters.");
    }
}