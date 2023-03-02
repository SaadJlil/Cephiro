using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class UpdateListingValidator: AbstractValidator<UpdateListingRequest>
{
    public UpdateListingValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ListingId).NotEmpty();

        //String Length
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description length must be less than 500 characters.");
        RuleFor(x => x.Name).MaximumLength(70).WithMessage("Name length must be less than 70 characters.");
    }
}