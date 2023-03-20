using Cephiro.Listings.Application.Search.Contracts.Request;
using Cephiro.Listings.Domain.ValueObjects;
using FluentValidation;
using FluentValidation.TestHelper;

using Cephiro.Listings.Application.Search.Contracts.Validators.Request;



namespace Cephiro.Listings.UnitTest.Validators;

public class GetListingSearchRequestValidator 
{
    private ListingSearchRequestValidator validator;

    [SetUp]
    public void Setup()
    {
        validator = new ListingSearchRequestValidator();
    }

    [Test]
    public async Task ListingSearchValidator_Success()
    {
        var creation = new ListingSearchRequest
        {
            EndDate = DateTime.Now.AddDays(2).ToUniversalTime(),
            Country = "Morocco"
        };

       
        var validation = await validator.ValidateAsync(creation);
        Assert.IsTrue(validation.IsValid);
    }

    [Test]
    public async Task ListingSearchValidator_EndDateNow_Failure()
    {
        var creation = new ListingSearchRequest
        {
            EndDate = DateTime.Now.ToUniversalTime(),
            Country = "Morocco"
        };

        var validation = await validator.TestValidateAsync(creation);
        validation.ShouldHaveValidationErrorFor(x => x.EndDate);
   }
}
