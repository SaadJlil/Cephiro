using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class DeleteUserListingsValidator: AbstractValidator<DeleteUserListingsRequest>
{
    public DeleteUserListingsValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty();
    }
}