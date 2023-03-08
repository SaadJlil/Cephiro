using Cephiro.Listings.Application.Catalog.Commands.Handlers;
using Cephiro.Listings.Application.Catalog.Commands;
using Moq;
using FluentValidator;

namespace Cephiro.Listings.UnitTests;

public class UnitTest1
{
    private readonly CreationHandler handler;
    private readonly Mock<ICatalogExecute> _mockRepo;
    private readonly Mock<IValidator<CreationRequest>> _mockRepo;


    public UnitTest1()
    {
        _mockRepo = new Mock<ICatalogExecute>();
        handler = new CreationHandler(_mockRepo.Object);

    }

    [Fact]
    public void Test1()
    {
        _mockRepo.Setup(x => x.thingservice("I don't ask for much")).Returns("zbi");


    }
}