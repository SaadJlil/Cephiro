using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class ListingInfoRequestValidator: AbstractValidator<ListingInfoRequest>
{
    public ListingInfoRequestValidator()
    {
        //Not null
        RuleFor(x => x.Id).NotEmpty().WithMessage("Listing Id is required");
    }
}