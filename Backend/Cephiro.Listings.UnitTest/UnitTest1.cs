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



namespace Cephiro.Listings.UnitTest;

public class Tests
{
    private CreationHandler handler;
    private Mock<ICatalogExecute> _MockRepo;
    private Mock<IValidator<CreationRequest>> _Mockvalidator;


    [SetUp]
    public void Setup()
    {
        _MockRepo = new Mock<ICatalogExecute>();
        _Mockvalidator = new Mock<IValidator<CreationRequest>>();
    }

    [Test]
    public async Task Test1()
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
                    Price_day = 0,
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
                    DishWasher = true
                };
        var validationResult = new FluentValidation.Results.ValidationResult();
        //_Mockvalidator.Setup(x => x.Validate(creation)).Returns(validationResult);
        _Mockvalidator.Setup(x => x.Validate(It.IsAny<CreationRequest>())).Returns(validationResult);


        _MockRepo.Setup(x => x.CreateListing(creation, new CancellationToken()))
            .ReturnsAsync(new DbWriteInternal{
                ChangeCount = 0,
                Error = null
        });


        var harness = new InMemoryTestHarness();
        var consumer = harness.Consumer(() => {
            return new CreationHandler(_MockRepo.Object, _Mockvalidator.Object);
        });

        var response_ = new CreationResponse{
            IsError = false,
            Error = null
        };

        await harness.Start();
        try
        {
            var requestClient = await harness.ConnectRequestClient<CreationRequest>();

            var response = await requestClient.GetResponse<CreationResponse>(creation);

            _MockRepo.Verify(x => x.CreateListing(It.IsAny<CreationRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            _Mockvalidator.Verify(x => x.Validate(It.IsAny<CreationRequest>()), Times.Once());
            Assert.IsTrue(response.Message.IsError == response_.IsError && response.Message.Error == response_.Error);
        }
        finally
        {
            await harness.Stop();
        }
    }
}