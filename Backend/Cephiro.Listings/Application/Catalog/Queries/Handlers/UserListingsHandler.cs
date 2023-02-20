using MassTransit;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Queries;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;


namespace Cephiro.Listings.Application.Catalog.Queries.Handlers;


public sealed class UserListingsHandler: IConsumer<UserListingsRequest>
{
    private readonly ICatalogAccess _catalogRepository; 

    public UserListingsHandler(ICatalogAccess catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<UserListingsRequest> context)
    {
        var response = await _catalogRepository.UserListings(context.Message, context.CancellationToken); 
        await context.RespondAsync<UserListingsResponse>(response ?? new UserListingsResponse{});
   }

}