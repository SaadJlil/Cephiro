using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



public sealed class CreationValidator: AbstractValidator<CreationRequest>
{
    public CreationValidator()
    {
        //Not null
        RuleFor(x => x.Images).NotEmpty();
        RuleFor(x => x.Addresse).NotEmpty();
        RuleFor(x => x.Price_day).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Beds).NotEmpty();
        RuleFor(x => x.Bedrooms).NotEmpty();
        RuleFor(x => x.Bathrooms).NotEmpty();
        RuleFor(x => x.Wifi).NotEmpty();
        RuleFor(x => x.AirConditioning).NotEmpty();
        RuleFor(x => x.Smoking).NotEmpty();
        RuleFor(x => x.WashingMachine).NotEmpty();
        RuleFor(x => x.DishWasher).NotEmpty();

        //String Length
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description length must be less than 500 characters.");
        RuleFor(x => x.Name).MaximumLength(70).WithMessage("Name length must be less than 70 characters.");
    }
}