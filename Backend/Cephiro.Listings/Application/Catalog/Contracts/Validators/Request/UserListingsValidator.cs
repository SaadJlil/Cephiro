using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class UserListingsValidator: AbstractValidator<UserListingsRequest>
{
    public UserListingsValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty();

        //take and skip Limiter
        RuleFor(x => x.take).InclusiveBetween(1, 40);

        //Don't forget skip ********
        
    }
}