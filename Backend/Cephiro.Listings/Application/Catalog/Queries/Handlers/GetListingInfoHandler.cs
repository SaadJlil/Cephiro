using MassTransit;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;


namespace Cephiro.Listings.Application.Catalog.Queries.Handlers;


public sealed class GetListingInfoHandler: IConsumer<ListingInfoRequest>
{
    private readonly ICatalogAccess _catalogRepository; 

    public GetListingInfoHandler(ICatalogAccess catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<ListingInfoRequest> context)
    {


        var response = await _catalogRepository.GetListingInfo(context.Message, context.CancellationToken); 
        await context.RespondAsync<ListingInfoResponse>(new ListingInfoResponse{
            Info = response
        });

    }

}