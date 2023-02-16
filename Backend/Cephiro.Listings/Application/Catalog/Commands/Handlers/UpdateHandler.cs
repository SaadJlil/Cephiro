using MassTransit;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Commands;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;


namespace Cephiro.Listings.Application.Catalog.Commands.Handlers;


public sealed class UpdateListingHandler: IConsumer<UpdateListingRequest>
{
    private readonly ICatalogExecute _catalogRepository; 

    public UpdateListingHandler(ICatalogExecute catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<UpdateListingRequest> context)
    {
        CreationResponse result = new () {IsError = false, Error = null};

        var response = await _catalogRepository.UpdateListing(context.Message, context.CancellationToken); 

        if(response.Error is null)
        {
            await context.RespondAsync<CreationResponse>(result);
        }

        result.Error = response.Error;
        result.IsError = false;
        await context.RespondAsync<CreationResponse>(result);
    }

}