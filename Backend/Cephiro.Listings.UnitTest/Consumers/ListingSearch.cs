using Cephiro.Listings.Application.Search.Queries.Handlers;
using Cephiro.Listings.Application.Search.Contracts.Request;
using Cephiro.Listings.Application.Search.Contracts.Response;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Application.Search.Queries;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Moq;
using FluentValidation;
using FluentAssertions;
using FluentValidation.TestHelper;

using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;



namespace Cephiro.Listings.UnitTest.Consumers;

public class ListingSearch
{
    private Mock<ISearchAccess>? _MockRepo;
    private Mock<IValidator<ListingSearchRequest>>? _Mockvalidator;


    [SetUp]
    public void Setup()
    {
        _MockRepo = new Mock<ISearchAccess>();
        _Mockvalidator = new Mock<IValidator<ListingSearchRequest>>();
    }

    [Test]
    public async Task ListingSearch_ValidRequest_Success()
    {
        var creation = new ListingSearchRequest
        {
            EndDate = DateTime.Now.AddDays(2).ToUniversalTime(),
            Country = "Morocco"
        };

        var validationResult = new FluentValidation.Results.ValidationResult();
        _Mockvalidator.Setup(x => x.ValidateAsync(
            It.IsAny<ListingSearchRequest>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(validationResult);

        var sId = new Guid();

        _MockRepo.Setup(x => x.GetListingSearch(It.IsAny<ListingSearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListingSearchResponse
            {
                minilistings = new List<MinimalListingInfoInternal>{
                    new MinimalListingInfoInternal{
                        Id = sId,
                        Images = new List<string>{
                                "/string"
                        },
                        Country = "Morocco",
                        City = "Casablanca",
                        Name = "House under the sea",
                        Price = 10,
                        Stars = 3.5f
                    }
                },
                IsError = false
            });


        var harness = new InMemoryTestHarness();
        var consumer = harness.Consumer(() =>
        {
            return new ListingSearchHandler(_MockRepo.Object, _Mockvalidator.Object);
        });


        var response_ = new ListingSearchResponse
        {
            minilistings = new List<MinimalListingInfoInternal>{
                new MinimalListingInfoInternal{
                    Id = sId,
                    Images = new List<string>{
                            "/string"
                    },
                    Country = "Morocco",
                    City = "Casablanca",
                    Name = "House under the sea",
                    Price = 10,
                    Stars = 3.5f
                }
            }
        };

        await harness.Start();

        try
        {
            var requestClient = await harness.ConnectRequestClient<ListingSearchRequest>();

            var response = await requestClient.GetResponse<ListingSearchResponse>(creation);

            _MockRepo.Verify(x => x.GetListingSearch(It.IsAny<ListingSearchRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            _Mockvalidator.Verify(x => x.ValidateAsync(It.IsAny<ListingSearchRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            response.Message.Should().BeEquivalentTo(response_);
        }
        finally
        {
            await harness.Stop();
        }
    }

    [Test]
    public async Task ListingSearch_NonValidRequest_Failure()
    {
        var creation = new ListingSearchRequest
        {
            EndDate = DateTime.Now.AddDays(-2).ToUniversalTime(),
            Country = "Morocco"
        };

        var validationResult = new FluentValidation.Results.ValidationResult();
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("ListingSearchRequest", "End Date must be between tomorrow and next year"));
        _Mockvalidator.Setup(x => x.ValidateAsync(
            It.IsAny<ListingSearchRequest>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(validationResult);

        var sId = new Guid();

        _MockRepo.Setup(x => x.GetListingSearch(It.IsAny<ListingSearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ListingSearchResponse
            {
                minilistings = new List<MinimalListingInfoInternal>{
                    new MinimalListingInfoInternal{
                        Id = sId,
                        Images = new List<string>{
                                "/string"
                        },
                        Country = "Morocco",
                        City = "Casablanca",
                        Name = "House under the sea",
                        Price = 10,
                        Stars = 3.5f
                    }
                },
                IsError = false
            });


        var harness = new InMemoryTestHarness();
        var consumer = harness.Consumer(() =>
        {
            return new ListingSearchHandler(_MockRepo.Object, _Mockvalidator.Object);
        });


        var response_ = new ListingSearchResponse
        {
            IsError = true,
            Message = "End Date must be between tomorrow and next year"
        };

        await harness.Start();

        try
        {
            var requestClient = await harness.ConnectRequestClient<ListingSearchRequest>();

            var response = await requestClient.GetResponse<ListingSearchResponse>(creation);

            _Mockvalidator.Verify(x => x.ValidateAsync(It.IsAny<ListingSearchRequest>(), It.IsAny<CancellationToken>()), Times.Once());
            response.Message.Should().BeEquivalentTo(response_);
        }
        finally
        {
            await harness.Stop();
        }
    }
}