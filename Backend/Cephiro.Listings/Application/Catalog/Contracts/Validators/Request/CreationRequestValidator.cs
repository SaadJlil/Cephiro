using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class CreationRequestValidator: AbstractValidator<CreationRequest>
{
    public CreationRequestValidator()
    {
        //Not null
        RuleFor(x => x.Images).NotEmpty().WithMessage("Images are required");
        RuleFor(x => x.Addresse).NotEmpty().WithMessage("The Address is obligatory");
        RuleFor(x => x.Type).NotNull().WithMessage("Listing type is required");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Userid is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Wifi).NotEmpty().WithMessage("Input if the listing has a wifi or not");
        RuleFor(x => x.AirConditioning).NotEmpty().WithMessage("Input if the listing has a airconditioning or not");
        RuleFor(x => x.Smoking).NotEmpty().WithMessage("Input if smoking is allowed or not");
        RuleFor(x => x.WashingMachine).NotEmpty().WithMessage("Input if the listing has a washing machine or not");
        RuleFor(x => x.DishWasher).NotEmpty().WithMessage("Input if the Listing has a Dishwasher or not");

        //String Length
        RuleFor(x => x.Beds).NotNull().WithMessage("Input the number beds is required").GreaterThanOrEqualTo(0).WithMessage("The number of beds must be positive");
        RuleFor(x => x.Surface).NotNull().WithMessage("Input the Surface is required").GreaterThanOrEqualTo(0).WithMessage("Surface must be positive");
        RuleFor(x => x.Bedrooms).NotNull().WithMessage("Input the number beds is required").GreaterThanOrEqualTo(0).WithMessage("The number of bedrooms must be positive");
        RuleFor(x => x.Bathrooms).NotNull().WithMessage("Input the number bathrooms is required").GreaterThanOrEqualTo(0).WithMessage("The number of bathrooms must be positive");
        RuleFor(x => x.Price_day).NotEmpty().GreaterThan(0).WithMessage("Price has to be great than 0");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description length must be less than 500 characters.").When(x => x.Description is not null);
        RuleFor(x => x.Name).MaximumLength(70).WithMessage("Name length must be less than 70 characters.");
    }
}