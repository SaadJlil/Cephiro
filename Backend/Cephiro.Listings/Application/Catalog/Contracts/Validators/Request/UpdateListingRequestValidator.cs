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

        //conditions
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description length must be less than 500 characters.")
            .When(x => x.Description is not null);

        RuleFor(x => x.Name)
            .MaximumLength(70)
            .WithMessage("Name length must be less than 70 characters.")
            .When(x => x.Name is not null);

        RuleFor(x => x.Beds)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The number of beds must be positive")
            .When(x => x.Beds is not null);
        RuleFor(x => x.Surface)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The surace must be positive")
            .When(x => x.Surface is not null);
        RuleFor(x => x.Bedrooms)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The number of Bedrooms must be positive")
            .When(x => x.Bedrooms is not null);
        RuleFor(x => x.Bathrooms)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The number of Bathrooms must be positive")
            .When(x => x.Bathrooms is not null);
        RuleFor(x => x.Price_day)
            .GreaterThan(0).WithMessage("The price must be positive")
            .When(x => x.Price_day is not null);
    }
}