using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class ListingInfoValidator: AbstractValidator<ListingInfoRequest>
{
    public ListingInfoValidator()
    {
        //Not null
        RuleFor(x => x.Id).NotEmpty();
    }
}