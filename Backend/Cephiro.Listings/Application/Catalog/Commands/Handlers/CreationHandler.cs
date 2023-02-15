using MassTransit;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Commands;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;


namespace Cephiro.Listings.Application.Catalog.Commands.Handlers;


public sealed class CreationHandler: IConsumer<CreationRequest>
{
    private readonly ICatalogExecute _catalogRepository; 

    public CreationHandler(ICatalogExecute catalogRepository, CancellationToken token)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<CreationRequest> context)
    {
        CreationResponse result = new () {IsError = false, Error = null};

        var response = await _catalogRepository.CreateListing(context.Message, context.CancellationToken); 

        if(response.Error is null)
        {
            await context.RespondAsync<CreationResponse>(result);
        }

        result.Error = response.Error;
        result.IsError = true;
        await context.RespondAsync<CreationResponse>(result);
    }

}