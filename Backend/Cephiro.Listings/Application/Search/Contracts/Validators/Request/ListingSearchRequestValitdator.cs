using Cephiro.Listings.Application.Search.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Search.Contracts.Validators.Request;



public sealed class ListingSearchRequestValidator: AbstractValidator<ListingSearchRequest>
{
    public ListingSearchRequestValidator()
    {
        RuleFor(x => x.take).InclusiveBetween(1, 30).WithMessage("take is out of range");
        RuleFor(x => x.skip).GreaterThanOrEqualTo(0).WithMessage("skip must be positive");
        RuleFor(x => x.QueryString).MaximumLength(150)
            .WithMessage("Query length must be less than 150 characters.")
            .When(x => x.QueryString is not null);
        RuleFor(x => x.Country).MaximumLength(100)
            .WithMessage("Country length must be less than 100 characters.")
            .When(x => x.Country is not null);
        RuleFor(x => x.City)
            .MaximumLength(100)
            .WithMessage("City length must be less than 100 characters.")
            .When(x => x.City is not null);
        RuleFor(x => x.MinimumPrice).GreaterThanOrEqualTo(0).WithMessage("MinimumPrice must be positive.")
            .When(x => x.MinimumPrice is not null);
        RuleFor(x => x.MaximumPrice).GreaterThan(0).WithMessage("MaximumPrice must be positive.")
            .When(x => x.MinimumPrice is not null);
        RuleFor(x => x).Must(x => x.MinimumPrice < x.MaximumPrice)
                       .WithMessage("The maximum price must be higher than the minimum price.")
                       .When(x => x.MinimumPrice is not null && x.MaximumPrice is not null);
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required").
                Must(   
                    x => DateTime.Today <= x && 
                    DateTime.Today.AddYears(1) > x
                ).WithMessage("Start Date must be between today and next year");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("EndDate is required").
            Must(   
                x => DateTime.Today.AddDays(1) <= x && 
                DateTime.Today.AddYears(1) >  x 
            ).WithMessage("End Date must be between tomorrow and next year").
            GreaterThanOrEqualTo(x => x.StartDate.AddDays(1)).
            WithMessage("EndDate cannot be before StartDate");
        RuleFor(x => x.Beds)
            .InclusiveBetween(0, 100)
            .WithMessage("The number of beds must be between 0 and 100")
            .When(x => x.Beds is not null);
        RuleFor(x => x.Bedrooms)
           .InclusiveBetween(0, 100)
           .WithMessage("Input the number of bedrooms must be between 0 and 100")
           .When(x => x.Bedrooms is not null);
        RuleFor(x => x.Bathrooms)
           .InclusiveBetween(0, 100)
           .WithMessage("Input the number of Bathrooms must be between 0 and 100")
           .When(x => x.Bathrooms is not null);
    }
}