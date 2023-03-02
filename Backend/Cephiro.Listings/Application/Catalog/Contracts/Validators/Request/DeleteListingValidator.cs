using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class DeleteListingValidator: AbstractValidator<DeleteListingRequest>
{
    public DeleteListingValidator()
    {
        //Not null
        RuleFor(x => x.ListingId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}