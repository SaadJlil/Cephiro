using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class DeleteUserListingsRequestValidator: AbstractValidator<DeleteUserListingsRequest>
{
    public DeleteUserListingsRequestValidator()
    {
        //Not null
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}