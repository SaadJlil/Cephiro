using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class UserListingsRequestValidator: AbstractValidator<UserListingsRequest>
{
    public UserListingsRequestValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");

        //take and skip Limiter
        RuleFor(x => x.take).InclusiveBetween(1, 30).WithMessage("take is out of range");
        RuleFor(x => x.skip).GreaterThanOrEqualTo(0).WithMessage("skip must be positive");

        
    }
}