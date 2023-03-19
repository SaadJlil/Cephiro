using Cephiro.Listings.Application.Reservation.Commands.Handlers;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Application.Reservation.Commands;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Moq;
using FluentValidation;
using FluentValidation.TestHelper;

using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;



namespace Cephiro.Listings.UnitTest.Consumers;

public class CreateReservation 
{
    private Mock<IReservationExecute>? _MockRepo;
    private Mock<IValidator<CreateReservationRequest>>? _Mockvalidator;


    [SetUp]
    public void Setup()
    {
        _MockRepo = new Mock<IReservationExecute>();
        _Mockvalidator = new Mock<IValidator<CreateReservationRequest>>();
    }

    [Test]
    public async Task ReservationCreation_ValidRequest_Success()
    {
        var creation = new CreateReservationRequest{
                    UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    ListingId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
                    StartDate = DateTime.Now.AddDays(1).ToUniversalTime(),
                    EndDate = DateTime.Now.AddDays(3).ToUniversalTime()
                };
        var validationResult = new FluentValidation.Results.ValidationResult();
        _Mockvalidator.Setup(x => x.ValidateAsync(
            It.IsAny<CreateReservationRequest>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(validationResult);


        _MockRepo.Setup(x => x.CreateReservation(creation, new CancellationToken()))
            .ReturnsAsync(new DbWriteInternal{
                ChangeCount = 1,
                Error = null
        });


        var harness = new InMemoryTestHarness();
        var consumer = harness.Consumer(() => {
            return new CreateReservationHandler(_MockRepo.Object, _Mockvalidator.Object);
        });

        var response_ = new CreateReservationResponse{
            IsError = false,
            Error = null
        };

        await harness.Start();
        try
        {
            var requestClient = await harness.ConnectRequestClient<CreateReservationRequest>();

            var response = await requestClient.GetResponse<CreateReservationResponse>(creation);

            _MockRepo.Verify(x => x.CreateReservation(It.IsAny<CreateReservationRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            _Mockvalidator.Verify(x => x.ValidateAsync(It.IsAny<CreateReservationRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.IsTrue(response.Message.IsError == response_.IsError && response.Message.Error == response_.Error);
        }
        finally
        {
            await harness.Stop();
        }
    }
/*
    [Test]
    public async Task ListingCreation_NonValidRequest_Failure()
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

        var validationResult = new FluentValidation.Results.ValidationResult();
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("propertyName", "Error message."));
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

        await harness.Start();
        try
        {
            var requestClient = await harness.ConnectRequestClient<CreationRequest>();

            var response = await requestClient.GetResponse<CreationResponse>(creation);

            _Mockvalidator.Verify(x => x.Validate(It.IsAny<CreationRequest>()), Times.Once());
            Assert.IsTrue(response.Message.IsError == true);
        }
        finally
        {
            await harness.Stop();
        }
    }
*/
}
