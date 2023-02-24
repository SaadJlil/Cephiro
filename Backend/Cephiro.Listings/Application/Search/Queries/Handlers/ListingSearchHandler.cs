using MassTransit;
using Cephiro.Listings.Application.Search.Contracts.Response;
using Cephiro.Listings.Application.Search.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Search.Queries;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;


namespace Cephiro.Listings.Application.Search.Queries.Handlers;


public sealed class ListingSearchHandler: IConsumer<ListingSearchRequest>
{
    private readonly ISearchAccess _SearchRepository; 

    public ListingSearchHandler(ISearchAccess SearchRepository)
    {
        _SearchRepository = SearchRepository;
    }

    public async Task Consume(ConsumeContext<ListingSearchRequest> context)
    {
        var response = await _SearchRepository.GetListingSearch(context.Message, context.CancellationToken); 
        await context.RespondAsync<ListingSearchResponse>(response ?? new ListingSearchResponse{});
   }

}