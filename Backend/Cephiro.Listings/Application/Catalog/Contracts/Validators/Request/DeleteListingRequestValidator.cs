using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class DeleteListingRequestValidator: AbstractValidator<DeleteListingRequest>
{
    public DeleteListingRequestValidator()
    {
        //Not null
        RuleFor(x => x.ListingId).NotEmpty().WithMessage("ListingId is required");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}