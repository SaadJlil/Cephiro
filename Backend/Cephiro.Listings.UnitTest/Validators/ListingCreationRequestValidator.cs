using Cephiro.Listings.Application.Catalog.Commands.Handlers;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Application.Catalog.Commands;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Moq;
using FluentValidation;
using FluentValidation.TestHelper;

using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Cephiro.Listings.Application.Catalog.Contracts.Validators.Request;



namespace Cephiro.Listings.UnitTest.Consumers;

public class ListingCreationValidator 
{
    private CreationRequestValidator validator;

    [SetUp]
    public void Setup()
    {
        validator = new CreationRequestValidator();
    }

    [Test]
    public async Task ListingCreationValidator_Success()
    {
        var creation = new CreationRequest{
                    Images = new List<Uri>{
                        new Uri("/string")
                    },
                    Addresse = new Location(
                        street : "string",
                        country : "string",
                        city : "string",
                        zipCode : "strin",
                        longitude : 180,
                        latitude : 90
                    ),
                    Description = "string",
                    Price_day = 10,
                    Type = 0,
                    UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "string",
                    Beds = 0,
                    Bedrooms = 0,
                    Bathrooms = 0,
                    Wifi = true,
                    AirConditioning = true,
                    Smoking = true,
                    WashingMachine = true,
                    DishWasher = true,
                    Surface = 50
                };

        var validation = await validator.ValidateAsync(creation);
        Assert.IsTrue(validation.IsValid);
   }

    [Test]
    public async Task ListingCreationValidator_PriceNegative_Failure()
    {
        var creation = new CreationRequest{
                    Images = new List<Uri>{
                        new Uri("/string")
                    },
                    Addresse = new Location(
                        street : "string",
                        country : "string",
                        city : "string",
                        zipCode : "strin",
                        longitude : 180,
                        latitude : 90
                    ),
                    Description = "string",
                    Price_day = -10,
                    Type = 0,
                    UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "string",
                    Beds = 0,
                    Bedrooms = 0,
                    Bathrooms = 0,
                    Wifi = true,
                    AirConditioning = true,
                    Smoking = true,
                    WashingMachine = true,
                    DishWasher = true,
                    Surface = 50
                };

        var validation = await validator.TestValidateAsync(creation);
        validation.ShouldHaveValidationErrorFor(x => x.Price_day);
   }
}
