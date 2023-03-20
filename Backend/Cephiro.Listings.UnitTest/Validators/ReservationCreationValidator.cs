using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.TestHelper;

using Cephiro.Listings.Application.Reservation.Contracts.Validators.Request;



namespace Cephiro.Listings.UnitTest.Validators;

public class ReservationCreationValidator 
{
    private CreateReservationRequestValidator validator;

    [SetUp]
    public void Setup()
    {
        validator = new CreateReservationRequestValidator();
    }

    [Test]
    public async Task ListingCreationValidator_Success()
    {
        var creation = new CreateReservationRequest{
            UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            ListingId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
            StartDate = DateTime.Now.AddDays(1).ToUniversalTime(),
            EndDate = DateTime.Now.AddDays(3).ToUniversalTime()
        };

       
        var validation = await validator.ValidateAsync(creation);
        Assert.IsTrue(validation.IsValid);
    }

    [Test]
    public async Task ListingCreationValidator_StartDateBeforeEndate_Failure()
    {
        var creation = new CreateReservationRequest{
            UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            ListingId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
            StartDate = DateTime.Now.AddDays(3).ToUniversalTime(),
            EndDate = DateTime.Now.AddDays(1).ToUniversalTime()
        };

        var validation = await validator.TestValidateAsync(creation);
        validation.ShouldHaveValidationErrorFor(x => x.EndDate);
   }
}
